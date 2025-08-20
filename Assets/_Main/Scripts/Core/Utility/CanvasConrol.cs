using UnityEngine;

   public class CanvasConrol
   {
      public static void Show(CanvasGroup cg)
      {
         cg.alpha = 1;
         cg.interactable = true;
         cg.blocksRaycasts = true;
      }

      public static void Hide(CanvasGroup cg)
      {
         cg.alpha = 0;
         cg.interactable = false;
         cg.blocksRaycasts = false;
      }
   }
