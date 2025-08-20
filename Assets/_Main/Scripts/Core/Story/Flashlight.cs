using System;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public static Flashlight instance;

    public GameObject light;
    private bool isOn;

    public AudioClip lightSwitch;
    private void Awake()
    {
        instance = this;
    }
    

    public void TurnOn()
    {
        light.SetActive(true);
        SoundManager.instance.PlaySound(lightSwitch);
    }
    
    public void TurnOff()
    {
        light.SetActive(false);
        SoundManager.instance.PlaySound(lightSwitch);
    }
}
