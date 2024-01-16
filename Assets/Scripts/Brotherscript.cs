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

    public bool playMode = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();

        // Start the shooting coroutine
        StartCoroutine(ShootAtPlayerPeriodically());
    }

    private IEnumerator ShootAtPlayerPeriodically()
    {
        while (playMode)
        {
            // Aim at the player
            AimAtPlayer();

            // Wait for 5-6 seconds before shooting again
            yield return new WaitForSeconds(Random.Range(3f, 4f));

            // Shoot a bullet
            ShootBullet();
        }
    }

    private void AimAtPlayer()
    {
        // Calculate the direction to the player
        Vector3 directionToPlayer = (player.position - cannon.transform.position).normalized;

        // Rotate the cannon to aim at the player
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        cannon.transform.rotation = lookRotation;
    }

    private void ShootBullet()
    {
        // Instantiate and shoot a bullet
        GameObject newBullet = Instantiate(bullet, cannon.transform.position, cannon.transform.rotation) as GameObject;
        newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * bulletSpeed);
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
        }
    }
}
