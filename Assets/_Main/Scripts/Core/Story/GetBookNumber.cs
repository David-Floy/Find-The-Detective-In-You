using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetBookNumber : MonoBehaviour, IInteractable
{
   public TextMeshProUGUI noteText;
  

   public void Interact()
   {
       Debug.Log(gameObject.GetComponentInChildren<TextMeshPro>().text);
         noteText.text = gameObject.GetComponentInChildren<TextMeshPro>().text;
      
      
   }
   
   


}
