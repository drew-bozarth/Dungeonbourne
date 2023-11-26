/*
CPSC340_Dungeonborne_BOZARTH
Drew Bozarth
2373658
dbozarth@chapman.edu
CPSC 340-02
Dungeonborne - Fighter.cs
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    // Public fields
    public int hitpoint = 10;
    public int maxHitpoint = 10;
    public float pushRecoverySpeed = 0.2f;
    
    // Immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;
    
    // Push
    protected Vector3 pushDirection;
    
    // All fighters can ReceiveDamage / Die
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;
            if (this.name == "Player")
            {
                GameManager.instance.ShowText("-" + dmg.damageAmount.ToString(), 45, Color.red, transform.position, Vector3.zero, 0.5f);
                AudioManager.Instance.PlaySFX("Player_Damage");
            }
            else
            {
                GameManager.instance.ShowText("-" + dmg.damageAmount.ToString(), 45, Color.magenta, transform.position, Vector3.zero, 0.5f);
                AudioManager.Instance.PlaySFX("Enemy_Damage");
            }
            
            if (hitpoint <= 0)
            {
                hitpoint = 0;
                Death();
            }
        }
    }

    protected virtual void Death()
    {
        Debug.Log(this.name + " died...");
    }
}
