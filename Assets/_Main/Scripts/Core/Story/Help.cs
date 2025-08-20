using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NOTE;
using Unity.Burst.CompilerServices;
using UnityEngine;


public class Help : MonoBehaviour
{
   public static Help instance;
   
   
   private void Awake()
   {
      instance = this;
   }
   
   public void StartTimer()
   {
      Invoke("TimerCallback", 360f);
   }

   public void StopTimer()
   {
      // Timer mit der gespeicherten TimerID stoppen
         CancelInvoke();
   }
   private void TimerCallback()
   {
      HintContainer.HintBox.ShowHintFor("Hmm, vielleicht sollte ich zum Hafen gehen", 8000);
      NoteManager.Add("Zum Hafen gehen.");
      Debug.Log("times up");
   }
}
