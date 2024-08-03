using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveSystem : MonoBehaviour
{


    // 0 = 왼
    // 1 = 오
    // 2 = 앞
    // 3 = 뒤
    public int enemyMoveState;

    public Rigidbody rigid;
    private RaycastHit hit;
    
    Vector3 forwardRayPos;
    Vector3 backRayPos;
    Vector3 leftRayPos;
    Vector3 rightRayPos;

    public float speed = 5f;
    public float time = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        enemyMoveState = 0;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 2.5f)
        {
            enemyMoveState = Random.Range(0, 3);
            time = 0;
        }

    }

    private void FixedUpdate()
    {
        CheckMovingDir();
    }

    private void LateUpdate()
    {

    }

    // Patrol state
    public void PatrolEnemy()
    {
        // 0 = 왼
        // 1 = 오
        // 2 = 앞
        // 3 = 뒤

        switch (enemyMoveState)
        {
            case 0:
                {
                    transform.Translate(-speed * Time.deltaTime, 0, 0);
                    Debug.Log("왼");
                }
                break;
            case 1:
                {
                    transform.Translate(speed * Time.deltaTime, 0, 0);
                    Debug.Log("오");
                }
                break;
            case 2:
                {
                    transform.Translate(0, 0, speed * Time.deltaTime);
                }
                break;
            case 3:
                {
                    transform.Translate(0, 0, -speed * Time.deltaTime);
                }
                break;
        }

    }

    private void CheckMovingDir()
    {
        // 오른쪽으로 이동할 플랫폼이 없을 때
        if (!HitRightRayCast())
        {
            enemyMoveState = 0;
        }

        // 왼쪽으로 이동할 공간이 없을 때
        if (!HitLeftRayCast())
        {
            enemyMoveState = 1;
        }

        // 뒤로 이동할 공간이 없을 때
        if (!HitBackRayCast())
        {
            enemyMoveState = 2;
        }

        // 앞으로 이동할 공간이 없을 때
        if (!HitFrontRayCast())
        {
            enemyMoveState = 3;
        }
    }

    bool HitLeftRayCast()
    {
        leftRayPos = rigid.position;
        leftRayPos.x -= 1f;

        if (Physics.Raycast(leftRayPos, Vector3.down * 5f, out hit))
        {
            Debug.DrawRay(leftRayPos, Vector3.down, Color.red);
            RaycastHit2D rayHit = Physics2D.Raycast(leftRayPos, Vector3.down, 1, LayerMask.GetMask("Platform"));
        }
        else
        {
            return false;
        }
        return true;
    }
    bool HitRightRayCast()
    {
        rightRayPos = rigid.position;
        rightRayPos.x += 1f;

        if (Physics.Raycast(rightRayPos, Vector3.down * 5f, out hit))
        {
            Debug.DrawRay(rightRayPos, Vector3.down, Color.red);
            RaycastHit2D rayHit = Physics2D.Raycast(rightRayPos, Vector3.down, 1, LayerMask.GetMask("Platform"));
        }
        else
        {
            return false;
        }
        return true;
    }

    bool HitFrontRayCast()
    {
        forwardRayPos = rigid.position;
        forwardRayPos.z += 1f;

        if (Physics.Raycast(forwardRayPos, Vector3.down * 5f, out hit))
        {
            Debug.DrawRay(forwardRayPos, Vector3.down, Color.red);
            RaycastHit2D rayHit = Physics2D.Raycast(forwardRayPos, Vector3.down, 1, LayerMask.GetMask("Platform"));
        }
        else
        {
            return false;
        }
        return true;
    }

    bool HitBackRayCast()
    {
        backRayPos = rigid.position;
        backRayPos.z -= 1f;

        if (Physics.Raycast(backRayPos, Vector3.down * 5f, out hit))
        {
            Debug.DrawRay(backRayPos, Vector3.down, Color.red);
            RaycastHit2D rayHit = Physics2D.Raycast(backRayPos, Vector3.down, 1, LayerMask.GetMask("Platform"));
        }
        else
        {
            return false;
        }
        return true;
    }

    // Approach state

    public void ApproachEnemy(RaycastHit player)
    {
        transform.Translate(player.point.x * Time.deltaTime,
            player.point.y * Time.deltaTime,
            player.point.z * Time.deltaTime);
    }
}
