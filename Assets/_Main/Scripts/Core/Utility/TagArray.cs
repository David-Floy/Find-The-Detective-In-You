using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagArray : MonoBehaviour
{

    public static GameObject[] manager;
    public static GameObject[] trigger;
    public void Start()
    {
        manager = GameObject.FindGameObjectsWithTag("Manager");
        trigger = GameObject.FindGameObjectsWithTag("Trigger");
    }
}
