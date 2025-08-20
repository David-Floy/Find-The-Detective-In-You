using System;
using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using UnityEngine;
using Random = UnityEngine.Random;


namespace TESTING
{
    public class Testing_Architect : MonoBehaviour
    {
        private DialogueSystem ds;
        private TextArchitect architect;

        public TextArchitect.BuildMethod bm = TextArchitect.BuildMethod.instant;

        private string[] lines = new string[5]
        {
            "Ich teste die Dialog Funktion des Spieles ich hoffe alles funktioniert sehr gut hahah bitte es soll klappen.",
            "Test Dialogue 2,Test Dialogue 2, Test Dialogue 2.",
            "Test Dialogue 3,Test Dialogue 3, Test Dialogue 3.",
            "Test Dialogue 4,Test Dialogue 4, Test Dialogue 4.",
            "Test Dialogue 5,Test Dialogue 5, Test Dialogue 5."
        };
        private void Start()
        {
            ds = DialogueSystem.instance;
            architect = new TextArchitect(ds.dialogueContainer.dialogueText);
            architect.buildMethod = TextArchitect.BuildMethod.fade;
        }
        
        private void Update()
        {
            if (bm != architect.buildMethod)
            {
                architect.buildMethod = bm;
                architect.Stop();
            }
            /*    
            if (NpcInteractable.isInteractable == true)
            {
                architect.Build(lines[Random.Range(0, lines.Length)]);
                NpcInteractable.isInteractable = false;
            }*/
            else if (Input.GetKeyDown(KeyCode.A))
            {
                architect.Append(lines[Random.Range(0, lines.Length)]);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (architect.isBuilding)
                {
                    if (!architect.hurryUp)
                    {
                        architect.hurryUp = true;
                    }
                    else
                    {
                        architect.ForceComplete();
                    }
                }
            }
        }
    }
}
