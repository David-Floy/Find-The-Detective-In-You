using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace DIALOGUE
    {
        public class DialogueSystem : MonoBehaviour
        {
            public DialogueContainer dialogueContainer = new DialogueContainer();
            public ConversationManager conversationManager { get; private set; }
            private TextArchitect architect;

            public static DialogueSystem instance { get; private set; }

            public delegate void DialogueSystemEvent();

            public event DialogueSystemEvent onUserPrompt_Next;

            public bool isRunningConversation => conversationManager.IsRunning;

            public DialogueContinuePrompt promt;

            private void Awake()
            {
                if (instance == null)
                {
                   
                    instance = this;
                    Initialize();
                }

                else
                {
                    DestroyImmediate(gameObject);
                }
            }

            private bool _initialized = false;
            public void Initialize()
            {
                if (_initialized)
                {
                    return;
                }
                architect = new TextArchitect(dialogueContainer.dialogueText);
                conversationManager = new ConversationManager(architect);
                dialogueContainer.Initialize();
            }

            public void OnUserPrompt_Next()
            {
                onUserPrompt_Next?.Invoke();
            }
            

            public void ShowSpeakerName(string speakerName = "")
            {
                if (speakerName.ToLower() != "narrator")
                {
                    dialogueContainer.nameContainer.Show(speakerName);       
                }
                else
                {
                    HideSpeakerName();
                }
            }
            public void HideSpeakerName() => dialogueContainer.nameContainer.Hide();
            
                
            

            public Coroutine Say(string speaker, string dialogue)
            {
                List<string> conversation = new List<string>() { $"{speaker} \"{dialogue}\"" };
                return Say(conversation);
            }

            public Coroutine Say(List<string> lines)
            {
                Conversation conversation = new Conversation(lines);
                return conversationManager.StartConversation(conversation);
            }

            public Coroutine Say(Conversation conversation)
            {
                
                 return conversationManager.StartConversation(conversation);
            }
        }
    }