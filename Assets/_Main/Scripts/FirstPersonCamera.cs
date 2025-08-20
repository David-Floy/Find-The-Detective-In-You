
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FirstPersonCamera : MonoBehaviour
{
    // New Input System
    private PlayerInputs controlls;
    private Vector2 mousLook;
    public Slider slider;
    
    
    // Start is called before the first frame update
    public float mouseSensitivity = 100f;
        
    // Transform from Camera
    public Transform orientation;

    public static bool lockCamera = false;
    
    float xRotation;
    float yRotation;
    
    //private bool lockedCursor = true;
    
    
    
    private void Awake()
    {
        controlls = new PlayerInputs();
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void OnEnable()
    {
        controlls.Enable();
    }

    private void OnDisable()
    {
        controlls.Disable();
    }
    
    public void ChangeMousSensi()
    {
        mouseSensitivity = slider.value;
    }
    
    private void Look()
    {
        // Get mouse inputs
        mousLook = controlls.Player.Look.ReadValue<Vector2>();
        
        // Get mouse x/y Axis from Vector2
        //float inputX = mousLook.x * Time.deltaTime * mouseSensitivity;
        //float inputY = mousLook.y * Time.deltaTime * mouseSensitivity;
        
        float inputX = mousLook.x  * mouseSensitivity;
        float inputY = mousLook.y  * mouseSensitivity;


        
        if (!lockCamera)
        {
            yRotation += inputX;

            xRotation -= inputY;
        
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Look();
    }
        //transform.localEulerAngles = Vector3.right * xRotation;
        //transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        //orientation.rotation = Quaternion.Euler(0, yRotation, 0);
       


    
}
