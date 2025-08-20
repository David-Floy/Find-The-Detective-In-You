using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class KnockOut : MonoBehaviour
{
    public static KnockOut instance;
    
    [Header("Teleports")]
    public Transform player;
    public GameObject destinationKnockout1;
    public GameObject destinationKnockout2;
    public GameObject destinationKnockout3;

    private AudioClip hit;

    private int knockoutCount;
    private void Awake()
    { 
        instance = this;
        hit = Resources.Load<AudioClip>("Audio/hit");
    }

    public async void Knockout()
    {
        PlayerMovement.LockPlayer();
        knockoutCount++;
        Debug.Log(knockoutCount);
        if (knockoutCount < 4)
        {
            SoundManager.instance.PlaySound(hit);
            await Task.Delay(1500);
            SoundManager.instance.MuteEnv();
            FadeToBlack.instance.FadeInOut(4);
            await Task.Delay(3000);
            SoundManager.instance.UnMuteEnv();
            switch (RandomeNumber.GenerateNumber(4))
            {
                case 1:
                    destinationKnockout1.GetComponent<DialogueTrigger>().active = true;
                    player.position = destinationKnockout1.transform.position;
                    Debug.Log("1");
                    break;
                case 2:
                    destinationKnockout2.GetComponent<DialogueTrigger>().active = true;
                    player.position = destinationKnockout2.transform.position;
                    Debug.Log("2");
                    break;
                case 3:
                    destinationKnockout3.GetComponent<DialogueTrigger>().active = true;
                    player.position = destinationKnockout3.transform.position;
                    Debug.Log("3");
                    break;
                default:
                    destinationKnockout1.GetComponent<DialogueTrigger>().active = true;
                    player.position = destinationKnockout1.transform.position;
                    Debug.Log("1");
                    break;
            }
        }
        else
        {
            SoundManager.instance.PlaySound(hit);
            await Task.Delay(1500);
            GameState.GameOver("Du bist mehr als 3 Mal K.O. gegangen weshalb du nicht mehr in der Lage bist den Fall zu lösen. Game Over");
        }
        
        
        
    }
    
}
