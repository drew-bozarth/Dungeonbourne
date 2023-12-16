/*
CPSC340_Dungeonborne_BOZARTH
Drew Bozarth
2373658
dbozarth@chapman.edu
CPSC 340-02
Dungeonborne - Player.cs
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Mover
{
    // Set to 1.0 for player
    public float playerSpeedChange = 1.0f;
    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;
    private bool controlsOn = true;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hitpoint = 30;
        maxHitpoint = 30;
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (!isAlive)
            return;
        
        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitpointChange();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        
        if (isAlive && controlsOn)
        {
            UpdateMotor(new Vector3(x,y,0), playerSpeedChange);
        }
    }

    protected override void Death()
    {
        GameManager.instance.deathMenuAnim.SetTrigger("Show");
        isAlive = false;
        this.gameObject.SetActive(false);
        
        //Destroy(gameObject);
        //Destroy(GameManager.instance.floatingTextManager.gameObject);
        //Destroy(GameManager.instance.gameObject);
        AudioManager.Instance.PlaySFX("Enemy_Death");
        //SceneManager.LoadScene(0);
    }

    public void SwapSprite(int skinID)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinID];
    }

    public void OnLevelUp()
    {
        // increase max health
        maxHitpoint++;
        // refill health
        hitpoint = maxHitpoint;
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }

    public void Heal (int healingAmount)
    {
        if (hitpoint == maxHitpoint)
        {
            return;
        }
        
        hitpoint += healingAmount;
        if (hitpoint > maxHitpoint)
        {
            hitpoint = maxHitpoint;
        } 
        GameManager.instance.ShowText("+" + healingAmount.ToString() + " hp", 45, Color.green, transform.position, Vector3.up * 30, 1.0f);
        GameManager.instance.OnHitpointChange();
    }
    
    // enable or disable controls externally
    public void SetControlsOn(bool isEnabled)
    {
        controlsOn = isEnabled;
    }
}
