/*
CPSC340_Dungeonborne_BOZARTH
Drew Bozarth
2373658
dbozarth@chapman.edu
CPSC 340-02
Dungeonborne - Chest.cs
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int coinsAmount = 5;
    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            AudioManager.Instance.PlaySFX("Chest_Open");
            // +5 coins!
            GameManager.instance.ShowText("+" + coinsAmount + " coins!",30,Color.yellow,transform.position,Vector3.up * 25,1.5f);
            GameManager.instance.coins += coinsAmount;
        }
    }
}
