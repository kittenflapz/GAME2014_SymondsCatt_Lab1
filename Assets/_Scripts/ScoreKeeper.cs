/* 
 Filename: PlayerController.cs
 Author: Catt Symonds
 Student Number: 101209214
 Date Last Modified: 17/10/2020
 Description: Keeps score including high score for display on game over screen
 Revision History: 
 17/10/2020: File created
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class ScoreKeeper : MonoBehaviour
{
    private string saveString;
    private string HSCheckString;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("SquirrelsSpookedHighScore"))
        {
            PlayerPrefs.SetString("SquirrelsSpookedHighScore", 0.ToString());
        }
        if (!PlayerPrefs.HasKey("TreatsEatenHighScore"))
        {
            PlayerPrefs.SetString("TreatsEatenHighScore", 0.ToString());
        }
        if (!PlayerPrefs.HasKey("TimeLeftHighScore"))
        {
            PlayerPrefs.SetString("TimeLeftHighScore", 0f.ToString());
        }
    }
    public void SaveSquirrelsSpooked(int squirrelsSpooked)
    {
        // Save in latest data string
        StringBuilder playerLastSqScore = new StringBuilder();
        playerLastSqScore.Append(squirrelsSpooked);
        saveString = playerLastSqScore.ToString();

        PlayerPrefs.SetString("LastSquirrelsSpooked", saveString);

        Debug.Log("Saved the following data: " + saveString);

        // If higher than existing high score, save in high score data string too

        HSCheckString = PlayerPrefs.GetString("SquirrelsSpookedHighScore");
        int existingHighScore = int.Parse(HSCheckString);

        if (squirrelsSpooked > existingHighScore)
        {
            PlayerPrefs.SetString("SquirrelsSpookedHighScore", saveString);
        }
    }

    public void SaveTreatsEaten(int treatsEaten)
    {
        // Save in latest data string
        StringBuilder playerLastTreatScore = new StringBuilder();
        playerLastTreatScore.Append(treatsEaten);
        saveString = playerLastTreatScore.ToString();

        PlayerPrefs.SetString("LastTreatsEaten", saveString);

        Debug.Log("Saved the following data: " + saveString);

        // If higher than existing high score, save in high score data string too

        HSCheckString = PlayerPrefs.GetString("TreatsEatenHighScore");
        int existingHighScore = int.Parse(HSCheckString);

        if (treatsEaten > existingHighScore)
        {
            PlayerPrefs.SetString("TreatsEatenHighScore", saveString);
        }
    }

    public void SaveTimeLeft(float timeLeft)
    {
        // Save in latest data string
        StringBuilder playerLastTimeLeft = new StringBuilder();
        playerLastTimeLeft.Append(timeLeft);
        saveString = playerLastTimeLeft.ToString();

        PlayerPrefs.SetString("LastTimeLeft", saveString);

        Debug.Log("Saved the following data: " + saveString);

        // If higher than existing high score, save in high score data string too

        HSCheckString = PlayerPrefs.GetString("TimeLeftHighScore");
       float existingHighScore = float.Parse(HSCheckString);

        if (timeLeft > existingHighScore)
        {
            PlayerPrefs.SetString("TimeLeftHighScore", saveString);
        }
    }

    public int LoadTreatsEatenHS()
    {
        HSCheckString = PlayerPrefs.GetString("SquirrelsSpookedHighScore");
        int existingHighScore = int.Parse(HSCheckString);

        return existingHighScore;
    }

    public int LoadSquirrelsSpookedHS()
    {
        HSCheckString = PlayerPrefs.GetString("SquirrelsSpookedHighScore");
        int existingHighScore = int.Parse(HSCheckString);

        return existingHighScore;
    }

    public float LoadTimeLeftHS()
    {
        HSCheckString = PlayerPrefs.GetString("TimeLeftHighScore");
        float existingHighScore = float.Parse(HSCheckString);

        return existingHighScore;
    }

    public int LoadLastTreatsEaten()
    {
        saveString = PlayerPrefs.GetString("LastTreatsEaten");
        int score = int.Parse(saveString);

        return score;
    }

    public int LoadLastSquirrelsSpooked()
    {
        saveString = PlayerPrefs.GetString("LastSquirrelsSpooked");
        int score = int.Parse(saveString);

        return score;
    }

    public float LoadLastTimeLeft()
    {
        saveString = PlayerPrefs.GetString("LastTimeLeft");
        float score = float.Parse(saveString);

        return score;
    }

}
