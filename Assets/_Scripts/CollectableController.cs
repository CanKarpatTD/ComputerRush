using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    public enum CollectableType
    {
        None,
        Mouse,
        Keyboard,
        Monitor
    }

    public CollectableType collectableType;
}
