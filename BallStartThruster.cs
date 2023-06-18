using System.Collections;
using UnityEngine;

public class pinBallstartThruster : MonoBehaviour
{
    private GameObject[] pinBalls;
    private Rigidbody2D[] rbs;
    private string[] pinBallsTags = { "Ball_Green", "Ball_Yellow" };
    private bool isThrustActive = false;
    private float maxForceUp = 1000f;

    void Awake()
    {
        pinBalls = new GameObject[2];
        rbs = new Rigidbody2D[2];
    }

    void Start()
    {
        for (int i = 0; i < pinBallsTags.Length; i++)
        {
            pinBalls[i] = GameObject.FindGameObjectWithTag(pinBallsTags[i]);
            rbs[i] = pinBalls[i].GetComponent<Rigidbody2D>();

        }
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Space) && !isThrustActive)
        {
            StartCoroutine(thrustPinBalls());
        }
    }

    IEnumerator thrustPinBalls()
    {
        isThrustActive = true;

        for (int i = 0; i < pinBallsTags.Length; i++)
        {
            rbs[i].AddForce(new Vector2(0, maxForceUp));
        }

        yield return new WaitForSeconds(0.2f);
        isThrustActive = false;
    }

}
