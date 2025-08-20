using System;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;



public class Lupe : MonoBehaviour
{
    public GameObject camObject ;
    public Camera cam;
    public float zoom;
    private CanvasGroupController canvasGroupController;
    
    public bool InLupe = false;
    private int InCount;

    private float tempZoom;
    private float tempCamSens;
    private float tempFieldOfView;
    
    public static Lupe instance { get; private set; }
    
    public void Awake()
    {
        instance = this;
        
        canvasGroupController =
            new CanvasGroupController(this, gameObject.GetComponent<CanvasGroup>(), 4);
    }

    public void Update()
    {
        if (InLupe &&!PlayerMovement.lockMovement)
        {
            PlayerMovement.lockMovement = true;
            
        }
        
    }

    public void EnterLupe(int count)
    {
        if (!InLupe)
        {
            if (count != 0)
            {
                Utility.FindByTagName(TagArray.manager, "ChapterManager").GetComponent<ChapterMannager>().AdvanceStory(count);
                InCount = count;
            }
            
            InLupe = true;
            
            tempZoom = cam.focalLength;
            tempFieldOfView = cam.fieldOfView;
            canvasGroupController.Show();
            cam.focalLength = zoom;
            cam.fieldOfView = 15;
            tempCamSens = camObject.GetComponent<FirstPersonCamera>().mouseSensitivity;
            camObject.GetComponent<FirstPersonCamera>().mouseSensitivity = 0.005f;
        }
        


    }

    public void ExitLupe()
    {
        if (InLupe)
        {
            
            
            Utility.FindByTagName(TagArray.manager, "ChapterManager").GetComponent<ChapterMannager>().AdvanceStory(InCount);
                
            
            
            InLupe = false;
            //PlayerMovement.lockMovement = false;
            canvasGroupController.Hide();
            cam.focalLength = tempZoom;
            cam.fieldOfView = tempFieldOfView;
            camObject.GetComponent<FirstPersonCamera>().mouseSensitivity = tempCamSens;
        }
    }
}
