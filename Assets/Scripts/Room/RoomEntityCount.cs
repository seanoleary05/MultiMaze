using UnityEngine;

public class RoomEntityCount : MonoBehaviour
{

    private BoxCollider boxCollider;
    public PlayerCurrentRoom player;

    public string tagToCount = "Enemy";


    private int entityCount = 0;

    private string roomName = "";

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        roomName = transform.parent.name;
   }

   void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrentRoom>(); // we need the current room value to that we are not counting entities in the all the rooms at once
    }

   void Update()
    {
        if (player == null) return;

        // only the room the player is in does the counting
        if (player.currentRoomName != roomName) return;

        int newCount = CountTagged();
        if (newCount != entityCount)
        {
            Debug.Log(roomName + " entity count: " + entityCount + " -> " + newCount);
            entityCount = newCount;
        }
    }


    int CountTagged()
{
    Bounds b = boxCollider.bounds;   
    Collider[] hits = Physics.OverlapBox(b.center, b.extents);

    int count = 0;
    foreach (Collider hit in hits)
        if (hit.CompareTag(tagToCount)) count++;
    return count;
}






}
