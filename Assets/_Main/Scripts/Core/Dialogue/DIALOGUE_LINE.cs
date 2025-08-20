using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace DIALOGUE
    {
        public class DIALOGUE_LINE
        {
            public string rawData { get; private set; } = string.Empty;
            public string speaker;
            public string dialogue;
            public DL_COMMAND_DATA commandData;

            public bool hasSpeaker => speaker != string.Empty;
            public bool hasDialogue => dialogue != string.Empty;
            public bool hasCommands => commandData != null;

            public DIALOGUE_LINE(string speaker, string dialogue, string commands)
            {
                this.speaker = speaker;
                this.dialogue = dialogue;
                this.commandData = (string.IsNullOrEmpty(commands) ? null : new DL_COMMAND_DATA(commands));
            }
        }
    }