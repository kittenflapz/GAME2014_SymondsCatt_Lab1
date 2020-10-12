/* 
 Filename: PlayerController.cs
 Author: Catt Symonds
 Student Number: 101209214
 Date Last Modified: 12/10/2020
 Description: Player controller script to parse user input and move the player
 Revision History: 
 11/10/2020: File created as simple player controller
 12/10/2020: Bug fixing
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_rigidBody;
    private Vector3 touchesEnd = Vector3.zero;

    public float horizontalBoundary;
    public float horizontalSpeed;
    public float maxSpeed;

    public float leftTrack; // the x point on the screen that the player goes when player holds left of screen
    public float rightTrack;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();

    }

    private void _Move()
    {
        float direction = 0.0f;


        // simple touches
        foreach (Touch screenTouch in Input.touches)
        {
            Vector3 worldTouch = Camera.main.ScreenToWorldPoint(screenTouch.position);

            if (worldTouch.x > 0)
            {
                // direction is positive
                direction = 1.0f;
            }
            if (worldTouch.x < 0)
            {
                // direction is negative
                direction = -1.0f;
            }
            touchesEnd = worldTouch;
        }

        // keyboard input
        if (Input.GetAxis("Horizontal") >= 0.01f)
        {
            // direction is positive
            direction = 1.0f;
        }
        if (Input.GetAxis("Horizontal") <= -0.01f)
        {  
            // direction is negative
            direction = -1.0f;
        }

        // move the cat
        if (touchesEnd != Vector3.zero) // player is touching screen
        {
            transform.position = new Vector2(Mathf.Lerp(transform.position.x, direction < 0 ? leftTrack : 0.0f, 0.01f), transform.position.y);
        }
        else // player not touching screen
        {
            //Vector2 newVelocity = m_rigidBody.velocity + new Vector2(direction * horizontalSpeed, 0.0f);
            //m_rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
            //m_rigidBody.velocity *= 0.99f;
            transform.position = new Vector2(Mathf.Lerp(transform.position.x, 0.0f, 0.01f), transform.position.y); // go back to the middle
           // transform.position = new Vector2(Mathf.Lerp(transform.position.x, direction < 0 ? leftTrack : rightTrack, 0.01f), transform.position.y);
        }

    }

    private void _CheckBounds()
    {
        // Check right bounds
        if (transform.position.x >= horizontalBoundary)
        {
            transform.position = new Vector3(horizontalBoundary, transform.position.y);
        }

        // Check left bounds
        if (transform.position.x <= -horizontalBoundary)
        {
            transform.position = new Vector3(-horizontalBoundary, transform.position.y);
        }

    }

}