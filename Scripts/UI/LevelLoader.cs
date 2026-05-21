using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadLevel(int levelIndex)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(levelIndex);
    }
}