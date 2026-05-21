using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundsource;
    private AudioSource musicsource;

    private void Awake()
    {
        soundsource = GetComponent<AudioSource>();
        musicsource = transform.GetChild(0).GetComponent<AudioSource>();

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
            
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        ChangeMusicVolume(0);
        ChangeSoundVolume(0);
    }

    public void PlaySound(AudioClip _sound)
    {
        soundsource.PlayOneShot(_sound);
    }
    public void ChangeSoundVolume(float _change)
    {
        ChangeSourceVolume(1, "SoundVolume", _change, soundsource);
    }
    public void ChangeMusicVolume(float _change)
    {
        ChangeSourceVolume(0.3f, "MusicVolume", _change, musicsource);
    }
    private void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource source)
    {
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
        currentVolume += change;

        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        float finalVolume = baseVolume * currentVolume;
        source.volume = currentVolume;
        
        PlayerPrefs.SetFloat(volumeName, currentVolume);
    }

    
}
