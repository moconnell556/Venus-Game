using UnityEngine;

public class Interactable : MonoBehaviour
{
    public RoomId room;
    public ActionType action;
    public string label = "Working...";

    [Tooltip("Seconds the player must stay in the room. 0 = instant.")]
    public float duration = 0f;
}
