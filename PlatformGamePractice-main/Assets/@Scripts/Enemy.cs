using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    public double maxHp = 50;
    [SerializeField]
    public double curHp = 50;

    public float turnovertimer = 0;
    public float turnovertime = 2f;

    // 0 = 순찰
    // 1 = 주시
    // 3 = 공격
    public int enemyActionState;

    public bool isAttack;
    public Player player;

    public float lineSize = 16f;

    private EnemyMoveSystem enemyMoveSystem;
    void Start()
    {
        enemyMoveSystem = GetComponent<EnemyMoveSystem>();
        enemyActionState = 0;
        isAttack = false;
    }

    public void Jump()
    {

    }
    void Update()
    {
        if(isAttack)    Turnover();

        if (OnSearchPlayer(player))
        {
            enemyActionState = 1;
        }
        else
        {
            enemyActionState = 0;
        }
        if (StopEnemy(player) && !isAttack)
        {
            enemyActionState = 2;
        }

        switch (enemyActionState)
        {
            case 0:
                enemyMoveSystem.PatrolEnemy();
                break;
            case 1:
                enemyMoveSystem.OnApproachstate(player);
                break;
            case 2:
                AttackToPlayer(player);
                break;
        }
    }

    private void Turnover()
    {
        turnovertimer += Time.deltaTime;
        if (turnovertimer > turnovertime)
        {
            turnovertimer = 0;
            isAttack = false;
        }
    }

    public bool OnSearchPlayer(Player player)
    {
        if (player.tag == "Player")
        {
            Vector3 dir = enemyMoveSystem.rigid.position - player.playerinputSystem.playerPos;
            if (Mathf.Abs(Vector3.Magnitude(dir)) >= 50)
            {
                return true;
            }
        }
        return false;
    }

    public bool StopEnemy(Player player)
    {
        Vector3 dir = enemyMoveSystem.rigid.position - player.playerinputSystem.playerPos;
        if (Mathf.Abs(Vector3.Magnitude(dir)) <= 4)
        {
            return true;
        }
        return false;
    }

    public void AttackToPlayer(Player player)
    {
        player.curHp--;
        isAttack = true;
    }
}
