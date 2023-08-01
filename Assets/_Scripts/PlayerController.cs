using System;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
            
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Door"))
        {
            var checker = other.gameObject.GetComponent<DoorController>().doorType;

            if (checker == DoorController.DoorType.Stock)
                other.gameObject.transform.DOScale(0, 0.1f).SetEase(Ease.InBack);
        }
    }
}
