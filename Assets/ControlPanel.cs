using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    public GameObject panel;

    [Header("Bars (Image Type: Filled, Fill Method: Horizontal)")]
    public Image oxygenBar;
    public Image temperatureBar;
    public Image powerBar;
    public Image solarBar;
    public Image domeBar;
    public Image waterBar;
    public Image recyclerBar;
    public Image coatingBar;

    void Update()
    {
        var s = HabitatSim.Instance;
        bool inRoom = s.PlayerRoom == RoomId.Control;
        if (panel.activeSelf != inRoom) panel.SetActive(inRoom);
        if (!inRoom) return;

        oxygenBar.fillAmount = s.Oxygen / 100f;
        temperatureBar.fillAmount = s.Temperature / 100f;
        powerBar.fillAmount = s.Power / 100f;
        solarBar.fillAmount = s.SolarIntegrity / 100f;
        domeBar.fillAmount = s.DomeIntegrity / 100f;
        waterBar.fillAmount = s.Water / 100f;
        recyclerBar.fillAmount = s.RecyclerCondition / 100f;
        coatingBar.fillAmount = s.CoatingCondition / 100f;
    }
}
