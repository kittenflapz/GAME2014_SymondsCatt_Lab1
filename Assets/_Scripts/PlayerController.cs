/* 
 Filename: PlayerController.cs
 Author: Catt Symonds
 Student Number: 101209214
 Date Last Modified: 17/10/2020
 Description: Player controller script to parse user input and move the player
 Revision History: 
 11/10/2020: File created as simple player controller
 12/10/2020: Bug fixing.
 12/10/2020: Changed areas of screen that are touchable as game input. Added attack audio.
 15/10/2020: Added handler for being stunned by enemy
 16/10/2020: Added attack in radius function
 16/10/2020: Overhauled points system, time is now added to timer when player kills an enemy
 17/10/2020: Added cooldown to attack
 17/10/2020: Added treat pickups
 17/10/2020: Added PlayerPrefs saving using new ScoreKeeper.cs script
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 touchesEnd = Vector3.zero;

    // Movement

    [SerializeField]
    private float horizontalBoundary;

    [SerializeField]
    private float horizontalSpeed;

    [SerializeField]
    private float verticalSpeed;

    [SerializeField]
    private float maxSpeed;

    [SerializeField]
    private float leftTrack; // the x point on the screen that the player goes when player holds left of screen

    [SerializeField]
    private float rightTrack;

    public Transform gameInputVerticalCutoff;
    public float movementZoneStart;

    // Attacks

    [SerializeField]
    private float attackRadius;
    [SerializeField]
    private float attackCooldown;
    private float nextAttackTime;

    // Enemy detection

    private int enemiesInRadius;
    public SpriteRenderer attackRadiusIndicator;

    private bool beingAttacked;

   
    public bool BeingAttacked
    {
        get
        {
            return beingAttacked;
        }
        set
        {
            beingAttacked = value;
        }
    }

    // Animation

    public Animator animator;

    // Sound
    public AudioSource meow;

   
    // Misc
    private BoxCollider2D playerCollider;
    [SerializeField]
    private float treatRadius;
    public TreatManager treatManager;


    // Game management
    public GameManager gameManager;
    private bool isDead;


    // Saving/Loading
    public ScoreKeeper scoreKeeper;
    private int squirrelsSpooked = 0;
    private int treatsEaten = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        meow = GetComponent<AudioSource>();
        playerCollider = GetComponentInChildren<BoxCollider2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
        treatManager = GameObject.Find("TreatManager").GetComponent<TreatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
           CheckIfEnemiesInRadius();
           ParseInput();
           TreatCheck();
        }
    }

    // To see if there are enemies nearby - changes timer speed
    private void CheckIfEnemiesInRadius()
    {
        if (enemiesInRadius > 0)
        {
            beingAttacked = true;
        }
        else
        {
            beingAttacked = false;
        }
    }

    // Increments an int if there are enemies nearby. This is to prevent buggy behaviour when using a bool, since if 
    // any enemy exits the radius, the player would no longer register any enemies even if there are still enemies
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemiesInRadius++;
        }
    }

    // Decrements same int
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemiesInRadius--;
        }
    }


    // Parsing touch input from the player
    private void ParseInput()
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
                    if (Time.time > nextAttackTime)
                    {
                        nextAttackTime = Time.time + attackCooldown;
                        Attack();
                    }
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
            Attack();
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

    // Handles attack with animation, sound, and checking whether the attack hits anything
    private void Attack()
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
            gameManager.AddTime(3);
            squirrelsSpooked++;
        }
    }

    //public void Stunned()
    //{
    //    StartCoroutine(OnStunned());
    //}

        // Kills the player

    public void Kill()
    {
        isDead = true;
        scoreKeeper.SaveSquirrelsSpooked(squirrelsSpooked);
        scoreKeeper.SaveTreatsEaten(treatsEaten);
    }


    // Checks if there are treats in the player's treat-eating radius
    public void TreatCheck()
    {
        LayerMask mask = LayerMask.GetMask("Treat");

        // Check a circle around the player and get all treat colliders overlapping the circle
        Collider2D[] collidersInTreatRadius= Physics2D.OverlapCircleAll(transform.position, treatRadius, mask);
        foreach(Collider2D collider in collidersInTreatRadius)
        {
            treatManager.ReturnTreat(collider.gameObject);
            // A treat has been destroyed, so spawn a new one at least two screen's height away from the character
            Vector2 newTreatPos = new Vector2(Random.Range(-2f, 2f), Random.Range(transform.position.y + 7f, transform.position.y + 14f));
            GameObject newTreat = treatManager.GetTreat(newTreatPos);
            gameManager.AddTime(10);
            treatsEaten++;
        }

    }

    //public IEnumerator OnStunned()
    //{
    //    isStunned = true;
    //    yield return new WaitForSeconds(1);
    //    isStunned = false;
    //}

}