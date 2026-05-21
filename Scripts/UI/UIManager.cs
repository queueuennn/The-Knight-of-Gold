using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject finishScreen;
    [SerializeField] private AudioClip finishSound;


    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        finishScreen.SetActive(false);
    }

    #region Game Over Functions
    //Game over function
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    //Restart level
    public void Restart()
    {
        Time.timeScale = 1; // Ensure time scale is reset in case we were paused
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Activate game over screen
    public void MainMenu()
    {
        Time.timeScale = 1; // Ensure time scale is reset in case we were paused
        SceneManager.LoadScene(0);
    }

    //Quit game/exit play mode if in Editor
    public void Quit()
    {
        Application.Quit(); //Quits the game (only works in build)


        //UnityEditor.EditorApplication.isPlaying = false; //Exits play mode
    }
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeInHierarchy)
            {
                PauseGame(false);
            }
            else
            {
                PauseGame(true);
            }
        }
    }

    public void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);

        if (status)
        {
            Time.timeScale = 0; // Pause the game
        }
        else
        {
            Time.timeScale = 1; // Resume the game
        }
    }

    #region Finish Functions
    public void FinishLevel()
    {
        finishScreen.SetActive(true);

        if (SoundManager.instance != null)
            SoundManager.instance.PlaySound(finishSound);

        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    #endregion
   

    

    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }
    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
}