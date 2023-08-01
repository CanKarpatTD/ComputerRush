using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalCounter : MonoBehaviour
{
    public TextMeshPro text;
    private void Update()
    {
        var a = GameManager.Instance.hardwareList.Count + GameManager.Instance.stockedHardwareList.Count;
        text.text = a.ToString();
    }
}
