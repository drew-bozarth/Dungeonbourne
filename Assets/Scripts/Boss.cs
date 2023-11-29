/*
CPSC340_Dungeonborne_BOZARTH
Drew Bozarth
2373658
dbozarth@chapman.edu
CPSC 340-02
Dungeonborne - Boss.cs
*/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : Enemy
{
    public float[] fireballSpeed = {2.5f, -2.5f};
    public float distance = 0.3f;
    public Transform[] fireballs;

    private void Update()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            fireballs[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * fireballSpeed[i]) * distance,
                Mathf.Sin(Time.time * fireballSpeed[i]) * distance, 0);
        }
    }
}
