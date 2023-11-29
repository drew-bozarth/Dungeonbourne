/*
CPSC340_Dungeonborne_BOZARTH
Drew Bozarth
2373658
dbozarth@chapman.edu
CPSC 340-02
Dungeonborne - NPCText.cs
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCText : Collidable
{
    public string message;

    private float cooldown = 5.0f;
    private float lastShout;

    protected override void Start()
    {
        base.Start();
        lastShout = -cooldown;
    }
    
    protected override void OnCollide(Collider2D coll)
    {
        if (Time.time - lastShout > cooldown)
        {
            lastShout = Time.time;
            GameManager.instance.ShowText(message, 45, Color.white, transform.position + new Vector3(0,0.16f,0), Vector3.zero, 4.0f);
        }
        
    }
}
