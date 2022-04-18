using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Use to load main game scene to start the game from the File > Build Settings > Scenes In Build index
    public void PlayGame()
    {
        // Set Start Screen scene to index 0 in Scenes In Build
        // Set first game scene as index 1 in Scenes In Build
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Use to quit game
    public void QuitGame()
    {
        // This function only works when the game build has been exported
        Application.Quit();
        // For testing purposes only in the Unity console when in Play mode
        Debug.Log("Exited Game");
    }
}
