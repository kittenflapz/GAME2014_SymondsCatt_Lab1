/* 
 Filename: GameOverScoreLoader.cs
 Author: Catt Symonds
 Student Number: 101209214
 Date Last Modified: 17/10/2020
 Description: For loading in and displaying scores on the GameOver screen
 Revision History: 
 17/10/2020: File created
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameOverScoreLoader : MonoBehaviour
{
   public TextMeshProUGUI treatsEaten;
   public TextMeshProUGUI squirrelsSpooked;
    ScoreKeeper scoreKeeper;

    // Start is called before the first frame update

        // Loads scores at end of game
    void Start()
    {
        scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();

        treatsEaten.SetText(scoreKeeper.LoadLastTreatsEaten().ToString());
        squirrelsSpooked.SetText(scoreKeeper.LoadLastSquirrelsSpooked().ToString());
    }

   
}
