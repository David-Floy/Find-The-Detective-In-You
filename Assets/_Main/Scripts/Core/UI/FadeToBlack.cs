using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FadeToBlack : MonoBehaviour
{
    public GameObject root;
        private CanvasGroupController canvasGroupController;
        private bool initialized = false;
        public static FadeToBlack instance { get; private set; }

        public void Awake()
        {
            instance = this;
            
            canvasGroupController =
                new CanvasGroupController(this, root.GetComponent<CanvasGroup>(), 8);
        }

        
           

        public bool isVisible => canvasGroupController.isVisible;

        public Coroutine Show() => canvasGroupController.Show();
        public Coroutine ShowToSet(float count) => canvasGroupController.ShowToSet(count);

        public Coroutine Hide() => canvasGroupController.Hide();

        public void FadeInOut(float time)
        {
            canvasGroupController.FadeInAndOut(time);
        }

    
    
}
