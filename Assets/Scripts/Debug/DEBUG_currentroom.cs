using UnityEngine;
using TMPro;

public class DEBUG_currentroom : MonoBehaviour
{

    public TextMeshProUGUI currentRoomText;
    public RoomEntityCount roomEntityCount;

    public PlayerCurrentRoom player;

    void Update()
    {
        if (player != null && currentRoomText != null)
        {
            currentRoomText.text = "Current Room: " + player.currentRoomName;
        }
    }
}
