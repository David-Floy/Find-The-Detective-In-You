using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPanelTesting : MonoBehaviour
{
    public InputPanel inputPanel;
    
    
    void Start()
    {
        StartCoroutine(Running());
    }

    IEnumerator Running()
    {

        yield return null;
        inputPanel.Show("What is your name?");
        
        while (inputPanel.isWaitingOnUserInput)
        {
            yield return null;
        }

        string characterName = inputPanel.lastInput;
        Debug.Log(characterName);
        yield return null;
    }

}
