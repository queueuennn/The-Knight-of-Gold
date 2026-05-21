using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundsMenu : MonoBehaviour
{
    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }
    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
}