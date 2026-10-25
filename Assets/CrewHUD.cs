using UnityEngine;

public class CrewHUD : MonoBehaviour
{
    public Transform healthBar, thirstBar, hungerBar;
    public float leftEdgeX = -7f;     // world x of the bar's left edge
    public float fullWidth = 4f;      // world width at 100%
    public float barHeight = 0.3f;

    void Update()
    {
        var s = HabitatSim.Instance;
        SetBar(healthBar, s.Health / 100f, 4f);
        SetBar(thirstBar, s.Thirst / 100f, 3f);
        SetBar(hungerBar, s.Hunger / 100f, 2.5f);
    }

    void SetBar(Transform bar, float amount, float y)
    {
        if (bar == null) return;
        float w = Mathf.Max(0.001f, fullWidth * Mathf.Clamp01(amount));
        bar.localScale = new Vector3(w, barHeight, 1f);
        bar.position = new Vector3(leftEdgeX + w / 2f, y, 0f);
    }
}