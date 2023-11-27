using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour
{
    private string[,] Rooms = {{"Room_0_0","Room_0_1", "Room_0_2"},
        {"Room_1_0","Room_1_1","Room_1_2"},{"Room_2_0","Room_2_1","Room_2_2"},
        {"Room_3_0","Room_3_1","Room_3_2"}};

    List<string> unclearedRooms = new List<string>();
    List<string> clearedRooms = new List<string>();
    private string currentRoom;
    private bool doorsUnlocked;
    private bool isClearingRoom = false;
    private GameObject doorsParent;
    private GameObject doorGridObject;
    private TilemapRenderer tilemapRenderer;

    private void Start()
    {
        GameManager.instance.SetRoomManager(this);
        foreach (string room in Rooms)
        {
            unclearedRooms.Add(room);
        }
        doorsParent = GameObject.Find("Doors");
        doorGridObject = GameObject.Find("OtherExtra");
        //tilemapRenderer = doorGridObject.GetComponent<TilemapRenderer>();
        
        currentRoom = Rooms[0,0];
        clearRoom(currentRoom);
    }
    
    void Update()
    {
        if (unclearedRooms.Contains(currentRoom) && !isClearingRoom)
        {
            StartCoroutine(CheckAndClearRoom());
        }
    }
    
    private IEnumerator CheckAndClearRoom()
    {
        isClearingRoom = true;
        Debug.Log("Clearing room rn");
        Transform currentRoomTransform = transform.Find(currentRoom);
        // Wait until the next frame to allow Unity to update the hierarchy
        yield return null;
        if (currentRoomTransform != null && currentRoomTransform.childCount == 0)
        {
            Debug.Log("We cleared the room");
            // Call clearRoom(currentRoom)
            clearRoom(currentRoom);
        }
        isClearingRoom = false;
    }

    public void moveToNewRoom(string newRoomName)
    {
        Debug.Log("Got new room name: " + newRoomName);
        Debug.Log("current Uncleared Rooms: " + string.Join(", ", unclearedRooms));
        Debug.Log("current Cleared Rooms: " + string.Join(", ", clearedRooms));
        currentRoom = newRoomName;
        // update doors whether or not room is clear rn
        updateDoors();
    }

    private void clearRoom(string roomName)
    {
        // Check if the room is found in list
        if (unclearedRooms.Contains(roomName))
        {
            Debug.Log("Clearing :: " + roomName);
            // Remove the room from unclearedRooms
            unclearedRooms.Remove(roomName);
            // Add the room to clearedRooms
            clearedRooms.Add(roomName);
        }
        // update doors
        updateDoors();
    }

    private void updateDoors()
    {
        if (clearedRooms.Contains(currentRoom))
        {
            // SET SPRITE TO OPEN (on)
            //tilemapRenderer.enabled = false;
            doorGridObject.gameObject.SetActive(false);
            // TURN ON COLLIDERS
            doorsParent.gameObject.SetActive(true);
            Debug.Log("DOORS OPEN!!");
        }
        else
        {
            // Set sprite to closed (off)
            //tilemapRenderer.enabled = true;
            doorGridObject.gameObject.SetActive(true);
            // turn off colliders
            doorsParent.gameObject.SetActive(false);
            Debug.Log("Doors closed");
        }
    }
}
