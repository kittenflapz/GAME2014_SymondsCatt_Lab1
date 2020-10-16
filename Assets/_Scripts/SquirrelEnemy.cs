/* 
 Filename: SquirrelEnemy.cs
 Author: Catt Symonds
 Student Number: 101209214
 Date Last Modified: 15/10/2020
 Description: Player controller script to parse user input and move the player
 Revision History: 
 11/10/2020: File created as enemy AI controller with detection and follow
 16/10/2020: Removed collision detection from enemy and put it in player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SquirrelEnemy : MonoBehaviour
{
    [SerializeField]
    private float maxSqrDistance = 16;
    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private float stunDistance = 16f;

    UnityEvent enemyInPlayerRadius;

    public PlayerController player;
    public Transform target;


    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        Vector3 dir = target.position - transform.position;

        float distance = dir.sqrMagnitude;
            
        float dot = Vector3.Dot(transform.forward, dir.normalized);

        if (distance < maxSqrDistance)
        {
            MoveToTarget(dir);
        }
    }

    private void MoveToTarget(Vector3 target)
    {
        transform.position += target.normalized * moveSpeed * Time.deltaTime;
        transform.up = -(target - transform.position);
    }

}
