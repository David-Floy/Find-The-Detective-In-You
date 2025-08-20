using System;
using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using Unity.VisualScripting;
using UnityEngine;

public class TestConversationQueue : MonoBehaviour
{
   private void Start()
   {
      StartCoroutine(Running());
   }

   IEnumerator Running()
   {
      List<string> lines = new List<string>()
      {
         "This is Line 1 from origninal",
         "This is Line 2 from origninal",
         "This is Line 3 from origninal",
         "This is Line 4 from origninal"
      };
      yield return DialogueSystem.instance.Say(lines);
      
   }
}
