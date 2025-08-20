using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandTesting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Running());

    }

    IEnumerator Running()
    {
        yield return CommandManager.instance.Execute("print");
        yield return CommandManager.instance.Execute("print_lp", "Hello, you are using my command");
        yield return CommandManager.instance.Execute("print_mp", "Liine 1", "HELLO", "Hello2");
        
        
        yield return CommandManager.instance.Execute("lambda");
        yield return CommandManager.instance.Execute("lambda_lp", "Hello lambda");
        yield return CommandManager.instance.Execute("lambda_mp", "test1", "test2", "test3" );
        
        yield return CommandManager.instance.Execute("process");
        yield return CommandManager.instance.Execute("process_1p", "3");
        yield return CommandManager.instance.Execute("process_mp", "process L1", "process L2", "process L3" );

    }
}
