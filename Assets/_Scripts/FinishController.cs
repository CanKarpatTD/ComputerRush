using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    public int divisionValue;
    public int collectedObjectCount;

    public bool isDone;

    public GameObject desk;
    public float zPos;

    private void Start()
    {
        zPos = 11;
    }

    private void Update()
    {
        if (isDone)
        {
            //TODO: Masalar "divisionValue" kadar spawn olucak. Kamera masaları takip edicek. Her masaya kamera geldiğinde eşya düşücek.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instance.hardwareList.Count <= 0 && GameManager.Instance.stockedHardwareList.Count <= 0)
            {
                GameManager.Instance.GameLose();
            }
            
            GameManager.Instance.gameState = GameManager.GameState.Win;
            
            var a = collectedObjectCount;
            var b = GameManager.Instance.stockedHardwareList.Count;

            var c = a + b;
            divisionValue = c / 3;

            StartCoroutine(StartInstantiate());
        }
        
        if (other.gameObject.CompareTag("Collected"))
        {
            collectedObjectCount++;
            
            Destroy(other.gameObject);
        }
    }

    private void StockCameFinishEffect()
    {
        //TODO: Particle
    }

    private IEnumerator StartInstantiate()
    {
        int i;
        
        for (i = 0; i < divisionValue; i++)
        {
            GameObject a = Instantiate(desk, gameObject.transform.parent.transform, false);

            a.transform.localPosition = new Vector3(2,-0.49f,zPos);

            zPos += 10;
            
            Camera.main.transform.DOLocalMove(new Vector3(3.5f, 3.5f, -7f),0.6f);
            Camera.main.transform.DOLocalRotate(new Vector3(20.28f, -26.763f, 0), 0.6f);

            yield return new WaitForSeconds(0.5f);
            
            //GameManager.Instance.cf.animationMode = true;
            GameManager.Instance.cf.target = a.transform.GetChild(0).transform.GetChild(0).transform;
            
            yield return new WaitForSeconds(0.5f);
            
            //GameManager.Instance.cf.animationMode = false;
        }

        if (i == divisionValue)
        {
            GameManager.Instance.GameWin();
        }
    }
}
