using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PropInteractable : MonoBehaviour
{
   
   public GameObject propElement;
   //public TextMeshProUGUI propTextbox = null;
   private float DEFAULT_FADE_SPEED = 4;

   private CanvasGroup rootCG => propElement.GetComponent<CanvasGroup>();

   private bool isShowing =  false;

   public void Interact()
   {
      if (isShowing)
      {
         Fading(0);
         isShowing = false;
      }
      else
      {
         Fading(1);
         isShowing = true;
      }
   }

   private void Fading(float alpha){
      
      while (rootCG.alpha != alpha)
      {
         rootCG.alpha = Mathf.MoveTowards(rootCG.alpha, alpha, Time.deltaTime * DEFAULT_FADE_SPEED);
      }
   }
}
      
   


