using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinKibbleController : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerController player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.Kill();
            gameManager.Win();
        }
    }
}
