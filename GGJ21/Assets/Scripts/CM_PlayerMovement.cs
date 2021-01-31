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
    public Animator animator;

    CM_Shooting shooting;
    CM_Melee melee;

    private bool isAttacking;
    public static CM_PlayerMovement instance;

    private GameManager gameManager;

    void Awake()
    {
        instance = this;
    }

    void Start(){
        gameManager = GameManager.GetInstance();

        shooting = GetComponent<CM_Shooting>();
        melee = GetComponent<CM_Melee>();
        look.Set(0, 1);

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetLives() > 0){
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            shoot.x = Input.GetAxisRaw("ShootHorizontal");
            shoot.y = Input.GetAxisRaw("ShootVertical");

            look = movement;
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
            }

            if (Input.GetButtonDown("Jump") && gameManager.CanDash()){
                gameManager.UseDash();
                StartCoroutine(Dash());
            }
            //Debug.Log(Time.time - lastFire);
        
            if (Input.GetButtonDown("Fire2") && gameManager.CanMelee()){
                Debug.Log("Attack");
                gameManager.UseMelee();
                StartCoroutine(Attack());
            }

            if (isAttacking)
            {
                melee.Attack(transform);
            }
            
            animator.SetFloat("Horizontal", look.x);
            animator.SetFloat("Vertical", look.y);
            animator.SetFloat("Speed", look.sqrMagnitude);
        }
        else
        {
            movement.x = 0;
            movement.y = 0;

            shoot.x = 0;
            shoot.y = 0;
        }

    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void MoveBulletPoint(){
        float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90f;
        bulletPoint.localPosition = new Vector2(look.x * 3.0f, look.y * 3.0f);
        bulletPoint.eulerAngles = new Vector3(0,0,angle);
    }

    IEnumerator Dash(){
        moveSpeed = 20f;
        yield return new WaitForSeconds(0.3f);
        moveSpeed = 5f;
    }
    
    IEnumerator Attack(){
        animator.SetBool("Attack", true);
        isAttacking = true;
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Attack", false);
        isAttacking = false;
    }
    private void Death(){
        animator.SetBool("Death", true);
    }

    public void Damage(int damage){

        if (isAttacking)
        {
            return;
        }

        int lives = gameManager.DamagePlayer(damage);

        if(lives <= 0)
        {
            Death();
        }
    }

    
}
