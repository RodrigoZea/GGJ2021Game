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

public class E_EnemyController : MonoBehaviour
{

    GameObject player;
    public EnemyState currentState = EnemyState.Wander;
    public float range;
    public float speed;
    public float attackRange = 0.5f; 
    public float coolDown;
    private bool chooseDirection = false;
    private bool dead = false;
    private Vector3 randomDirecton;
    private bool coolDownAttack = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
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
        randomDirecton = new Vector3(0,0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDirecton);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDirection = false;
    }

    void Wander()
    {
        if(!chooseDirection)
        {
            StartCoroutine(ChooseDirection());
        }


        transform.position += -transform.right * speed * Time.deltaTime;

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
            Debug.Log("Attack!");
            StartCoroutine(CoolDown());
        }     

    }

    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;

    }

    public void Death()
    {
        Destroy(gameObject);
    }

}
 