using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Instance Method / GameState
    public static GameManager Instance;
    private void InstanceMethod()
    {
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    
    public enum GameState
    {
        Play,
        Pause,
        Win,
        Lose,
        StartMenu,
    }
    public GameState gameState;
    #endregion

    [Space(5)][Header("Mechanic Variables")]
    public float clampValue = 5;
    public float speedSideways = 500;
    private bool holding;
    private Vector3 pos1, pos2;
    
    [Space(5)][Header("Player Variables")]
    [Space(20)]public GameObject player;
    private Rigidbody playerRb;
    public Animator anim;

    public float moveSpeed;

    [Space(5)][Header("Game Variables")]
    [Space(20)]public List<GameObject> hardwareList = new List<GameObject>();
    public List<GameObject> stockedHardwareList = new List<GameObject>();
    
    private float firstProductOffsetZ;
    
    public GameObject sphereFollower;
    
    public GameObject hardwareMouse;
    public GameObject hardwareKeyboard;
    public GameObject hardwareMonitor;

    public GameObject collectableMouse;
    public GameObject collectableKeyboard;
    public GameObject collectableMonitor;

    public GameObject stackerObj;
    
    public float stackIndex;

    public int stocked;

    public CameraFollow cf;

    [Header("Particles")] public GameObject smokeExplosion;
    public GameObject plexusEffect;
    
    private void Awake()
    {
        #region Instance Method
        InstanceMethod();
        #endregion
        
    }
    
    private void Start()
    {
        playerRb = player.GetComponent<Rigidbody>();
    }
    
    private void LateUpdate()
    {
        if (gameState == GameState.Play)
        {
           
        }
    }
    
    private void FixedUpdate()
    {
        if (gameState == GameState.Play)
        {
           player.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
           
           
        }
        
        SetStack();
    }
    
    private void Update()
    {
        //Clamp
        player.transform.position = new Vector3(Mathf.Clamp(playerRb.transform.position.x, -clampValue, clampValue), playerRb.transform.position.y, playerRb.transform.position.z);

        if (gameState == GameState.Play)
        {
            stackerObj.transform.position = new Vector3(stackerObj.transform.position.x, stackerObj.transform.position.y, sphereFollower.transform.position.z);
            
            if (Input.GetMouseButtonDown(0))
            {
                pos1 = GameManager.Instance.GetMousePosition();

                holding = true;
            }

            if (Input.GetMouseButton(0) && holding) //set players velocity on X axis and clamp value
            {
                pos2 = GameManager.Instance.GetMousePosition();

                Vector3 delta = pos1 - pos2;

                pos1 = pos2;

                playerRb.velocity = new Vector3(Mathf.Lerp(playerRb.velocity.x, -delta.x * speedSideways, 1f * Time.deltaTime), playerRb.velocity.y, playerRb.velocity.z);
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            holding = false;

            playerRb.velocity = Vector3.zero;
        }
    }

    public void CheckEmptyObj(int i)
    {
        Destroy(hardwareList[i].gameObject);

        TimeManager.Instance.transform.DOMove(new Vector3(0, 0, 0), 0.035f).OnComplete(() =>
        {
            // for (var b = hardwareList.Count - 1; b > -1; b--)
            // {
            //     if (hardwareList[b] == null)
            //     {
            //         hardwareList.RemoveAt(b);
            //     }
            // }

            foreach (var go in hardwareList)
            {
                if (go == null)
                {
                    hardwareList.Remove(go);
                }
            }
        });
    }
    
    public void GainProduct(int type)
    {
        if (hardwareList.Count < 29)
        {
            if (type == 1)
            {
                GameObject ga = Instantiate(hardwareMouse, new Vector3(0, 0, 0), Quaternion.identity);
                ga.transform.parent = stackerObj.transform;


                if (hardwareList.Count <= 0)
                {
                    ga.transform.localPosition = new Vector3(player.transform.localPosition.x, 0, stackIndex);
                }
                else
                {
                    ga.transform.localPosition = new Vector3(player.transform.localPosition.x, 0,
                        hardwareList[hardwareList.Count - 1].transform.localPosition.z + 0.5f);
                }

                hardwareList.Add(ga);
            }

            if (type == 2)
            {
                GameObject ga = Instantiate(hardwareKeyboard, new Vector3(0, 0, 0), Quaternion.identity);
                ga.transform.parent = stackerObj.transform;

                if (hardwareList.Count <= 0)
                {
                    ga.transform.localPosition = new Vector3(player.transform.localPosition.x, 0, stackIndex);
                }
                else
                {
                    ga.transform.localPosition = new Vector3(player.transform.localPosition.x, 0,
                        hardwareList[hardwareList.Count - 1].transform.localPosition.z + 0.5f);
                }

                hardwareList.Add(ga);
            }

            if (type == 3)
            {
                GameObject ga = Instantiate(hardwareMonitor, new Vector3(0, 0, 0), Quaternion.identity);
                ga.transform.parent = stackerObj.transform;

                if (hardwareList.Count <= 0)
                {
                    ga.transform.localPosition = new Vector3(player.transform.localPosition.x, 1, stackIndex);
                }
                else
                {
                    ga.transform.localPosition = new Vector3(player.transform.localPosition.x, 1,
                        hardwareList[hardwareList.Count - 1].transform.localPosition.z + 0.5f);
                }
                hardwareList.Add(ga);
            }
        }
        
        StartCoroutine(DelayedStackAnim());
    }
    
    public IEnumerator DelayedStackAnim()
    {
        if (hardwareList.Count > 0)
        {
            for (var i = hardwareList.Count - 1; i > -1; i--)
            {
                hardwareList[i].GetComponent<HardwareController>().BlopEffect();

                yield return new WaitForSeconds(0.03f);
            }
        }
    }
    
    private void SetStack()
    {
        for (int i = 0; i <  hardwareList.Count; i++)
        {
            if (hardwareList[i] != null)
            {
                if (hardwareList[i].GetComponent<HardwareController>().canMove)
                {
                    if (i != 0)
                    {
                        hardwareList[i].transform.localPosition = new Vector3(
                            Mathf.Lerp(hardwareList[i].transform.localPosition.x,
                                hardwareList[i - 1].transform.localPosition.x, 0.45f),
                            Mathf.Lerp(hardwareList[i].transform.localPosition.y,
                                hardwareList[i - 1].transform.localPosition.y, 0.45f),
                            hardwareList[i].transform.localPosition.z);
                    }
                    else
                    {
                        hardwareList[i].transform.position = Vector3.Lerp(hardwareList[i].transform.position,
                            new Vector3(sphereFollower.transform.position.x, sphereFollower.transform.position.y,
                                sphereFollower.transform.position.z + firstProductOffsetZ), 0.45f);
                    }
                }
            }
        }
    }

    #region Win/Lose/CoinUpdate
    
    public void GameWin()
    {
        gameState = GameState.Win;
        //////////////////////////
        UIManager.Instance._GameWin();
    }

    public void GameLose()
    {
        gameState = GameState.Lose;
        ///////////////////////////
        UIManager.Instance._GameLose();
        
        anim.SetTrigger("lose");
    }
    #endregion
    
    #region Constant Methods
    
    public static float ClampAngle(float angle, float min, float max)
    {
        angle = Mathf.Repeat(angle, 360);
        min = Mathf.Repeat(min, 360);
        max = Mathf.Repeat(max, 360);
        var inverse = false;
        var timing = min;
        var tangle = angle;
        if (min > 180)
        {
            inverse = true;
            timing -= 180;
        }
        if (angle > 180)
        {
            inverse = !inverse;
            tangle -= 180;
        }
        var result = !inverse ? tangle > timing : tangle < timing;
        if (!result)
            angle = min;
        inverse = false;
        tangle = angle;
        var tax = max;
        if (angle > 180)
        {
            inverse = true;
            tangle -= 180;
        }
        if (max > 180)
        {
            inverse = !inverse;
            tax -= 180;
        }
        result = !inverse ? tangle < tax : tangle > tax;
        if (!result)
            angle = max;
        return angle;
    }
    
    public Vector2 GetMousePosition()
    {
        var pos = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

        return pos;
    }
    
    #endregion
}
