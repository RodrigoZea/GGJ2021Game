using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Spawn : MonoBehaviour
{
    public GameObject[] enemies;
    public int enemiesAmount;
    public int xRange;
    public int yRange;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemiesAmount; i++)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(transform.position.x + Random.Range(-xRange,xRange), transform.position.y + Random.Range(-yRange,yRange)), Quaternion.identity);
            
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
