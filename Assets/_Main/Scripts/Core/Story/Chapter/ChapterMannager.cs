using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChapterMannager : MonoBehaviour
{
  public static int chapterCounter = 0;
  
  [Header("Chapter")]
  public GameObject Chapter1;
  public GameObject Chapter2;
  public GameObject Chapter4;
  
  
  
  [Header("ImagesNotebook")]
  public CanvasGroup Image1;
  public CanvasGroup Image2;
  public CanvasGroup Image3;
  public CanvasGroup Image4;
  public CanvasGroup Image5;
  public CanvasGroup Image6;
  public CanvasGroup Image7;
  public CanvasGroup Image8;
  
  
  [Header("Hints")]
  public GameObject Hint1;
  public GameObject Hint2;
  public GameObject Hint3;
  public GameObject Hint4;
  public GameObject Hint5;
  
  public GameObject Hint6;
  public GameObject Hint7;
  public GameObject Hint8;
  
  
  [Header("Box")]
  public GameObject Box1;
  public GameObject Box2;
  public GameObject Box3;
  public GameObject Box4;
  public GameObject Box5;
  public GameObject Box6;
  public GameObject Box7;

  [Header("Fischer")] 
  public GameObject Fischer1;
  public GameObject Fischer2;

  [Header("Chapter 6")] 
  public GameObject AliceOutside;
  public GameObject BriefkastenDummy;
  public GameObject Briefkasten;
  public GameObject Owen;
  
  [Header("Chapter 7")] 
  public GameObject MissTessa;
  public GameObject MissTessaChapter2;

  [Header("Chapter 8")] 
  public GameObject policeBorder;

  public GameObject owenOffice;
  public GameObject owenOffice2;
  public GameObject Player;
  public Transform destination;
  public Transform destination2;

  public GameObject qrCode;

  public void AdvanceStory(int count)
  {
    Debug.Log($"advance to Chapter: {count}");
    chapterCounter = count;
    ChapterControl();
  }
  
  private void ChapterControl()
  {
    switch (chapterCounter)
    {
      case 1:
        Utility.FindByTagName(TagArray.trigger,"Trigger-Office").GetComponent<DialogueTrigger>().active = false;
        Utility.FindByTagName(TagArray.trigger,"Trigger-Office").GetComponent<Teleporter>().active = true;
        NoteManager.Remove("Detektivausrüstung aus meinem Büro holen");
        NoteManager.Add("Den Tatort nach Indizien untersuchen");
        NoteManager.Add("Zum Anwesen des Ehepaars Ashcroft gehen");
        AdvanceStory(2);
        break;
      case 2:
        Chapter1.SetActive(false);
        Chapter2.SetActive(true);
        break;
      case 3:
        Hint1.SetActive(true);
        Hint2.SetActive(true);
        break;
      case 4:
        CanvasConrol.Show(Image1);
        break;
      case 5:
        CanvasConrol.Show(Image2);
        CanvasConrol.Show(Image3);
        break;
      case 6:
        CanvasConrol.Show(Image4);
        break;
      case 7:
        
        break;
      case 8:
        Box1.GetComponent<Outline>().enabled = true;
        Box2.GetComponent<Outline>().enabled = true;
        Box3.GetComponent<Outline>().enabled = true;
        Box4.GetComponent<Outline>().enabled = true;
        Box5.GetComponent<Outline>().enabled = true;
        Box6.GetComponent<Outline>().enabled = true;
        Box7.GetComponent<Outline>().enabled = true;
        switch (RandomeNumber.GenerateNumber(7))
        {
          case 1:
            Box1.GetComponent<NPCInteractable>().path = "Kapitel-4/Box/BoxFull";
            break;
          case 2:
            Box2.GetComponent<NPCInteractable>().path = "Kapitel-4/Box/BoxFull";
            break;
          case 3:
            Box3.GetComponent<NPCInteractable>().path = "Kapitel-4/Box/BoxFull";
            break;
          case 4:
            Box4.GetComponent<NPCInteractable>().path = "Kapitel-4/Box/BoxFull";
            break;
          case 5:
            Box5.GetComponent<NPCInteractable>().path = "Kapitel-4/Box/BoxFull";
            break;
          case 6:
            Box6.GetComponent<NPCInteractable>().path = "Kapitel-4/Box/BoxFull";
            break;
          case 7:
            Box7.GetComponent<NPCInteractable>().path = "Kapitel-4/Box/BoxFull";
            break;
        }
        break;
      case 9:
        if (!Hint3.activeSelf)
        {
          Hint3.SetActive(true);
        }
        else
        {
          Hint3.SetActive(false);
        }
        break;
      case 10:
        Fischer1.SetActive(false);
        break;
      case 11:
        CanvasConrol.Show(Image5);
        Hint4.SetActive(false);
        break;
      case 12:
        CanvasConrol.Show(Image6);
        Hint4.SetActive(false);
        break;
      case 13:
        Box1.GetComponent<Outline>().enabled = false;
        Box2.GetComponent<Outline>().enabled = false;
        Box3.GetComponent<Outline>().enabled = false;
        Box4.GetComponent<Outline>().enabled = false;
        Box5.GetComponent<Outline>().enabled = false;
        Box6.GetComponent<Outline>().enabled = false;
        Box7.GetComponent<Outline>().enabled = false;
        break;
      case 14:
        Chapter4.SetActive(false);
        break;
      case 15:
        CanvasConrol.Show(Image7);
        Hint5.SetActive(false);
        break;
      case 16:
        Hint6.SetActive(false);
        Hint7.SetActive(true);
        break;
      case 17:
        AliceOutside.SetActive(false);
        break;
      case 18:
        MissTessa.SetActive(true);
        MissTessaChapter2.SetActive(false);
        break;
      case 19:
        CanvasConrol.Show(Image8);
        break;
      case 20:
        policeBorder.SetActive(false);
        break;
      case 21:
        BriefkastenDummy.SetActive(false);
        Briefkasten.SetActive(true);
        break;
      case 22:
        owenOffice.SetActive(false);
        break;
      case 23:
        owenOffice2.SetActive(true);
        break;
      case 24:
        Player.transform.position = destination.position;
        break;
      case 25:
        Player.transform.position = destination2.position;
        break;
      case 26:
        Hint8.SetActive(false);
        break;
      case 27:
        Hint8.SetActive(true);
        break;
      case 28:
        Chapter2.SetActive(false);
        break;
      case 29:
        MissTessa.GetComponent<NPCInteractable>().path = "Kapitel-7/Tessa1";
        break;
      case 30:
        Owen.SetActive(false);
        break;
      case 31:
        qrCode.SetActive(true);
      break;
    }
    
    
  }
    
}
