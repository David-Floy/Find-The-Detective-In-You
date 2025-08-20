using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TESTING
{

    public class ChoicePanelTest : MonoBehaviour
    {
        ChoicePanel panel;

        void Start()
        {
            StartCoroutine(Running());
        }


        IEnumerator Running()
        {
            panel = ChoicePanel.instance;

            string[] choices = new string[]
            {
                "Test1",
                "Test2",
                "Testuzgglkajshgdölkasjhölkhjäöloj#äpöoj",
                "jhdhasoiuhhsadkjihdkjashdkjlashdkalösjhdöalsdkhöalskhdlakdhälk"
            };
            
            panel.Show("Test Titel 12345676", choices);

            while (panel.isWaitingOnUserChoice)
            {
                yield return null;
            }

            var decision = panel.LastDecision;

            Debug.Log($"Made choice {decision.answerIndex} '{decision.choices[decision.answerIndex]}'");
        }
    }
}
