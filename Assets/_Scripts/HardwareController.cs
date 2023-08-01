using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class HardwareController : MonoBehaviour
{
    public enum HardwareType
    {
        None,
        Mouse,
        Keyboard,
        Monitor
    }

    public HardwareType hardwareType;

    public GameObject mouse;
    public GameObject keyboard;
    public GameObject monitor;
    
    public bool canMove;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            if (other.gameObject.GetComponent<CollectableController>().collectableType == CollectableController.CollectableType.Mouse)
            {
                GameManager.Instance.GainProduct(1);
            }
            
            if (other.gameObject.GetComponent<CollectableController>().collectableType == CollectableController.CollectableType.Keyboard)
            {
                GameManager.Instance.GainProduct(2);
            }
            
            if (other.gameObject.GetComponent<CollectableController>().collectableType == CollectableController.CollectableType.Monitor)
            {
                GameManager.Instance.GainProduct(3);
            }
            
            PlexusExplosion();
            
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Door"))
        {
            var checker = other.gameObject.GetComponent<DoorController>().doorType;

            if (checker == DoorController.DoorType.Stock)
            {
                other.gameObject.transform.GetChild(1).transform.DOScale(0.22f, 0.1f).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    other.gameObject.transform.GetChild(1).transform.DOScale(0.1685751f, 0.1f);
                });

                GameManager.Instance.stocked ++;

                PlexusExplosion();
                
                GameManager.Instance.hardwareList.Remove(gameObject);
                GameManager.Instance.stockedHardwareList.Add(gameObject);
                Destroy(gameObject);
            }

            if (checker == DoorController.DoorType.Upgrade)
            {
                PlexusExplosion();
                
                if (hardwareType == HardwareType.Mouse)
                {
                    mouse.SetActive(false);
                    keyboard.SetActive(true);
                    monitor.SetActive(false);

                    TimeManager.Instance.transform.DOMoveX(0, 0.5f).OnComplete(() => { hardwareType = HardwareType.Keyboard; });
                }

                if (hardwareType == HardwareType.Keyboard)
                {
                    mouse.SetActive(false);
                    keyboard.SetActive(false);
                    monitor.SetActive(true);
                    
                    TimeManager.Instance.transform.DOMoveX(0, 0.5f).OnComplete(() => { hardwareType = HardwareType.Monitor; });
                }

                if (hardwareType == HardwareType.Monitor)
                {
                    mouse.SetActive(false);
                    keyboard.SetActive(false);
                    monitor.SetActive(true);
                }
                
                BlopEffect();
            }
        }
    }

    public void PlexusExplosion()
    {
        var pos = gameObject.transform.position.y + 1;
        Instantiate(GameManager.Instance.plexusEffect, new Vector3(gameObject.transform.position.x,pos,gameObject.transform.position.z), Quaternion.identity);
    }

    public void BlopEffect()
    {
        transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.2f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
        });
    }
}
