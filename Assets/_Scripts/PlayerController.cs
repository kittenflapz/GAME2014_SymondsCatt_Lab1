/* 
 Filename: PlayerController.cs
 Author: Catt Symonds
 Student Number: 101209214
 Date Last Modified: 12/10/2020
 Description: Player controller script to parse user input and move the player
 Revision History: 
 11/10/2020: File created as simple player controller
 12/10/2020: Bug fixing.
 12/10/2020: Changed areas of screen that are touchable as game input. Added attack audio.
 15/10/2020: Added handler for being stunned by enemy
 16/10/2020: Added attack in radius function
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 touchesEnd = Vector3.zero;

    [SerializeField]
    private float horizontalBoundary;

    [SerializeField]
    private float horizontalSpeed;

    [SerializeField]
    private float verticalSpeed;

    [SerializeField]
    private float maxSpeed;

    [SerializeField]
    private float attackRadius;

    [SerializeField]
    private float leftTrack; // the x point on the screen that the player goes when player holds left of screen

    [SerializeField]
    private float rightTrack;

    public Transform gameInputVerticalCutoff;
    public float movementZoneStart;

    public Animator animator;
    public AudioSource meow;
    

    private bool isStunned;
    private bool isDead;
    private BoxCollider2D playerCollider;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        meow = GetComponent<AudioSource>();
        playerCollider = GetComponentInChildren<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (!isStunned)
            {
                _Move();
            }
        }
    }

    private void _Move()
    {
        float direction = 0.0f;


        // simple touches
        foreach (Touch screenTouch in Input.touches)
        {
            Vector3 worldTouch = Camera.main.ScreenToWorldPoint(screenTouch.position);

            if (worldTouch.y < gameInputVerticalCutoff.position.y)
            {
                if (worldTouch.x > movementZoneStart)
                {
                    // direction is positive
                    direction = 1.0f;
                }
                else if (worldTouch.x < -movementZoneStart)
                {
                    // direction is negative
                    direction = -1.0f;
                }
                else
                {
                    _Attack();
                }
                
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _Attack();
        }


            // move the cat left or right
        if (touchesEnd != Vector3.zero) // player is touching screen
        {
            if(direction < 0)
            {
                transform.position = new Vector2(Mathf.Lerp(transform.position.x, leftTrack, 0.1f), transform.position.y);
            }
            else if (direction > 0)
            {
                transform.position = new Vector2(Mathf.Lerp(transform.position.x, rightTrack, 0.1f), transform.position.y);
            }
            else
            {
                transform.position = new Vector2(Mathf.Lerp(transform.position.x, 0.0f, 0.1f), transform.position.y);
            }

           // transform.position = new Vector2(Mathf.Lerp(transform.position.x, direction < 0 ? leftTrack : rightTrack, 0.01f), transform.position.y);
        }
        else // player not touching screen
        {
            transform.position = new Vector2(Mathf.Lerp(transform.position.x, 0.0f, 0.01f), transform.position.y); // go back to the middle
        }

        // move the cat up
        transform.position = new Vector2(transform.position.x, transform.position.y + verticalSpeed * Time.deltaTime);
    }

    private void _Attack()
    {
        // Play the attack animation
        animator.SetTrigger("Attack");

        // Play the attack sound
        meow.Play();

        // Create a layer mask for enemies (must put enemies on 'Enemy' layer!)
        LayerMask mask = LayerMask.GetMask("Enemy");

        // Check a circle around the player and get all enemy colliders overlapping the circle
        Collider2D[] collidersInAttackRadius = Physics2D.OverlapCircleAll(transform.position, attackRadius, mask);

        foreach(Collider2D collider in collidersInAttackRadius)
        {
            Destroy(collider.gameObject);
        }
    }

    public void Stunned()
    {
        StartCoroutine(OnStunned());
    }

    public void Kill()
    {
        isDead = true;
    }

    public IEnumerator OnStunned()
    {
        isStunned = true;
        yield return new WaitForSeconds(1);
        isStunned = false;
    }

}