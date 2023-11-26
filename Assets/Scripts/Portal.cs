/*
CPSC340_Dungeonborne_BOZARTH
Drew Bozarth
2373658
dbozarth@chapman.edu
CPSC 340-02
Dungeonborne - Portal.cs
*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public string[] sceneNames;
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            // TP player to other dungeon
            GameManager.instance.SaveState();
            AudioManager.Instance.PlaySFX("Door_Close");
            string sceneName = sceneNames[0];
            SceneManager.LoadScene(sceneName);
        }
    }
}
