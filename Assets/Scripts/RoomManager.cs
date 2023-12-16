/*
CPSC340_Dungeonborne_BOZARTH
Drew Bozarth
2373658
dbozarth@chapman.edu
CPSC 340-02
Dungeonborne - RoomManager.cs
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class RoomManager : MonoBehaviour
{
    public string[][] Rooms;
    public int rows;
    public int cols;

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
        // NEW CODE TO SETUP ROOMS:
        Rooms = new string[rows][];
        for (int i = 0; i < rows; i++)
        {
            Rooms[i] = new string[cols];
            for (int j = 0; j < cols; j++)
            {
                // Assign values in the format "Room_(row)_(col)"
                Rooms[i][j] = "Room_" + i + "_" + j;
            }
        }
        //foreach (string room in Rooms)
        //{
          //  unclearedRooms.Add(room);
        //}
        foreach (string[] row in Rooms)
        {
            foreach (string room in row)
            {
                //Debug.Log(room);
                unclearedRooms.Add(room);
            }
        }
        doorsParent = GameObject.Find("Doors");
        doorGridObject = GameObject.Find("OtherExtra");
        
        //currentRoom = Rooms[0,0];
        currentRoom = Rooms[0][0];
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
        Transform currentRoomTransform = transform.Find(currentRoom);
        // Wait until the next frame to allow Unity to update the hierarchy
        yield return null;
        if (currentRoomTransform != null && currentRoomTransform.childCount == 0)
        {
            // Call clearRoom(currentRoom)
            clearRoom(currentRoom);
        }
        isClearingRoom = false;
    }

    public void moveToNewRoom(string newRoomName)
    {
        currentRoom = newRoomName;
        // update doors whether or not room is clear rn
        updateDoors();
    }

    private void clearRoom(string roomName)
    {
        // Check if the room is found in list
        if (unclearedRooms.Contains(roomName))
        {
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
            doorGridObject.gameObject.SetActive(false);
            // TURN ON COLLIDERS
            doorsParent.gameObject.SetActive(true);
            //Debug.Log("DOORS OPEN!!");
        }
        else
        {
            // Set sprite to closed (off)
            doorGridObject.gameObject.SetActive(true);
            // turn off colliders
            doorsParent.gameObject.SetActive(false);
            //Debug.Log("Doors closed");
        }
    }
}
