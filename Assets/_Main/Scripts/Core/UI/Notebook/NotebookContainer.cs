using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Update = Unity.VisualScripting.Update;


namespace NOTE
{
    [System.Serializable]
    public class NotebookContainer 
    {
        public GameObject root;
        public TextMeshProUGUI noteText;
        private CanvasGroupController canvasGroupController;
        private bool initialized = false;

        public void Initialize()
        {
            if (initialized)
            {
                return;
            }

            canvasGroupController =
                new CanvasGroupController(NoteManager.instance, root.GetComponent<CanvasGroup>(), 4);
        }
            
           

        public bool isVisible => canvasGroupController.isVisible;

        public Coroutine Show() => canvasGroupController.Show();

        public Coroutine Hide() => canvasGroupController.Hide();


    }
    
}