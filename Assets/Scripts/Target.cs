//lets add some target
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Target : MonoBehaviour
{
    public Score scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<Score>().GetComponent<Score>();
    }
    //this method is called whenever a collision is detected
    private void OnCollisionEnter(Collision collision) {
        //on collision adding point to the score
        //scoreManager.AddPoint();
 
        // printing if collision is detected on the console
        Debug.Log("Collision Detected");
        //after collision is detected destroy the gameobject
        Destroy(gameObject, 5f);
    }
}