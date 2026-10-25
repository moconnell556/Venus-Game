using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    [Header("Demo")]
    public bool ignoreRooms = true;

    [Header("Progress UI")]
    public GameObject progressRoot;
    public Image progressFill;
    public TextMeshProUGUI progressLabel;

    [Header("Feedback")]
    public TextMeshProUGUI feedbackText;
    public float feedbackSeconds = 2.5f;

    private HabitatSim sim;
    private Camera cam;
    private Interactable current;
    private float elapsed;
    private float feedbackTimer;

    void Start()
    {
        sim = HabitatSim.Instance;
        cam = Camera.main;
        SetProgressVisible(false);
        if (feedbackText != null) feedbackText.text = "";
    }

    void Update()
    {
        if (feedbackTimer > 0f)
        {
            feedbackTimer -= Time.deltaTime;
            if (feedbackTimer <= 0f && feedbackText != null) feedbackText.text = "";
        }

        if (sim.Status != GameStatus.Running) return;

        if (current != null)
        {
            if (!ignoreRooms && sim.PlayerRoom != current.room)
            {
                CancelTask();
                return;
            }

            elapsed += Time.deltaTime;
            if (elapsed >= current.duration)
            {
                PerformAndReport(current);
                EndTask();
            }
            else if (progressFill != null)
            {
                progressFill.fillAmount = elapsed / current.duration;
            }
            return;
        }

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            TryClick();
    }

    void TryClick()
    {
        Vector2 screen = Mouse.current.position.ReadValue();
        Vector2 world = cam.ScreenToWorldPoint(
            new Vector3(screen.x, screen.y, -cam.transform.position.z));

        foreach (var hit in Physics2D.OverlapPointAll(world))
        {
            if (!hit.TryGetComponent(out Interactable item)) continue;

            if (!ignoreRooms && sim.PlayerRoom != item.room)
            {
                Debug.Log($"Go to the {item.room} room first.");
                return;
            }

            if (item.duration <= 0f)
            {
                PerformAndReport(item);
            }
            else
            {
                current = item;
                elapsed = 0f;
                if (progressLabel != null) progressLabel.text = item.label;
                SetProgressVisible(true);
            }
            return;
        }
    }

    // Runs the action and shows what changed, e.g. "Cleaning panels: Solar panels 96 to 100"
    bool PerformAndReport(Interactable item)
    {
        var (name, before) = Read(item.action);
        bool ok = sim.Perform(item.action);
        var (_, after) = Read(item.action);

        if (feedbackText != null)
        {
            feedbackText.text = ok
                ? $"{item.label}: {name} {before:F0} to {after:F0}"
                : $"Not enough resources for {item.label}";
            feedbackTimer = feedbackSeconds;
        }
        return ok;
    }

    (string name, float value) Read(ActionType action) => action switch
    {
        ActionType.CleanPanels      => ("Solar panels", sim.SolarIntegrity),
        ActionType.ReplaceCover     => ("Solar panels", sim.SolarIntegrity),
        ActionType.Electrolyze      => ("Oxygen", sim.Oxygen),
        ActionType.HeatPump         => ("Temperature", sim.Temperature),
        ActionType.Hydrate          => ("Thirst", sim.Thirst),
        ActionType.Harvest          => ("Hunger", sim.Hunger),
        ActionType.MaintainRecycler => ("Recycler", sim.RecyclerCondition),
        ActionType.RepairDome       => ("Coating", sim.CoatingCondition),
        _                           => ("", 0f)
    };

    void EndTask()
    {
        current = null;
        SetProgressVisible(false);
    }

    void CancelTask()
    {
        Debug.Log("Task cancelled: you left the room.");
        EndTask();
    }

    void SetProgressVisible(bool visible)
    {
        if (progressRoot != null) progressRoot.SetActive(visible);
    }
}
