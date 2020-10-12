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
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgmtButtons : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void OnStartButtonPressed()
    {
        //Debug.Log("Start Button Pressed");
        //SceneManager.LoadScene("PlayScene");

        StartCoroutine(WaitForUIClickAndLoadScene("PlayScene"));
    }

    public void OnNextButtonPressed()
    {
        //Debug.Log("End Button Pressed");
        //SceneManager.LoadScene("EndScene");

        StartCoroutine(WaitForUIClickAndLoadScene("EndScene"));
    }

    public void OnBackButtonPressed()
    {
        //Debug.Log("Back Button Pressed");
        // SceneManager.LoadScene("StartScene");

        StartCoroutine(WaitForUIClickAndLoadScene("StartScene"));
    }
    public void OnInstructionsButtonPressed()
    {
       // Debug.Log("Instructions Button Pressed");
        // SceneManager.LoadScene("InstructionsScene");

        StartCoroutine(WaitForUIClickAndLoadScene("InstructionsScene"));
    }


    private IEnumerator WaitForUIClickAndLoadScene(string sceneName)
    {
        // Play animation
        transition.SetTrigger("Start");

        // Wait for it to stop playing
        yield return new WaitForSeconds(transitionTime); // Waiting for the UI clicking sound to finish playing before we load into the new scene

        SceneManager.LoadScene(sceneName);
    }
}
