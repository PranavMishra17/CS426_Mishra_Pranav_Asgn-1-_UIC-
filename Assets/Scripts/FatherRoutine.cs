using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FatherRoutine : MonoBehaviour
{
    public Transform player;
    public Transform brother;
    public GameObject fatherFigure;
    public GameObject targets;

    public float rotationTime = 20f;
    public float eventIntervalMin = 15f;
    public float eventIntervalMax = 20f;
    public float lerpTime = 5f; // Time to lerp the father figure
    public float raiseHeight = 15f;

    public bool playMode = true;

    public TMP_Text timerText;
    public GameObject timerUI;
    public GameObject redLight;

    private Vector3 originalFatherPosition;
    private Quaternion originalFatherRotation;

    public AudioClip panicAudio; // Assign the shoot audio clip in the Unity Editor

    private AudioSource audioSource;

    private void Start()
    {
        // Start the event coroutine
        StartCoroutine(EventCoroutine());

        // Store the original position and rotation of the father
        originalFatherPosition = fatherFigure.transform.position;
        originalFatherRotation = fatherFigure.transform.rotation;

        audioSource = player.gameObject.GetComponent<AudioSource>();
    }

    private IEnumerator EventCoroutine()
    {
        while (playMode)
        {
            // Wait for a random interval between 15 and 20 seconds
            yield return new WaitForSeconds(Random.Range(eventIntervalMin, eventIntervalMax));

            // Activate the event
            ActivateEvent();

            // Wait for the rotation time
            yield return new WaitForSeconds(rotationTime);

            //CheckforT();
            // Deactivate the event
            DeactivateEvent();

        }
    }
    private void CheckforT()
    {
        if(GameObject.FindGameObjectsWithTag("target") == null)
        {
            DeactivateEvent();
        }
        else
        {
            Debug.Log("Game Over");
        }
    }

    private void ActivateEvent()
    {

        brother.gameObject.GetComponent<Brotherscript>().playMode = false;
        audioSource.clip = panicAudio;
        audioSource.loop = true;
        audioSource.Play();
        // Activate the father figure (raise from below the ground)
        fatherFigure.SetActive(true);
        playMode = true;

        fatherFigure.GetComponent<Animation>().PlayQueued("fatherEntry");
        StartCountdown();

        // Instantiate targets at the origin (0, 0, 0)
        Instantiate(targets, Vector3.zero, Quaternion.identity);
    }

    private void DeactivateEvent()
    {

        // Check if there are any remaining targets in the scene
        GameObject[] targets = GameObject.FindGameObjectsWithTag("target");

            if (targets.Length == 0)
            {
            // No targets remaining, reset father to original position and rotation
                fatherFigure.transform.position = originalFatherPosition;
                fatherFigure.transform.rotation = originalFatherRotation;

                // After the countdown
                timerUI.SetActive(false);
                redLight.SetActive(false);
                fatherFigure.SetActive(false);

                brother.gameObject.GetComponent<Brotherscript>().playMode = true;

                playMode = true;

                audioSource.Stop();
                audioSource.loop = false;
                StartCoroutine(EventCoroutine());
                timerText.text = 15.ToString();
                brother.gameObject.GetComponent<Brotherscript>().startShoot();
            }
            else
            {
                // Targets still remaining, print Game Over
                Debug.Log("Game Over");
                timerText.text = "GAME OVER";
            }
    }

    private void StartCountdown()
    {
        // Activate the timer UI and red light
        timerUI.SetActive(true);
        redLight.SetActive(true);


        // Start the countdown coroutine and panic mode coroutine simultaneously
        StartCoroutine(CountdownCoroutine());
        StartCoroutine(PanicModeCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        int timeRemaining = 15;

        while (timeRemaining > 0)
        {
            // Update the timer text
            timerText.text = timeRemaining.ToString();

            // Wait for one second
            yield return new WaitForSeconds(1f);

            // Decrease the time remaining
            timeRemaining--;
        }


    }

    private IEnumerator PanicModeCoroutine()
    {
        while (!playMode)
        {
            // Toggle the state of the red light every half second
            redLight.SetActive(!redLight.activeSelf);

            // Wait for half a second
            yield return new WaitForSeconds(0.5f);
        }
    }

}
