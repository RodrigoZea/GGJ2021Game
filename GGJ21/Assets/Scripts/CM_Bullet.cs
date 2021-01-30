using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM_Bullet : MonoBehaviour
{
    public float time;
    void Start(){
        StartCoroutine(TimeOfDeath());
    }
    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other.gameObject.name);
        StopCoroutine(TimeOfDeath());
        Destroy(gameObject);
    }

    IEnumerator TimeOfDeath(){
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
