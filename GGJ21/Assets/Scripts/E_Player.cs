using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Player : MonoBehaviour
{
    public int health = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DamagePlayer(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Debug.Log("Muerto!");
        }
        else
        {
            Debug.Log("Golpe! ");
        }


    }
}
