using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        // Start the event coroutine
        StartCoroutine(EventCoroutine());
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

            CheckforT();
            // Deactivate the event

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

        // Activate the father figure (raise from below the ground)
        fatherFigure.SetActive(true);
        playMode = true;
        // Rotate the father figure over the specified rotation time
        StartCoroutine(RotateFatherFigure());

        // Lerp and raise the father figure
        StartCoroutine(LerpAndRaiseFatherFigure(fatherFigure.transform));

        // Rotate the father figure towards the player
        RotateFatherFigure(fatherFigure.transform);

        // Instantiate targets at the origin (0, 0, 0)
        Instantiate(targets, Vector3.zero, Quaternion.identity);
    }

    private void DeactivateEvent()
    {
        // Deactivate the father figure (go back down)

        brother.gameObject.GetComponent<Brotherscript>().playMode = false;
        fatherFigure.SetActive(false);
        playMode = false;

        // Resume the game (player and AI can continue playing catch)
        // Implement any additional logic you need for game resumption
    }

    private IEnumerator RotateFatherFigure()
    {
        float elapsedTime = 0f;

        while (elapsedTime < 15)
        {
            // Rotate the father figure towards the player
            fatherFigure.transform.LookAt(player.position);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator LerpAndRaiseFatherFigure(Transform fatherTransform)
    {
        float elapsedTime = 0f;

        while (elapsedTime < lerpTime)
        {
            // Lerp the position to raise the father figure
            fatherTransform.position = Vector3.Lerp(fatherTransform.position, new Vector3(0, 0, raiseHeight), elapsedTime / lerpTime);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    private void RotateFatherFigure(Transform fatherTransform)
    {
        // Rotate the father figure towards the player
        fatherTransform.LookAt(player.position, Vector3.up);
    }

}
