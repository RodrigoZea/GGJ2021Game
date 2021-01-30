using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM_PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement, shoot, look;

    public GameObject bulletPrefab;
    public Transform bulletPoint;
    public float fireDelay;
    private float lastFire;
    //private bool dash = false;

    CM_Shooting shooting;

    void Start(){
        shooting = GetComponent<CM_Shooting>();
        look.Set(0, 1);

    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        shoot.x = Input.GetAxisRaw("ShootHorizontal");
        shoot.y = Input.GetAxisRaw("ShootVertical");
        //if (Time.time > lastFire + fireDelay){
        if (shoot.x != 0 || shoot.y != 0){
            if (Input.GetButtonDown("ShootHorizontal")){
                look.Set(shoot.x, 0);
                MoveBulletPoint();
                shooting.Shoot(bulletPoint, bulletPrefab);
            } else 
            if (Input.GetButtonDown("ShootVertical")) {
                look.Set(0, shoot.y);
                MoveBulletPoint();
                shooting.Shoot(bulletPoint, bulletPrefab);
            }
            Debug.Log(look);
        }

        if (Input.GetButtonDown("Jump") && (Time.time > lastFire + fireDelay)){
            StartCoroutine(Dash());
            lastFire = Time.time;
            //rb.velocity = Vector2.zero;
        }
        Debug.Log(Time.time - lastFire);
        //}
        
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        //bulletPoint.eulerAngles = new Vector2(look.x * 90, look.y * 90); 

    }

    void MoveBulletPoint(){
        float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90f;
        bulletPoint.localPosition = new Vector2(look.x * 0.1f, look.y * 0.1f);
        bulletPoint.eulerAngles = new Vector3(0,0,angle);
    }

    IEnumerator Dash(){
        moveSpeed = 20f;
        yield return new WaitForSeconds(0.3f);
        moveSpeed = 5f;
    }

    
}
