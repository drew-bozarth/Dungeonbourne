using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staircase : Collidable
{
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            // This floor is over, call GameManager to tell LevelManager
            GameManager.instance.moveToNewFloor();
        }
    }
}
