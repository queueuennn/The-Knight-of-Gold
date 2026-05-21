using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    void Update()
    {
        SceneManager.LoadScene(1);
    }
}
