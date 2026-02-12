using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallScript : MonoBehaviour {
    // THIS BALL IS ALL POWERFUL IT CONTROLS TIME, SPACE, AND SCORING
    
    public float ballSpeed = 0.4f;
    private int[] directionOptions = {-1, 1};
    private int hDir, vDir;
    
    public int score1, score2;
    public AudioSource blip;
    
    private Rigidbody2D rb;
    
    //FIXING FALSE STARTS
    private bool ballLaunched = false;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Reset();
    }

    //GRAVITY FLIP INPUT
    void Update()
    {
        if (ballLaunched && Input.GetKeyDown(KeyCode.Space))
        {
            FlipVertical();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D wall) {

        if (wall.gameObject.name == "leftWall") {
            // give points to Player 2
            score2 += 1;
            Reset();
        }
        if (wall.gameObject.name == "rightWall") {
            // give points to Player 1
            score1 += 1;
            Reset();
        }

        if (wall.gameObject.name == "topWall" || wall.gameObject.name == "bottomWall") {
            blip.pitch = 0.75f;
            blip.Play();
            // blip.pitch = 1;
        } 
        
        if (wall.gameObject.name == "paddleLeft" || wall.gameObject.name == "paddleRight") {
            blip.pitch = 1f;
            blip.Play();
        } 
        
        
    }


    private IEnumerator Launch() {
        // Launch in random horizontal direction, vertical starts 0
        hDir = directionOptions[Random.Range(0, directionOptions.Length)];
        vDir = directionOptions[Random.Range(0, directionOptions.Length)];

        ballLaunched = true;

        rb.linearVelocity = new Vector2(hDir * ballSpeed, vDir * ballSpeed);
        
    }

    void Reset() {
        //RESET BALL LAUNCH
        ballLaunched = false;
        rb.linearVelocity = Vector2.zero;
        this.transform.localPosition = new Vector3(0, 0, 0);
        // Launch
        StartCoroutine(Launch());
    }
    
    //GRAVITY FLIP
    void FlipVertical()
    {
        // Flip only the vertical direction
        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,// keep horizontal
            -rb.linearVelocity.y //flip vertical
        );
    }
    
    //CONSTANT HORIZONTAL SPEED + SWAP VERTICAL
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            Mathf.Sign(rb.linearVelocity.x) * ballSpeed,  // constant horizontal speed
            Mathf.Sign(rb.linearVelocity.y) * ballSpeed   // vertical can be + or - depending on direction
        );
    }
    
}
