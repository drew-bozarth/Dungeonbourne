/*
CPSC340_Dungeonborne_BOZARTH
Drew Bozarth
2373658
dbozarth@chapman.edu
CPSC 340-02
Dungeonborne - Weapon.cs
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Damage struct
    public int[] damagePoint = { 1, 2, 3, 4, 5, 6, 7, 8 };
    public float[] pushForce = { 2.5f, 3.0f, 3.5f, 4.0f, 4.5f, 5.0f, 5.5f, 6.0f};
    
    // Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;
    
    // Swing
    private Animator anim;
    private float cooldown = 0.3f;
    private float lastSwing;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Fighter"))
        {
            if (coll.name != "Player")
            {
                // Create a new damage object, then we'll send it to the fighter we've hit
                Damage dmg = new Damage
                {
                    damageAmount = damagePoint[weaponLevel],
                    origin = transform.position,
                    pushForce = pushForce[weaponLevel]
                };
                
                coll.SendMessage("ReceiveDamage",dmg);
                //AudioManager.Instance.PlaySFX("Sword_Hit");
            }
        }
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
        AudioManager.Instance.PlaySFX("Sword_Miss");
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    public void ToggleEEWeapon(bool toggle)
    {
        if (toggle)
        {
            spriteRenderer.sprite = GameManager.instance.EEweaponSprite;
        }
        else
        {
            spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
        }
    }
}
