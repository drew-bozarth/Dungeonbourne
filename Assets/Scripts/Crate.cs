/*
CPSC340_Dungeonborne_BOZARTH
Drew Bozarth
2373658
dbozarth@chapman.edu
CPSC 340-02
Dungeonborne - Crate.cs
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Fighter
{
    protected override void Death()
    {
        Destroy(gameObject);
    }
}
