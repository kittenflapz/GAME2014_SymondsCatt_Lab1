/* 
 Filename: SceneMgmtButtons.cs
 Author: Catt Symonds
 Student Number: 101209214
 Date Last Modified: 22/09/2020
 Description: Button event managers for scene management
 Revision History: 
 22/09/2020: File created as an amalgamation of several button manager scripts.
 23/09/2020: Added a slight delay using a coroutine to allow for audio feedback.
 12/10/2020: Added crossfade animations
 16/10/2020: Added GameOver functionality
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgmtButtons : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;


    // Changes scene to play scene
    public void OnStartButtonPressed()
    {
        //Debug.Log("Start Button Pressed");
        //SceneManager.LoadScene("PlayScene");

        StartCoroutine(WaitForUIClickAndLoadScene("PlayScene"));
    }


    // Changes scene to GameOver scene

    public void OnNextButtonPressed()
    {
        //Debug.Log("End Button Pressed");
        //SceneManager.LoadScene("EndScene");

        StartCoroutine(WaitForUIClickAndLoadScene("GameOverScene"));
    }

    // Changes scene to win scene
    public void OnWin()
    {
        //Debug.Log("End Button Pressed");
        //SceneManager.LoadScene("EndScene");

        StartCoroutine(WaitForUIClickAndLoadScene("LevelWinScreen"));
    }

    // Changes scene to start scene (not actually used right now)
    public void OnBackButtonPressed()
    {
        //Debug.Log("Back Button Pressed");
        // SceneManager.LoadScene("StartScene");

        StartCoroutine(WaitForUIClickAndLoadScene("StartScene"));
    }

    //  Changes scene to instructions scene
    public void OnInstructionsButtonPressed()
    {
       // Debug.Log("Instructions Button Pressed");
        // SceneManager.LoadScene("InstructionsScene");

        StartCoroutine(WaitForUIClickAndLoadScene("InstructionsScene"));
    }


    // Waits a lil bit before loading the scene passed to it. Also handles scene transition
    private IEnumerator WaitForUIClickAndLoadScene(string sceneName)
    {
        // Play animation
        transition.SetTrigger("Start");

        // Wait for it to stop playing
        yield return new WaitForSeconds(transitionTime); // Waiting for the UI clicking sound to finish playing before we load into the new scene

        SceneManager.LoadScene(sceneName);
    }
}
