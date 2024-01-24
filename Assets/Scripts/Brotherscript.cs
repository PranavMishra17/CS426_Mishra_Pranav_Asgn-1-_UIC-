using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Brotherscript : MonoBehaviour
{

    public GameObject cannon;
    public GameObject bullet;
    public Transform player;

    Rigidbody rb;
    Transform t;

    public float bulletSpeed = 500;
    public float moveRadius = 20;
    public float moveSpeed = 20;
    public float rotSpeed = 100;

    public bool playMode = true;

    private float dashSpeed;
    private float dashLength;
    private float dashFrequency;
    private float dashTimer;
    private Vector3 startPosition;
    private int dashDirection = 1;
    public AudioClip shootAudio; // Assign the shoot audio clip in the Unity Editor

    private AudioSource audioSource;
    public Score scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();

        GenerateRandomDashParameters(); // Set initial random parameters
        startPosition = transform.position; // Store the starting position

        // Start the shooting coroutine
        StartCoroutine(ShootAtPlayerPeriodically());

        audioSource = GetComponent<AudioSource>();
        scoreManager = FindObjectOfType<Score>().GetComponent<Score>();
    }

    private void FixedUpdate()
    {
        if (playMode)
        {
            Dash();
        }

    }

    private IEnumerator ShootAtPlayerPeriodically()
    {
        while (playMode)
        {
            yield return new WaitForSeconds(0.5f);

            // Aim at the player
            AimAtPlayer();

            // Wait for 5-6 seconds before shooting again
            yield return new WaitForSeconds(Random.Range(3f, 4f));

            // Shoot a bullet
            ShootBullet();
        }
    }

    public void startShoot() { StartCoroutine(ShootAtPlayerPeriodically()); }

    public float maxDistanceFromOrigin = 10f; // Set the maximum distance from the origin

    private void Dash()
    {
        // Update the dash timer
        dashTimer += Time.deltaTime;

        // Check if the dash is complete
        if (dashTimer >= 1 / dashFrequency)
        {
            // Set new random parameters for the next dash
            GenerateRandomDashParameters();
            dashTimer = 0f;

            // Store the current position as the starting position for the next dash
            startPosition = transform.position;

            // Change direction to the opposite of the former
            dashDirection *= -1;

            // Ensure the brother doesn't go beyond the maximum distance from the origin
            float distanceFromOrigin = Vector3.Distance(Vector3.zero, transform.position);
            if (distanceFromOrigin > maxDistanceFromOrigin)
            {
                // Move the brother back within the allowed distance
                transform.position = Vector3.ClampMagnitude(transform.position, maxDistanceFromOrigin);
            }
        }

        // Calculate the position offset based on linear interpolation
        float t = dashTimer * dashFrequency;
        float offset = Mathf.Lerp(0f, dashLength, t);

        // Move sideways during the dash with the updated direction
        Vector3 sidewaysMovement = transform.right * offset * dashSpeed * dashDirection;

        // Move to the new position (startPosition + sidewaysMovement)
        transform.position = startPosition + sidewaysMovement;
    }



    private void GenerateRandomDashParameters()
    {
        // Generate random parameters for each dash
        dashSpeed = Random.Range(0.5f, 2f); // Adjust as needed
        dashLength = Random.Range(2f, 5f); // Adjust as needed
        dashFrequency = Random.Range(2f, 4f); // Adjust as needed
    }

    private void AimAtPlayer()
    {
        // Your existing code to aim at the player
        Vector3 directionToPlayer = (player.position - cannon.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        cannon.transform.rotation = Quaternion.Lerp(cannon.transform.rotation, lookRotation, Time.deltaTime * rotSpeed);

        // Shooting logic with a pause
        StartCoroutine(ShootWithDelay());
    }

    private IEnumerator ShootWithDelay()
    {
        // Shoot the first bullet
        ShootBullet();

        // Wait for half a second
        yield return new WaitForSeconds(0.5f);

        // Shoot the second bullet
        ShootBullet();
    }


    private void ShootBullet()
    {
        // Instantiate and shoot a bullet
        GameObject newBullet = Instantiate(bullet, cannon.transform.position, cannon.transform.rotation) as GameObject;
        newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * bulletSpeed);
        audioSource.PlayOneShot(shootAudio); // Play the shoot audio
        Destroy(newBullet, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet") 
        {
            ShootBullet();
            scoreManager.AddPoint();
        }
    }
}
