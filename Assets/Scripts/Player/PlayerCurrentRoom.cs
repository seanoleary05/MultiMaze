using UnityEngine;
public class PlayerCurrentRoom : MonoBehaviour
{
    public string currentRoomName;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RoomArena"))
        {
            currentRoomName = other.transform.parent.name;
            Debug.Log("Entered Room: " + currentRoomName);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RoomArena"))
        {
            // only clear it if we haven't already entered the next room
            if (currentRoomName == other.transform.parent.name)
                currentRoomName = "";

            Debug.Log("Exited Room: " + other.transform.parent.name);
        }
    }
}