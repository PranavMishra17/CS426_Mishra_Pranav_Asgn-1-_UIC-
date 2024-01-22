// shoot
// using __ imports namespace
// Namespaces are collection of classes, data types
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehavior is the base class from which every Unity Script Derives
public class CharacterController : MonoBehaviour
{
    public float speed = 25.0f;
    public float rotationSpeed = 90;
    public float force = 700f;
    public float bulletSpeed = 500;

    public GameObject cannon;
    public GameObject bullet;
    public AudioClip shootAudio; // Assign the shoot audio clip in the Unity Editor

    private AudioSource audioSource;

    Rigidbody rb;
    Transform t;
    public Score scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
        // Initialize the AudioSource component
        audioSource = GetComponent<AudioSource>();
        scoreManager = FindObjectOfType<Score>().GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        // Time.deltaTime represents the time that passed since the last frame
        // Rotation based on mouse movement
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * mouseX * rotationSpeed);

        // Movement based on WASD keys
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;
        transform.Translate(movement * speed * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Space))
            rb.AddForce(t.up * force);

        // https://docs.unity3d.com/ScriptReference/Input.html
        if (Input.GetButtonDown("Fire1")){
            GameObject newBullet = GameObject.Instantiate(bullet, cannon.transform.position, cannon.transform.rotation) as GameObject;
            //newBullet.GetComponent<Rigidbody>().velocity += Vector3.up * 2;
            newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * bulletSpeed);

            audioSource.PlayOneShot(shootAudio); // Play the shoot audio
            Destroy(newBullet, 5f);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bulletb")
        {
            scoreManager.SubPoint();
        }
    }
}