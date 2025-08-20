using System;
using System.Collections;
using System.Collections.Generic;
using COMMANDS;
using DIALOGUE;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class Tresor : MonoBehaviour
{
    protected static string Code;
    
    private static GameObject doorOpen;
    private static GameObject doorClose;
    
    [Header("BooksMain")]
    public TextMeshPro Book1;
    public TextMeshPro Book2;
    public TextMeshPro Book3;
    public TextMeshPro Book4;
    public TextMeshPro Book5;
    public TextMeshPro Book6;

    public static AudioClip opened;
    private static AudioClip closed;
    private void Awake()
    {
        Code = RandomeNumber.GenerateNumber(10).ToString() + RandomeNumber.GenerateNumber(10).ToString() + RandomeNumber.GenerateNumber(10).ToString() + RandomeNumber.GenerateNumber(10).ToString() +RandomeNumber.GenerateNumber(10).ToString() +RandomeNumber.GenerateNumber(10).ToString();
        Debug.Log(Code);
        doorOpen = GameObject.Find("DoorOpen");
        doorClose = GameObject.Find("DoorClosed");
        closed = Resources.Load<AudioClip>("Audio/");
        opened = Resources.Load<AudioClip>("Audio/");
        SetBookNumbers();
    }

    public static void CheckCode(string userCode)
    {
        if (userCode == Code)
        {
            Debug.Log("Code ist richtig");
            SoundManager.instance.PlaySound(opened);
            Utility.FindByTagName(TagArray.manager, "ChapterManager").GetComponent<ChapterMannager>().AdvanceStory(16);
            Utility.FindByTagName(GameObject.FindGameObjectsWithTag("Selectable"), "Tresor").GetComponent<NPCInteractable>().path = "Kapitel-6/Room/Tresor3";

            string[] data = new[] { "-f","Kapitel-6/Room/Tresor3"};
            
            CMD_DatabaseExtension_General.LoadNewDialogueFile(data);
        }
        else
        {
            SoundManager.instance.PlaySound(closed);
        }
        
    }

    private void SetBookNumbers()
    {
        char[] codeArray = Code.ToCharArray();
        Book1.text = codeArray[0].ToString();
        Book2.text = codeArray[1].ToString();
        Book3.text = codeArray[2].ToString();
        Book4.text = codeArray[3].ToString();
        Book5.text = codeArray[4].ToString();
        Book6.text = codeArray[5].ToString();
    }
}
