using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Heart : MonoBehaviour
{
    public Image img;

    private float originalScale;
    private float currentScale;

    private float animationTime = 1.0f;
    private float animationTimer;
    private bool animationForward = true;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = img.rectTransform.localScale.x;
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



        img.rectTransform.localScale = new Vector3(currentScale, currentScale, 1);
    }
}
