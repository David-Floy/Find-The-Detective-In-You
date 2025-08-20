using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;
using Unity.VisualScripting;


public class NPCInteractable : MonoBehaviour, IInteractable
{

    [Serialize] public string path = "default";
    
    public static bool isRunning = false;
    public static bool readyToInteract = true;
    private float resetTime = 0.2f;
    private GameObject HintBox = null;

    public void Awake()
    {
        HintBox = Utility.FindByTagName(GameObject.FindGameObjectsWithTag("Manager"), "HintManager");
    }

    public void Interact()
    {
     
        HintBox.GetComponent<HintContainerManager>().Hide();
        //GameObject.Find("RootDialogueBox").GetComponent<CanvasGroup>().alpha = 1;
        if (!isRunning && readyToInteract)
        {
           
            string filePath = FilePaths.GetPathToResource(FilePaths.resources_dialogueFiles, path);
            List<string> lines = FileManager.ReadTextAsset(filePath);

            DialogueSystem.instance.Say(lines);
            isRunning = true;
            
            readyToInteract = false;
            Invoke(nameof(restInteract), resetTime);
        }
        else if (readyToInteract)
        {
            DialogueSystem.instance.OnUserPrompt_Next();
            
            readyToInteract = false;
            Invoke(nameof(restInteract), resetTime);
        }
    }
    private void restInteract()
    {
        readyToInteract = true;
    }
    
    
}
