using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject lostPanel;
    public GameObject wonPanel;

    void Update()
    {
        var status = HabitatSim.Instance.Status;
        bool lost = status == GameStatus.Lost;
        bool won = status == GameStatus.Won;

        if (lostPanel.activeSelf != lost) lostPanel.SetActive(lost);
        if (wonPanel.activeSelf != won) wonPanel.SetActive(won);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
