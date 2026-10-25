using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public string gameSceneName = "SampleScene";
    public AudioSource titleMusic;

    public void StartGame()
    {
        if (titleMusic != null) titleMusic.Stop();
        SceneManager.LoadScene(gameSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}