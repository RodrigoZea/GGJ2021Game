using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM_Melee : MonoBehaviour
{
    public float attackRange = 1f;
    // Start is called before the first frame update
    public void Attack(Transform attackPoint){
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        foreach(Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.tag == "Enemy"){
                enemy.gameObject.GetComponent<E_EnemyController>().Damage(1);
            }
        }
    }
}
