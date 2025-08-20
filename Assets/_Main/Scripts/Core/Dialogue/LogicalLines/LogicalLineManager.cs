using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;


namespace DIALOGUE.LogicalLines
{


    public class LogicalLineManager
    {
        private DialogueSystem _dialogueSystem = DialogueSystem.instance;
        private List<ILogicalLine> _logicalLines = new List<ILogicalLine>();

        public LogicalLineManager() => LoadLogicalLines();

        private void LoadLogicalLines()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] lineTypes = assembly.GetTypes()
                .Where(t => typeof(ILogicalLine).IsAssignableFrom(t) && !t.IsInterface).ToArray();

            foreach (var lineType in lineTypes)
            {
                ILogicalLine line = (ILogicalLine)Activator.CreateInstance(lineType);
                _logicalLines.Add(line);
            }
            
        }


        public bool TryGetLogic(DIALOGUE_LINE line, out Coroutine logic)
        {
            foreach (var logicalLine in _logicalLines)
            {
                if (logicalLine.Matches(line))
                {
                    logic = _dialogueSystem.StartCoroutine(logicalLine.Execute(line));
                    return true;
                }
            }


            logic = null;
            return false;
        }
        
        
    }
}
