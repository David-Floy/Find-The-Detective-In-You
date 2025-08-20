using System;
using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using NOTE;
using UnityEngine;



namespace COMMANDS
{
    public class CMD_DatabaseExtension_General : CMD_DatabaseExtension
    {
        private static readonly string[] PARAM_FILEPATH = new string[] { "-f", "-file", "-filepath" };
        private static readonly string[] PARAM_ENQUEUE = new string[] { "-e", "-enqueue" };

        private static readonly string[] PARAM_STATE = new[] { "-s", "-state" };
        private static readonly string[] PARAM_TYPE = new string[] { "-t", "-type"};
        
        private static readonly string[] PARAM_NAME = new string[] { "-n", "-name"};
        private static readonly string[] PARAM_NPC_PATH = new string[] { "-p", "-path"};
        
        private static readonly string[] PARAM_HINT = new string[] { "-h", "-hint"};
        private static readonly string[] PARAM_TIME = new string[] {"-time"};



        new public static void Extend(CommandDatabase database)
        {
            
            // Notebook commands
            database.AddCommand("ShowDB", new Func<IEnumerator>(ShowDialogueBox));
            database.AddCommand("HideDB", new Func<IEnumerator>(HideDialogueBox));
            database.AddCommand("AddNote", new Action<string>(AddNote));
            database.AddCommand("RemoveNote", new Action<string>(RemoveNote));
            database.AddCommand("ReplaceNote", new Action<string[]>(ReplaceNote));
            database.AddCommand("ShowNotebook", new Action (ShowNote));
            database.AddCommand("HideNotebook", new Action (HideNote));
            
            // delay Dialogue
            database.AddCommand("wait", new Func<string, IEnumerator>(Wait));
            
            // load new path to Npc Dialogue
            database.AddCommand("DialogueNPC", new Action<string[]>(LoadToNpc));
            
            // loads new Dialogue file in current Dialogue 
            database.AddCommand("load", new Action<string[]>(LoadNewDialogueFile));
            
            //Activates and Deactivates an Obj.  
            database.AddCommand("activateObj", new Action<string>(ActivateObject));
            database.AddCommand("deactivateObj", new Action<string>(DeactivateObject));

            // can activate some Components in Gameobjects 
            database.AddCommand("setObjState", new Action<string[]>(SetStateOfObject));
            
            // shows a hint on screen
            database.AddCommand("showHintFor", new Action<string[]>(ShowHint));
            
            // Closes the Game
            database.AddCommand("gameOver", new Action<string>(GameOver));
            
            // Advances the story
            database.AddCommand("advanceStory", new Action<string>(AdvanceStory));
            database.AddCommand("advanceEnd", new Action<string>(AdvanceEnd));
            
            // locks and unlocks Player Movement
            
            database.AddCommand("lockMovement", new Action(LockPlayerMovement));
            database.AddCommand("unlockMovement", new Action(UnlockPlayerMovement));
            database.AddCommand("unlockMovementCamera", new Action(UnlockPlayerMovementAndCamera));
            
            
            database.AddCommand("lockMovementCursor", new Action(LockPlayerMoveAndCameraCursorVis));
            
            
            database.AddCommand("enterLupe", new Action<string>(EnterLupenMode));
            database.AddCommand("exitLupe", new Action(ExitLupenMode));
            
            database.AddCommand("playSound", new Action<string>(PlaySound));
            
            database.AddCommand("fadeToSet", new Action(FadeToSet));
            
            
            
            database.AddCommand("fadeOutBlack", new Action(FadeOutBlack));
            database.AddCommand("fadeToBlack", new Action(FadeToBlack));
            database.AddCommand("fadeInOut", new Action<string>(FadeInOut));
            database.AddCommand("knockout", new Action(Knockout));
            
            database.AddCommand("turnOnFlashlight", new Action(TurnOnFlashlight));
            database.AddCommand("turnOffFlashlight", new Action(TurnOffFlashlight));
            
            database.AddCommand("showGameOver", new Action(TextGameOver));
            database.AddCommand("startHelp", new Action(StartTimer));
            database.AddCommand("stopHelp", new Action(StopTimer));
        }

        private static void StartTimer() => Help.instance.StartTimer();
        
        private static void StopTimer() => Help.instance.StopTimer();
        
        private static void ShowNote() => NoteManager.instance.notebookContainer.Show();
        private static void HideNote() => NoteManager.instance.notebookContainer.Hide();

        private static void TurnOnFlashlight() => Flashlight.instance.TurnOn();
        private static void TurnOffFlashlight() => Flashlight.instance.TurnOff();
        private static void Knockout() => KnockOut.instance.Knockout();
        
        private static void TextGameOver()
        {
            GameObject.Find("GameOverText").GetComponent<CanvasGroup>().alpha = 1;
        }

        private static void PlaySound(string clip) => SoundManager.instance.PlaySound(Resources.Load<AudioClip>(clip));


        private static void FadeInOut(string data) => global::FadeToBlack.instance.FadeInOut(float.Parse(data));
        private static void FadeToBlack()
        {
           global::FadeToBlack.instance.Show();
        }
        private static void FadeOutBlack()
        {
            global::FadeToBlack.instance.Hide();
        }

        private static void FadeToSet()
        {
            global::FadeToBlack.instance.ShowToSet(0.5f);

        } 

        private static void EnterLupenMode(string data)
        {
            Lupe.instance.EnterLupe(int.Parse(data));
        }
        private static void ExitLupenMode()
        {
            Lupe.instance.ExitLupe();
        }
        
        private static void LockPlayerMoveAndCameraCursorVis()
        {
            PlayerMovement.LockPlayerCursorVisable();
        }

        private static void LockPlayerMovement()
        {
            PlayerMovement.lockMovement = true;
            Debug.Log("PlayerLocked");
        }
        private static void UnlockPlayerMovement()
        {
            PlayerMovement.lockMovement = false;
        }
        private static void UnlockPlayerMovementAndCamera()
        {
            PlayerMovement.UnLockPlayer();
        }

        private static void AdvanceStory(string count)
        {
            Utility.FindByTagName(TagArray.manager, "ChapterManager").GetComponent<ChapterMannager>().AdvanceStory(Int32.Parse(count));;
        }
        
        private static void AdvanceEnd(string count)
        {
            Utility.FindByTagName(TagArray.manager, "EndSequence").GetComponent<EndSequence>().CountControl(Int32.Parse(count));
        }
        
        private static void GameOver(string text) => GameState.GameOver(text);
        
        
        private static void ShowHint(string[] data)
        {
            string hint = String.Empty;
            int time = 5000;
        
            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(PARAM_HINT, out hint);
            
            parameters.TryGetValue(PARAM_TIME, out time);
            
            HintContainer.HintBox.ShowHintFor(hint, time);

        }


        private static void SetStateOfObject(string[] data)
        {
            string name = String.Empty;
            string typeOfObject = String.Empty;
            bool state = false;
        
            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(PARAM_NAME, out name);
            parameters.TryGetValue(PARAM_TYPE, out typeOfObject);
            parameters.TryGetValue(PARAM_STATE, out state);

            if (typeOfObject == "tele")
            {
                try
                {
                    Utility.FindByTagName(TagArray.trigger, name).GetComponent<Teleporter>().Aktivate(state);
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("GameObject not found");
                }
            }
            else if (typeOfObject == "item")
            {
               ItemList.SetItemState(name, state);
            }
            else if (typeOfObject == "hint")
            {
                try
                {
                    Utility.FindByTagName(TagArray.trigger, name).GetComponent<HintTrigger>().active = state;
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("GameObject not found");
                }

            }
            else if (typeOfObject == "dia")
            {
                try
                {
                    Utility.FindByTagName(TagArray.trigger, name).GetComponent<DialogueTrigger>().active = state;
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("GameObject not found");
                }
            }
            else
            {
                Debug.Log($"'{typeOfObject}' can't be found by CommandSystem!");
            }
        }
        
        private static void ActivateObject(string name)
        {
            try
            {
                GameObject.Find(name).SetActive(true);
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("GameObject not found");
                
            }
        }
        
        private static void DeactivateObject(string name)
        {
            
            try
            {
                GameObject.Find(name).SetActive(false);
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("GameObject not found");
                
            }
        }
        
        
        private static void LoadToNpc(string[] data)
        {
            string name = String.Empty;
            string path = String.Empty;
        
            var parameters = ConvertDataToParameters(data);
            
            parameters.TryGetValue(PARAM_NAME, out name);
            parameters.TryGetValue(PARAM_NPC_PATH, out path);
            
            try
            {
                Utility.FindByTagName(GameObject.FindGameObjectsWithTag("Selectable"), name).GetComponent<NPCInteractable>().path = path;
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("GameObject not found");
            }
            
       
        }
        
        
        public static void LoadNewDialogueFile(string[] data)
        {
            string fileName = string.Empty;
            bool enqueue = false;
            
            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(PARAM_FILEPATH, out fileName);
            parameters.TryGetValue(PARAM_ENQUEUE, out enqueue, defaultValue: false);

            string filePath = FilePaths.GetPathToResource(FilePaths.resources_dialogueFiles, fileName);
            TextAsset file = Resources.Load<TextAsset>(filePath);

            if (file == null)
            {
                Debug.LogWarning($"File '{filePath}' could not be loaded from dialogue files. Please make sure it exists within the '{FilePaths.resources_dialogueFiles}' resources folder.");
               return;
            }
            
            List<string> lines = FileManager.ReadTextAsset(file, includeBlankLines: true);
           Conversation newConversation = new Conversation(lines);

            if (enqueue)
            {
                DialogueSystem.instance.conversationManager.Enqueue(newConversation);
            }
            else
            {
                DialogueSystem.instance.conversationManager.StartConversation(newConversation);
            }
        
        }

        private static IEnumerator ShowDialogueBox()
        {
            yield return DialogueSystem.instance.dialogueContainer.Show();
        }

        private static IEnumerator HideDialogueBox()
        {
            yield return DialogueSystem.instance.dialogueContainer.Hide();
        }

        private static void AddNote(string text)
        {
            NoteManager.Add(text);
        }

        private static void RemoveNote(string text)
        {
            NoteManager.Remove(text);
        }

        private static void ReplaceNote(string[] text)
        {
            NoteManager.Replace(text[0], text[1]);
        }

        private static IEnumerator Wait(string data)
        {
            if (float.TryParse(data, out float time))
            {
                yield return new WaitForSeconds(time);
            }
        }
        
    }
}
