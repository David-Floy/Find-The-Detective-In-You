
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    
    public AudioSource playerSFX;
    public AudioSource envSFX;
    public AudioSource musicSFX;

    private float tempPlayerVolume;
    private float tempEnvVolume;
    private void Awake()
    {
        instance = this;
    }

    public void PlaySound(AudioClip clip)
    {
        playerSFX.clip = clip;
        playerSFX.Play();
    }
    
    public void MuteEnv()
    {
        while (envSFX.volume != 0)
        {
            envSFX.volume = Mathf.MoveTowards(envSFX.volume, 0, Time.deltaTime * 3);
        }
           
        
    }
    
    public void UnMuteEnv()
    {
        
        while (envSFX.volume != 1)
        {
            envSFX.volume = Mathf.MoveTowards(envSFX.volume, 1, Time.deltaTime * 3);
        }
            
        
    }
    
    
}
