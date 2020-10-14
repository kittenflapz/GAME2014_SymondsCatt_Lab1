/* 
 Filename: GameManager.cs
 Author: Catt Symonds
 Student Number: 101209214
 Date Last Modified: 14/10/2020
 Description: All base game management
 Revision History: 
 14/10/2020: File created, timer function implemented
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    public float totalSeconds;

    [SerializeField]
    public float timeSecondsLeft;

    private float totalTime;
    private float timeLeft;
    public Image timerFill;

    private void Start()
    {
        totalTime = totalSeconds * 1000;
        timeLeft = timeSecondsLeft * 1000;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        timeLeft -= Time.time;

        timerFill.fillAmount = timeLeft / totalTime;
    }
}
