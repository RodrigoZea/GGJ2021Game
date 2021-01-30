using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM_Shooting : MonoBehaviour
{
    public float bulletForce = 20f;



    public void Shoot(Transform bulletPoint, GameObject bulletPrefab){
        GameObject bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletPoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
