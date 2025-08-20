using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DIALOGUE.LogicalLines;



namespace DIALOGUE
{
    public class ConversationManager : MonoBehaviour
    {
        private DialogueSystem dialogueSystem => DialogueSystem.instance;
        private Coroutine _process = null;
        public bool IsRunning => _process != null;

        private readonly TextArchitect _architect = null;
        private bool _userPrompt = false;
        
        
        // Queue
        public Conversation conversation => (_conversationQueue.IsEmpty() ? null : _conversationQueue.top);

        private TagManager _tagManager;
        private LogicalLineManager _logicalLineManager;
        public int conversationProgress => (_conversationQueue.IsEmpty() ? -1 : _conversationQueue.top.GetProgress());
        private ConversationQueue _conversationQueue;

        public ConversationManager(TextArchitect architect)
        {
            this._architect = architect;
            dialogueSystem.onUserPrompt_Next += OnUserPrompt_Next;

            _tagManager = new TagManager();
            _logicalLineManager = new LogicalLineManager();
            _conversationQueue = new ConversationQueue();
        }

        public void Enqueue(Conversation conversation) => _conversationQueue.Enqueue(conversation);
        public void EnqueuePriority(Conversation conversation) => _conversationQueue.EnqueuePriority(conversation);

        private void OnUserPrompt_Next()
        {
            _userPrompt = true;
        }

        public Coroutine StartConversation(Conversation conversation)
        {
            StopConversation();
            dialogueSystem.dialogueContainer.Show();
            _conversationQueue.Clear();
            
            Enqueue(conversation);
            PlayerMovement.LockPlayer();

            _process = dialogueSystem.StartCoroutine(RunningConversation());
            
            return _process;
        }

        public void StopConversation()
        {
            if (!IsRunning)
            {
                return;
            }
            _conversationQueue.Clear();
            //GameObject.Find("RootDialogueBox").GetComponent<CanvasGroup>().alpha = 0;
            PlayerMovement.UnLockPlayer();
            dialogueSystem.dialogueContainer.Hide();
            dialogueSystem.StopCoroutine(_process);
            _process = null;
            

        }

        IEnumerator RunningConversation()
        {
            while (!_conversationQueue.IsEmpty())
            {
                Conversation currentConversation = conversation;

                if (currentConversation.HasReachedEnd())
                {
                    _conversationQueue.Dequeue();
                    continue;
                }
                
                string rawLine = conversation.CurrentLine();
                // Dont show any blank lines or try run logic on them 
                
                if (string.IsNullOrWhiteSpace(rawLine))
                {
                    TryAdvanceConversation(currentConversation);
                    continue;
                }

                DIALOGUE_LINE line = DialogueParser.Parse(rawLine);

                if (_logicalLineManager.TryGetLogic(line, out Coroutine logic))
                {
                    yield return logic;
                    TryAdvanceConversation(currentConversation);
                }
                else
                {
                    // Show dialogue 
                    if (line.hasDialogue)
                    {
                        yield return Line_RunDialogue(line);
                    }

                    if (line.hasCommands)
                    {
                        yield return Line_RunCommands(line);
                    }

                    // Wait for user Input if dialogue 
                    if (line.hasDialogue)
                    {
                        // Wait for user Input if next line has Dialogue!
                        yield return WaitForUserInput();
                    }

                    TryAdvanceConversation(currentConversation);
                }
            }

            _process = null;
            
            NPCInteractable.isRunning = false;
            PlayerMovement.UnLockPlayer();
        }

        private void TryAdvanceConversation(Conversation conversation)
        {
            conversation.IncrementProgress();

            if (conversation != _conversationQueue.top)
            {
                return;
            }
            
            if (conversation.HasReachedEnd())
            {
                _conversationQueue.Dequeue();
            }
        }
        
        IEnumerator Line_RunDialogue(DIALOGUE_LINE line)
        {

            // Show or hide speaker name if one is present.
            if (line.hasSpeaker)
            {
                dialogueSystem.ShowSpeakerName(_tagManager.Inject(line.speaker));
            }
            else
            {
                dialogueSystem.HideSpeakerName();
            }

            if (!dialogueSystem.dialogueContainer.isVisible)
            {
                dialogueSystem.dialogueContainer.Show();
            }

            // build dialogue
            yield return BuildDialogue(line.dialogue);
            
            
            
           
        }

        IEnumerator Line_RunCommands(DIALOGUE_LINE line)
        {
            List<DL_COMMAND_DATA.Command> commands = line.commandData.commands;

            foreach (DL_COMMAND_DATA.Command command in commands)
            {
                if (command.name == "wait")
                {
                    yield return CommandManager.instance.Execute(command.name, command.arguments);
                }
                else
                {
                    CommandManager.instance.Execute(command.name, command.arguments);
                }
            }
            yield return null;
        }

        IEnumerator BuildDialogue(string dialogue)
        {
            dialogue = _tagManager.Inject(dialogue);
            
            
            
            _architect.Build(dialogue);
            
            while (_architect.isBuilding)
            {
                if (_userPrompt)
                {
                    if (!_architect.hurryUp)
                    {
                        _architect.hurryUp = true;
                    }
                    else
                    {
                        _architect.ForceComplete();
                    }

                    _userPrompt = false;
                }

                yield return null; 
            }
        }

        IEnumerator WaitForUserInput()
        {
            dialogueSystem.promt.Show();
            
            
            
            while (!_userPrompt)
            {
                yield return null;
            }
            dialogueSystem.promt.Hide();
            _userPrompt = false;
        }
        

    }
}
