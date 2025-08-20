using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using DIALOGUE.LogicalLines;
using UnityEngine;

public class LL_Input : ILogicalLine
{
    public string keyword => "input";
    public bool Matches(DIALOGUE_LINE line)
    {
        return (line.hasSpeaker && line.speaker == keyword);
    }

    public IEnumerator Execute(DIALOGUE_LINE line)
    {
       InputPanel panel = InputPanel.instance;
       
       panel.Show(line.dialogue);

       while (panel.isWaitingOnUserInput)
       {
           yield return null;
       }
       PlayerMovement.lockMovement = true;
    }
}
