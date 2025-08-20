
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;


namespace DIALOGUE
    {
        [System.Serializable]
        public class DialogueContainer
        {
            private const float DEFAULT_FADE_SPEED = 3f;
            
            public GameObject root;
            public NameContainer nameContainer;
            public TextMeshProUGUI dialogueText;

            private CanvasGroupController canvasGroupController;
            private bool initialized = false;

            public void Initialize()
            {
                if (initialized)
                {
                    return;
                }

                canvasGroupController =
                    new CanvasGroupController(DialogueSystem.instance, root.GetComponent<CanvasGroup>(), 3);
            }
            
           

            public bool isVisible => canvasGroupController.isVisible;

            public Coroutine Show()
            {
                //PlayerMovement.lockMovement = true;
                return canvasGroupController.Show();
            }

            public Coroutine Hide()
            {
                //PlayerMovement.lockMovement = false;
               return canvasGroupController.Hide();
            }

        }
    }