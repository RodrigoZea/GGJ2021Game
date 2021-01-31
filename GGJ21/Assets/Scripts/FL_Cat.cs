using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FL_Cat : MonoBehaviour
{
    public static FL_Cat instance;
    private BoxCollider2D boxCollider;
    void Awake()
    {
        instance = this;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void UpdateTrigger()
    {
        boxCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
            GameManager.GetInstance().Win();
    }
}
