/* 
 Filename: GameManager.cs
 Author: Catt Symonds
 Student Number: 101209214
 Date Last Modified: 14/10/2020
 Description: All base game management
 Revision History: 
 14/10/2020: File created, timer function implemented
 16/10/2020: Created time up and game over function
 16/10/2020: Created AddTime function
 17/10/2020: Debugged timer visual so that if you get extra time it recalculates rather than just looking full
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

    [SerializeField]
    private float speedModifier = 1.0f;

    private float totalTime;
    private float timeLeft;

    private Animator timerAnim;

    public Image timerFill;
    public TextMeshProUGUI timeAddedLabel;

    public TextMeshProUGUI timeUpLabel;
    public SceneMgmtButtons sceneManagement;
    public PlayerController player;

    private void Start()
    {
        timerAnim = timerFill.GetComponent<Animator>();
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
            ApplySpeedModifiers();
            UpdateTimer();
        }

    }

    void ApplySpeedModifiers()
    {
        if (player.BeingAttacked)
        {
            speedModifier = 2.0f;
            timerAnim.SetBool("spedUp", true);
        }
        else
        {
            speedModifier = 1.0f;
            timerAnim.SetBool("spedUp", false);
        }
    }

    void UpdateTimer()
    {
        timeLeft -= Time.time * speedModifier;

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

    public void AddTime(int secondsToAdd)
    {
        timeLeft += secondsToAdd * 1000;
        if (timeLeft > totalTime)
        {
            totalTime = timeLeft;
        }
        timeAddedLabel.SetText("+" + secondsToAdd.ToString());
       StartCoroutine(TurnOnTimeLabelForSeconds(1.5f));
    }

    IEnumerator TurnOnTimeLabelForSeconds(float seconds)
    {
        timeAddedLabel.gameObject.SetActive(true);
        yield return new WaitForSeconds(seconds);
        timeAddedLabel.gameObject.SetActive(false);
    }


}
