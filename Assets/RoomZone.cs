using UnityEngine;

public class RoomZone : MonoBehaviour
{
    public RoomId room;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            HabitatSim.Instance.PlayerRoom = room;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && HabitatSim.Instance.PlayerRoom == room)
            HabitatSim.Instance.PlayerRoom = null;
    }
}
