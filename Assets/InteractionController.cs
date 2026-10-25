using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    [Header("Progress UI")]
    public GameObject progressRoot;
    public Image progressFill;
    public TextMeshProUGUI progressLabel;

    private HabitatSim sim;
    private Camera cam;
    private Interactable current;
    private float elapsed;

    void Start()
    {
        sim = HabitatSim.Instance;
        cam = Camera.main;
        SetProgressVisible(false);
    }

    void Update()
    {
        if (sim.Status != GameStatus.Running) return;

        if (current != null)
        {
            if (sim.PlayerRoom != current.room)
            {
                CancelTask();
                return;
            }

            elapsed += Time.deltaTime;
            if (elapsed >= current.duration)
            {
                sim.Perform(current.action);
                EndTask();
            }
            else
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

            if (sim.PlayerRoom != item.room)
            {
                Debug.Log($"Go to the {item.room} room first.");
                return;
            }

            if (item.duration <= 0f)
            {
                if (!sim.Perform(item.action))
                    Debug.Log($"Not enough resources for {item.label}.");
            }
            else
            {
                current = item;
                elapsed = 0f;
                progressLabel.text = item.label;
                SetProgressVisible(true);
            }
            return;
        }
    }

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
