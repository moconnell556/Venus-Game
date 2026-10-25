using UnityEngine;

public class RoomZone : MonoBehaviour
{
    public RoomId arrivalRoomId;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            HabitatSim.Instance.PlayerRoom = arrivalRoomId;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && HabitatSim.Instance.PlayerRoom == arrivalRoomId)
            HabitatSim.Instance.PlayerRoom = null;
    }
}
