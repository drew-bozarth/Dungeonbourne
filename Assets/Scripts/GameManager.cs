/*
CPSC340_Dungeonborne_BOZARTH
Drew Bozarth
2373658
dbozarth@chapman.edu
CPSC 340-02
Dungeonborne - GameManager.cs
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }
        
        // To clear progress turn off or on
        PlayerPrefs.DeleteAll();
        
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    // Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public Sprite EEweaponSprite;
    public List<int> weaponPrices;
    public List<int> xpTable;
    
    // References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public Animator deathMenuAnim;
    public GameObject hud;
    public GameObject menu;
    public RectTransform hitpointBar;
    private RoomManager roomManager;
    
    // Logic
    public int coins;
    public int xp;

    // Floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg,fontSize,color,position,motion,duration);
    }
    
    // Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        // is the weapon max level?
        if (weaponPrices.Count <= weapon.weaponLevel)
        {
            return false;
        }

        // do you have enough coins to upgrade?
        if (coins >= weaponPrices[weapon.weaponLevel])
        {
            coins -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }
    
    // Experience System
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while (xp >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count) // Max Level
            {
                return r;
            }
        }

        return r;
    }
    public int GetXPToLevel(int level)
    {
        int r = 0;
        int experience = 0;

        while (r < level)
        {
            experience += xpTable[r];
            r++;
        }

        return experience;
    }
    public void GrantXP(int experience)
    {
        int currLevel = GetCurrentLevel();
        xp += experience;
        if (currLevel < GetCurrentLevel())
        {
            OnLevelUp();
        }
    }
    public void OnLevelUp()
    {
        player.OnLevelUp();
        OnHitpointChange();
    }
    
    // Health Bar
    public void OnHitpointChange()
    {
        float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(ratio, 1, 1);
    }
    
    // Room system
    public void SetRoomManager(RoomManager roomManagerReference)
    {
        roomManager = roomManagerReference;
    }
    
    public void moveToNewRoom (string roomName)
    {
        roomManager.moveToNewRoom(roomName);
    }

    // Death Menu and Respawn
    public void Respawn()
    {
        deathMenuAnim.SetTrigger("Hide");
        Destroy(GameManager.instance.floatingTextManager.gameObject);
        Destroy(GameManager.instance.gameObject);
        Destroy(GameManager.instance.player.gameObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MM_DBozarth");
    }
    
    // On Scene Loaded
    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    // Save state of game
    /*
     * Int preferredSkin
     * Int coins
     * Int xp
     * Int weaponLevel
     */
    public void SaveState()
    {
        string s = "";

        s += "0" + "|";
        s += coins.ToString() + "|";
        s += xp.ToString() + "|";
        s += weapon.weaponLevel.ToString();
        
        PlayerPrefs.SetString("SaveState",s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;
        
        if (!PlayerPrefs.HasKey("SaveState"))
            return;
        
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        
        // Change player skin
        coins = int.Parse(data[1]);
        
        // Setup XP levels
        xp = int.Parse(data[2]);
        if (GetCurrentLevel() != 1)
        {
            player.SetLevel(GetCurrentLevel());
        }
        
        // Change the weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));

        
        // LAZY CODE --- REMOVE LATER
        hitpointBar = (RectTransform)GameObject.Find("Health").transform;
        OnHitpointChange();
    }
}
