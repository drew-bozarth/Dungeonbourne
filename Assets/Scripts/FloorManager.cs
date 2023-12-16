using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.SetFloorManager(this);
    }

    public void moveToNewFloor()
    {
        // Start the process of making a new floor
        
        StartCoroutine(prepareLoadingScreen());
    }

    private IEnumerator prepareLoadingScreen()
    {
        // Turn off player
        GameManager.instance.player.gameObject.SetActive(false);
        // Pop up the loading UI screen to hide everything
        yield return StartCoroutine(GameManager.instance.loadingScreenChange(1));
        yield return StartCoroutine(swapOutLevelPrefab());
    }

    private IEnumerator swapOutLevelPrefab()
    {
        Debug.Log("SwapOutThing: Start");
        DestroyLevelPrefab();
        yield return new WaitForSeconds(1f);
        InstantiateLevelPrefab();
        Debug.Log("SwapOutThing: End");
        yield return new WaitForSeconds(1f);
        Debug.Log("Gonna fade out screen");
        yield return StartCoroutine(GameManager.instance.FadeOutLoadingScreen());
        Debug.Log("gonna turn on player");
        turnOnPlayer();
    }
    
    private void DestroyLevelPrefab()
    {
        // Find the existing Level Prefab in the scene
        GameObject existingLevel = GameObject.FindWithTag("LevelPrefab");

        // Destroy it if found
        if (existingLevel != null)
        {
            Debug.Log("Destroyed that level");
            Destroy(existingLevel);
        }
    }
    
    private void InstantiateLevelPrefab()
    {
        // Instantiate a new Level Prefab
        Instantiate(GameManager.instance.levelPrefab, Vector3.zero, Quaternion.identity);
        UpdateEnemyHitpoints();
    }

    private void UpdateEnemyHitpoints()
    {
        GameObject[] fighters = GameObject.FindGameObjectsWithTag("Fighter");

        foreach (GameObject fighter in fighters)
        {
            // Check if it is the boss
            if (fighter.name == "Boss1")
            {
                // Update hitpoints based on the formula
                fighter.GetComponent<Boss>().hitpoint += 10 * (GameManager.instance.currentFloorNumber - 1);
                fighter.GetComponent<Boss>().maxHitpoint += 10 * (GameManager.instance.currentFloorNumber - 1);
                // Update it's damage
                fighter.transform.GetChild(0).GetComponent<EnemyHitbox>().damage += 3 * (GameManager.instance.currentFloorNumber - 1);
                // Update the fireball damage
                fighter.transform.GetChild(2).GetComponent<EnemyHitbox>().damage += 1 * (GameManager.instance.currentFloorNumber - 1);
                fighter.transform.GetChild(3).GetComponent<EnemyHitbox>().damage += 1 * (GameManager.instance.currentFloorNumber - 1);
            }
            // Check that the object is not named "Player"
            else if (fighter.name != "Player")
            {
                // Update hitpoints based on the formula
                fighter.GetComponent<Enemy>().hitpoint += 2 * (GameManager.instance.currentFloorNumber - 1);
                fighter.GetComponent<Enemy>().maxHitpoint += 2 * (GameManager.instance.currentFloorNumber - 1);
                // Update it's damage
                fighter.transform.GetChild(0).GetComponent<EnemyHitbox>().damage += 2 * (GameManager.instance.currentFloorNumber - 1);
            }
        }
    }

    private void turnOnPlayer()
    {
        GameManager.instance.player.transform.position = GameObject.Find("SpawnPoint").transform.position;
        GameManager.instance.player.gameObject.SetActive(true);
    }
}
