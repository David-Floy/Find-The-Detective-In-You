using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;
using Toggle = UnityEngine.UI.Toggle;

public class Menue : MonoBehaviour
{
    public CanvasGroup SettingsMenue;
    public CanvasGroup StartMenue;
    public CanvasGroup options;

    public Slider slider;
    public AudioMixer Mastermixer;
    public Toggle VSToggle;

    public TextMeshProUGUI statusDisplay;

    public void BackToGame()
    {
        CanvasConrol.Hide(SettingsMenue);
        PlayerMovement.UnLockPlayer();
        PlayerInteractable.inMenu = false;
    }

    public void ShowOptions()
    {
        CanvasConrol.Hide(SettingsMenue);
        if (PlayerInteractable.InStartMenu)
        {
            CanvasConrol.Hide(StartMenue);
        }
        CanvasConrol.Show(options);
    }

    public void HideOptions()
    {
        CanvasConrol.Hide(options);
        if (!PlayerInteractable.InStartMenu)
        {
            CanvasConrol.Show(SettingsMenue);
        }
        else
        {
            CanvasConrol.Show(StartMenue);
        }
       
    }

    public void SetGraphicsUp()
    {
      QualitySettings.IncreaseLevel();
      SetGraphicStatus(QualitySettings.GetQualityLevel());


    }
    public void SetGraphicsDown()
    {
       QualitySettings.DecreaseLevel();
       SetGraphicStatus(QualitySettings.GetQualityLevel());
    }

    public void SetGraphicStatus(int count)
    {
        switch (count)
        {
            case 0:
                statusDisplay.text = "Gering";
                break;
            case 1:
                statusDisplay.text = "Mittel";
                break;
            case 2:
                statusDisplay.text = "Hoch";
                break;
            case 3:
                statusDisplay.text = "Sehr Hoch";
                break;
            case 4:
                statusDisplay.text = "Ultra";
                break;
            default:
                statusDisplay.text = "Ultra";
                break;
        }
    }

    public void SetVolume()
    {
        
        
        
        Mastermixer.SetFloat("Master", Mathf.Log10( slider.value /100 )* 20f);

    }

    public void ToggleVsync()
    {
        if (VSToggle.isOn)
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            Application.targetFrameRate = -1;
            QualitySettings.vSyncCount = 0; 
        }
    }


}
