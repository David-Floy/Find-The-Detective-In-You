using System.Collections;
using DIALOGUE;
using System.Collections.Generic;
using UnityEngine;

public class TestingDialogue : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        StartConversation();
    }

    void StartConversation()
    {
        List<string> lines = FileManager.ReadTextAsset("TestDialogue");
        
        
        

        DialogueSystem.instance.Say(lines);
    }

}
