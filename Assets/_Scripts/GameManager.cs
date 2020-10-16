/* 
 Filename: GameManager.cs
 Author: Catt Symonds
 Student Number: 101209214
 Date Last Modified: 14/10/2020
 Description: All base game management
 Revision History: 
 14/10/2020: File created, timer function implemented
 16/10/2020: Created time up and game over function
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float totalSeconds;

    [SerializeField]
    private float timeSecondsLeft;

    private float totalTime;
    private float timeLeft;

    public Image timerFill;
    public TextMeshProUGUI timeUpLabel;
    public SceneMgmtButtons sceneManagement;
    public PlayerController player;

    private void Start()
    {
        totalTime = totalSeconds * 1000;
        timeLeft = timeSecondsLeft * 1000;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= 0)
        {
            TimeUp();
        }
        else
        {
            UpdateTimer();
        }
    }

    void UpdateTimer()
    {
        timeLeft -= Time.time;

        timerFill.fillAmount = timeLeft / totalTime;
    }

    void TimeUp()
    {
        timeUpLabel.gameObject.SetActive(true);
        StartCoroutine(TimeUpCoro());
        player.Kill();
        
    }

    IEnumerator TimeUpCoro()
    {
        yield return new WaitForSeconds(1.5f);
        GameOver();
    }

    void GameOver()
    {
        sceneManagement.OnNextButtonPressed();
    }

  
}
