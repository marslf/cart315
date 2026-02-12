using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallScript : MonoBehaviour {
    // THIS BALL IS ALL POWERFUL IT CONTROLS TIME, SPACE, AND SCORING
    
    public float ballSpeed = 2;
    public float minSpeed = 1.5f;
    public float maxSpeed = 6f;

    public float minScale = 0.5f;
    public float maxScale = 1.8f;

    private int[] directionOptions = {-1, 1};
    private int hDir, vDir;

    public int score1, score2;
    public AudioSource blip;

    private Rigidbody2D rb;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Reset();
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
        
        MutateBall();
    }

    void MutateBall() {

        // Randomize speed
        ballSpeed += Random.Range(-1f, 1f);
        ballSpeed = Mathf.Clamp(ballSpeed, minSpeed, maxSpeed);

        // Apply the new velocity while keeping direction
        rb.linearVelocity = rb.linearVelocity.normalized * ballSpeed;


        // Randomize size
        float randomScale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(randomScale, randomScale, 1);


        // Slight pitch variation
        blip.pitch = Random.Range(0.6f, 1.3f);
        
        //change colour
        GetComponent<SpriteRenderer>().color = 
            new Color(Random.value, Random.value, Random.value);
    }

    private IEnumerator Launch() {

        hDir = directionOptions[Random.Range(0, directionOptions.Length)];
        vDir = directionOptions[Random.Range(0, directionOptions.Length)];

        yield return new WaitForSeconds(1);

        rb.linearVelocity = new Vector2(hDir, vDir).normalized * ballSpeed;
    }


    void Reset() {
        rb.linearVelocity = Vector2.zero;
        transform.localPosition = new Vector3(0, 0, 0);

        // reset size
        transform.localScale = Vector3.one;

        ballSpeed = 2;
        // Launch
        StartCoroutine(Launch());
    }
}
