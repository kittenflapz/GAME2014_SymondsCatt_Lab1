/* 
 Filename: WinKibbleController.cs
 Author: Catt Symonds
 Student Number: 101209214
 Date Last Modified: 20/10/2020
 Description: For implementation of a win condition when player reaches this gameobject
 Revision History: 
 20/10/2020: File created
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinKibbleController : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerController player;

    // Checks if player has hit the kibble (in order to win the game)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.Kill();
            gameManager.Win();
        }
    }
}
