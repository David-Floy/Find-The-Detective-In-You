
using System.Collections.Generic;
using NOTE;
using UnityEngine;



public class NoteManager : MonoBehaviour
{
   private static List<string> notes = new List<string>();
   private static List<string> removedNotes = new List<string>();

   public static NoteManager instance { get; private set; }

   public NotebookContainer notebookContainer = new NotebookContainer();
   
   private static AudioClip penSfx;
   private static AudioClip penStrokeSfx;
 
   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
         notebookContainer.Initialize();
         penSfx = Resources.Load<AudioClip>("Audio/penSFX");
         penStrokeSfx = Resources.Load<AudioClip>("Audio/penStrokeSFX");
      }
      else
      {
         Destroy(gameObject);
      }

   }
   
   public static void Add(string text)
   {
      if (!notes.Contains(text) && !removedNotes.Contains(text))
      {
         
         notes.Add(text);
         SoundManager.instance.PlaySound(penSfx);
         
         instance.notebookContainer.noteText.text = "";
         foreach (string note in notes)
         {
            instance.notebookContainer.noteText.text += note + '\n' + '\n';
         }
      }
   }

   public static void Remove(string text)
   {
      if (notes.Contains(text))
      {
         removedNotes.Add(text);
         notes.Remove(text);
         SoundManager.instance.PlaySound(penStrokeSfx);
         
         instance.notebookContainer.noteText.text = "";
         foreach (string note in notes)
         {
            instance.notebookContainer.noteText.text += note + '\n' + '\n';
         }
      }
      else
      {
         Debug.Log($"{text} can't be found");
      }
      
   }

   public static void Replace(string noteToBeReplaced, string text)
   {
      if (notes.Contains(noteToBeReplaced))
      {
         int index = notes.IndexOf(noteToBeReplaced);
         notes.Insert(index, text);
         removedNotes.Add(noteToBeReplaced);
         notes.Remove(noteToBeReplaced);
         instance.notebookContainer.noteText.text = "";
         SoundManager.instance.PlaySound(penStrokeSfx);
         SoundManager.instance.PlaySound(penSfx);
         foreach (string note in notes)
         {
            instance.notebookContainer.noteText.text += note + '\n'+ '\n';
         }
      }
      else
      {
         Debug.Log($"{noteToBeReplaced} can't be found and replaced");
      }
   }

  
   
   
}
