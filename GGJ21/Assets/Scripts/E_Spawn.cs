using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Spawn : MonoBehaviour
{
    public GameObject[] enemies;
    public int enemiesAmount;
    public int xRange;
    public int yRange;
    public static E_Spawn instance;

    void Awake()
    {
        instance = this;
    }

    public void Spawn()
    {
        for (int i = 0; i < enemiesAmount; i++)
        {
            GameObject enemy = Instantiate(
                enemies[Random.Range(0, enemies.Length)],
                new Vector3(
                    transform.position.x + Random.Range(-xRange, xRange),
                    transform.position.y + Random.Range(-yRange, yRange)),
                Quaternion.identity);
            
            enemy.transform.parent = this.gameObject.transform.parent;
        }

        Destroy(this);
    }
}
