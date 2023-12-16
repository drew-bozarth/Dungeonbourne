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
    private bool hasCollided = false;
    protected override void OnCollide(Collider2D coll)
    {
        if (!hasCollided && coll.name == "Player")
        {
            hasCollided = true;
            // TP player to other dungeon
            GameManager.instance.SaveState();
            AudioManager.Instance.PlaySFX("Door_Close");
            
            StartCoroutine(GameManager.instance.FadeInLoadingScreenFromTutorial());
            Debug.Log("Done fading in portal");
            string sceneName = sceneNames[0];
            //SceneManager.LoadScene(sceneName);
        }
    }
}
