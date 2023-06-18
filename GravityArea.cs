using System.Collections;
using UnityEngine;


// Refactored
public class GravityArea : MonoBehaviour
{


    public GameObject targetObject;

    private GameObject ballTriggered;
    private SpriteRenderer ballSprite;

    private Rigidbody2D rb;
    private bool isGravityFieldActive = false;
    private bool isBallInField = false;
    private bool isFieldAvailable = false;
    private float moveSpeed = 60f;
    private int scorePoints = 60;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball_Green") || col.CompareTag("Ball_Yellow"))
        {
            isFieldAvailable = !isGravityFieldActive && !isBallInField;
            if (isFieldAvailable)
            {

                isGravityFieldActive = true;
                isBallInField = true;
                ballTriggered = GameObject.FindGameObjectWithTag(col.tag);
                rb = ballTriggered.GetComponent<Rigidbody2D>();

                GameObject ballRenderer = ballTriggered.transform.GetChild(0).gameObject;
                ballSprite = ballRenderer.GetComponent<SpriteRenderer>();

                ballSprite.material.color = new Color(0.7f, 0.9f, 0.2f, 1f);
                LabelManager.instance.UpdateStatusText("GRAVITY");
                StartCoroutine(enterGravityArea());
            }

        }



    }

    public void FixedUpdate()
    {
        if (isFieldAvailable)
        {

            if (rb != null && ballTriggered != null)
            {
                rb.AddForce((targetObject.transform.position - ballTriggered.transform.position) * moveSpeed);
            }

        }
    }

    private IEnumerator enterGravityArea()
    {

        yield return new WaitForSeconds(4f);
        ballSprite.material.color = new Color(1f, 1f, 1f, 1f);
        ballTriggered = null;
        isGravityFieldActive = false;
        isBallInField = false;
        ScoreManager.instance.AddPoint(scorePoints);
    }

}


