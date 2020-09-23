/* 
 Filename: SceneMgmtButtons.cs
 Author: Catt Symonds
 Student Number: 101209214
 Date Last Modified: 22/09/2020
 Description: Button event managers for scene management
 Revision History: 
 22/09/2020: File created as an amalgamation of several button manager scripts.
 23/09/2020: Added a slight delay using a coroutine to allow for audio feedback.
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgmtButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        yield return new WaitForSeconds(0.5f); // Waiting for the UI clicking sound to finish playing before we load into the new scene

        SceneManager.LoadScene(sceneName);
    }
}
