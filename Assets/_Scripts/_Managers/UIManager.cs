using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Instance Method
    public static UIManager Instance;
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
    
    #endregion

    #region Constant
    public TextMeshProUGUI levelIndex;
    public GameObject levelComplete,levelFailed,confetti;
    #endregion
    
    
    private void Awake()
    {
        #region Instance Method
        InstanceMethod();
        #endregion
    }
    
    private void Update()
    {
        
    }

    public void _GameStart()
    {
        levelIndex.enabled = true;
        GameManager.Instance.gameState = GameManager.GameState.Play;
        GameManager.Instance.anim.SetTrigger("run");
    }

    public void _GameWin()
    {
        levelIndex.enabled = false;
        levelComplete.SetActive(true);
        confetti.SetActive(true);
    }

    public void _GameLose()
    {
        levelIndex.enabled = false;
        levelFailed.SetActive(true);
    }
    
    public void SetLevelIndex()
    {
        levelIndex.text = "Level " + LevelManager.Instance.currentLevelNumber;
    }
}
