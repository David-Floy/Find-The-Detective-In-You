using System;
using UnityEngine;
using UnityEngine.Serialization;

public class HintTrigger : MonoBehaviour
{
    

    public bool active = false;
    
    [TextArea(15,20)]
    public string hint = "";
    public void OnTriggerStay(Collider other)
    {
        if (active)
        {
            HintContainer.HintBox.ShowHint(hint);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (active)
        {
            HintContainer.HintBox.Hide();
            active = false;
        }
        
    }
}
