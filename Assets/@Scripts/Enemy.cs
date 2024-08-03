using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 0 = 순찰
    // 1 = 주시
    // 3 = 공격
    public int enemyActionState;

    private RaycastHit hit;

    public float lineSize = 16f;

    private EnemyMoveSystem enemyMoveSystem;
    void Start()
    {
        enemyMoveSystem = GetComponent<EnemyMoveSystem>();
        enemyActionState = 0;
    }

    public void Jump()
    {
        
    }
    void Update()
    {
        if (SearchPlayer())
        {
            enemyActionState = 1;
        }
        if (enemyActionState == 0)
        {
            enemyMoveSystem.PatrolEnemy();
        }
        else if (enemyActionState == 1)
        {
            enemyMoveSystem.ApproachEnemy(hit);
        }
        else if (enemyActionState == 2)
        {
            
        }
    }

    bool SearchPlayer()
    {
        if (Physics.Raycast(enemyMoveSystem.rigid.position, Vector3.forward * 16f, out hit))
        {
            Debug.DrawRay(enemyMoveSystem.rigid.position, Vector3.forward * 16f, Color.red);
            RaycastHit2D rayHit = Physics2D.Raycast(enemyMoveSystem.rigid.position, Vector3.forward * 16f, 1, LayerMask.GetMask("Platform"));
            if (hit.collider.tag.Equals("Player")) return true;
        }

        return false;
    }
}
