using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class GameState 
{

    public async static void GameOver(string message = "Game Over")
    {
        
        GameObject text = GameObject.Find("GameOverText");
        FadeToBlack.instance.Show();
        
        text.GetComponent<TextMeshProUGUI>().text = message;
        text.GetComponent<CanvasGroup>().alpha = 1;
        SoundManager.instance.MuteEnv();
        SoundManager.instance.PlaySound(Resources.Load<AudioClip>("Audio/EndMusic"));
        PlayerMovement.LockPlayerCursorVisable();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        await Task.Delay(120000);
        Application.Quit();
        
    }

}
