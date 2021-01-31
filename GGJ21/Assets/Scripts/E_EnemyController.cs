using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum  EnemyState
{
    Wander,

    Follow,

    Die,

    Attack
    
};

public enum EnemyType
{
    Melee,

    Ranged
};

public class E_EnemyController : MonoBehaviour
{

    GameObject player;
    public EnemyState currentState = EnemyState.Wander;
    public EnemyType enemyType;
    public float range;
    public float speed;
    public float attackRange = 0.5f; 
    public float coolDown;
    public GameObject bulletPrefab; 
    public Animator animator;
    public int health;
    public GameObject heartPrefab;
    private bool chooseDirection = false;
    private bool isDead = false;
    private Vector3 randomDirecton;
    private bool coolDownAttack = false;
    private float oldX;
    private Vector3 tempDestination;
    private RG_Room currentRoom;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boxCollider = GetComponent<BoxCollider2D>();
        currentRoom = RG_CameraController.instance.currentRoom;
        oldX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (oldX < transform.position.x)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            oldX = transform.position.x;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            oldX = transform.position.x;
        }

        switch (currentState)
        {
            case(EnemyState.Wander):
                Wander();
                break;

            case(EnemyState.Follow):
                Follow();
                break;
            case(EnemyState.Die):
                break;
            case(EnemyState.Attack):
                Attack();
                break;
        }


        if (IsPlayerInRange(range) && currentState != EnemyState.Die)
        {
            currentState = EnemyState.Follow;
        }
        else if(!IsPlayerInRange(range) && currentState != EnemyState.Die)
        {
            currentState = EnemyState.Wander;
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            currentState = EnemyState.Attack;

        }

        
    }

    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection()
    {
        chooseDirection = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        //randomDirecton = new Vector3(0,0, Random.Range(0, 360));
        //Quaternion nextRotation = Quaternion.Euler(randomDirecton);
        //transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        tempDestination = new Vector3(transform.position.x - Random.Range(-360,360),
            transform.position.y - Random.Range(-360, 360),
            transform.position.z - Random.Range(-360, 360) );
        
        chooseDirection = false;
    }

    void Wander()
    {
        if(!chooseDirection)
        {
            StartCoroutine(ChooseDirection());
        }

        transform.position = Vector2.MoveTowards(transform.position, tempDestination, speed * Time.deltaTime);
        //transform.position += -transform.right * speed * Time.deltaTime;

        if (IsPlayerInRange(range))
        {
            currentState = EnemyState.Follow;
        }



    }

    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void Attack()
    {

        if ( !coolDownAttack)
        {
            switch (enemyType)
            {
                case(EnemyType.Melee):
                    Debug.Log("Attack Melee!");
                    player.gameObject.GetComponent<CM_PlayerMovement>().Damage(1);
                    StartCoroutine(CoolDown());
                    break;
                case(EnemyType.Ranged):
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<E_BulletController>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<E_BulletController>().isEnemyBullet = true;
                    StartCoroutine(CoolDown());
                    break;

                
            }
        }     

    }

    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;

    }

    private IEnumerator Death(){
        isDead = true;
        boxCollider.enabled = false;
        animator.SetBool("Death", true);
        yield return new WaitForSeconds(1.3f);
        if(Random.Range(0.0f, 1.0f) > 0.4f)
        {
            Instantiate(heartPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    public void Damage(int damage)
    {
        if (!isDead)
        {
            if (health > 0)
                health -= damage;

            if(health == 0)
            {
                currentRoom.EnemyKilled();
                StartCoroutine(Death());
            }
        }
    }

     

}
 