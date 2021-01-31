using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Heart : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject img;

    private float originalScale;
    private float currentScale;

    private float animationTime = 1.0f;
    private float animationTimer;
    private bool animationForward = true;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetInstance();
        originalScale = img.transform.localScale.x;
        currentScale = originalScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (animationTimer >= animationTime)
        {
            animationForward = false;
        }
        else if (animationTimer < 0)
        {
            animationForward = true;
        }

        if (animationForward)
        {
            animationTimer += Time.deltaTime;
        }
        else
        {
            animationTimer -= Time.deltaTime;
        }

        currentScale = Mathf.Lerp(originalScale, originalScale * 1.1f, animationTimer);

        img.transform.localScale = new Vector3(currentScale, currentScale, 1);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && gameManager.GetLives() < 3)
        {
            Debug.Log("Corazon recogido");
            gameManager.PickUpLife();
            Destroy(gameObject);
        }

    }
}
