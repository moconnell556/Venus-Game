using UnityEngine;
using UnityEngine.UI;

public class StatsHUD : MonoBehaviour
{
    [Header("Bars (Image Type: Filled, Fill Method: Horizontal)")]
    public Image healthBar;
    public Image thirstBar;
    public Image hungerBar;

    void Update()
    {
        var s = HabitatSim.Instance;
        healthBar.fillAmount = s.Health / 100f;
        thirstBar.fillAmount = s.Thirst / 100f;
        hungerBar.fillAmount = s.Hunger / 100f;
    }
}
