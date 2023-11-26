/*
CPSC340_Dungeonborne_BOZARTH
Drew Bozarth
2373658
dbozarth@chapman.edu
CPSC 340-02
Dungeonborne - Door.cs
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Collidable
{
    //private RoomManager roomManager;
    private string spawnPointString;

        /*
        GameObject roomsObject = GameObject.Find("Rooms");

        if (roomsObject != null)
        {
            roomManager = roomsObject.GetComponent<RoomManager>();

            if (roomManager == null)
            {
                Debug.LogError("RoomManager component not found on the 'Rooms' GameObject.");
            }
        }
        else
        {
            Debug.LogError("GameObject with the name 'Rooms' not found.");
        }
        */
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            // TP player to other room based on door
            //GameManager.instance.SaveState();
            //AudioManager.Instance.PlaySFX("Door_Close");

            spawnPointString = getCorrectDoorSpawn();

            GameManager.instance.player.transform.position = GameObject.Find(spawnPointString).transform.position;
        }
    }

    private string getCorrectDoorSpawn()
    {
        string thisDoorName = gameObject.name;
        string[] nameParts = thisDoorName.Split('_');
        // [0] = "Door", [1] = row, [2] = col, [3] = direction
        string newDoorName = "SpawnPoint";
        string newRoomName = "Room_";
        switch (nameParts[3])
        {
            case "T":
                // going up, so row + 1, spawn = B
                newDoorName += (int.Parse(nameParts[1]) + 1).ToString() + nameParts[2].ToString() + "B";
                newRoomName += (int.Parse(nameParts[1]) + 1).ToString() + "_" + nameParts[2].ToString();
                break;
            case "B":
                // going down, so row - 1, spawn = T
                newDoorName += (int.Parse(nameParts[1]) - 1).ToString() + nameParts[2].ToString() + "T";
                newRoomName += (int.Parse(nameParts[1]) - 1).ToString() + "_" + nameParts[2].ToString();
                break;
            case "L":
                // going left, so col - 1, spawn = R
                newDoorName += nameParts[1].ToString() + (int.Parse(nameParts[2]) - 1).ToString() + "R";
                newRoomName += nameParts[1].ToString() + "_" + (int.Parse(nameParts[2]) - 1).ToString();
                break;
            case "R":
                // going right, so col + 1, spawn = L
                newDoorName += nameParts[1].ToString() + (int.Parse(nameParts[2]) + 1).ToString() + "L";
                newRoomName += nameParts[1].ToString() + "_" + (int.Parse(nameParts[2]) + 1).ToString();
                break;
            default:
                Debug.Log("Invalid option here");
                break;
        }
        Debug.Log("GO TO DOOR: " + newDoorName);
        Debug.Log("GO TO new room from door: " + newRoomName);
        GameManager.instance.moveToNewRoom(newRoomName);
        //roomManager.moveToNewRoom(newRoomName);
        return newDoorName;
    }
}
