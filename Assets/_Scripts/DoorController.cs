using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public enum DoorType
    {
        None,
        Upgrade,
        Stock
    }

    public DoorType doorType;


    public TextMeshPro stockText;
    //public int stocked;

    private void Update()
    {
        if (doorType == DoorType.Stock)
            stockText.text = GameManager.Instance.stocked.ToString();
    }
}
