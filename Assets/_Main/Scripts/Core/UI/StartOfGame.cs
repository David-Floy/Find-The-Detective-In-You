using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartOfGame : MonoBehaviour
{
   
   public Transform destination;

   public Transform player;
   public CanvasGroup startScreen;
   public AudioSource startSfxSrc;
   public AudioSource playerSfxSrc;
   public AudioClip scream;


   public void Start()
   {
      PlayerMovement.LockPlayerCursorVisable();
      PlayerInteractable.InStartMenu = true;
   }

  

   public void StartGame()
   {
      playerSfxSrc.clip = scream;
      playerSfxSrc.Play();
      player.position = destination.position;
      PlayerMovement.UnLockPlayer();
      CanvasConrol.Hide(startScreen);
      PlayerInteractable.InStartMenu = false;
      startSfxSrc.Stop();
      
      
   }
}
