/* 
 Filename: CameraFollow.cs
 Author: Catt Symonds
 Student Number: 101209214
 Date Last Modified: 14/10/2020
 Description: Camera following functionality for camera to follow player
 Revision History: 
 14/10/2020: File created, basic camera follow functionality in place
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;

    // Ensures that the camera always follows the player on the y axis without moving on the x axis.
    void Update()
    {
        Vector3 position = transform.position;
        position.y = (player.transform.position + offset).y;
        transform.position = position;
    }
}
