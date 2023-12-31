using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 목표 : 적의 FSM 다이어그램에 따라 동작시키고 싶다.
// 필요속성1 : 적 상태 

// 목표2 : 플레이어와의 거리를 측정해서 특정 상태로 만들어 준다.
// 필요속성2 : 플레이어와의 거리, 플레이어 트랜스폼

// 목표3 : 적의 상태가 Move일 때, 플레이어와의 거리가 공격 범위 밖이면 적이 플레이어를 따라가고
// 공격 범위 내로 들어오면 공격으로 상태를 전환한다.
// 필요속성3 : 이동속도, 적의 이동을 위한 캐릭터컨트롤러, 공격범위

// 목표4 : 플레이어가 공격 범위 안에 들어올시 특정 시간에 한번씩 attackPower의 힘으로 공격한다.
// 필요속성4 : 현재시간, 특정공격딜레이, attackPower

// 목표5 : 플레이어를 따라가다가 초기 위치에서 일정 거리를 벗어나면 Return 상태로 전환한다.
// 필요속성5 : 초기위치, 이동가능 범위

// 목표6 : 초기 위치로 돌아온다. 특정거리 이내면, Idle 상태로 전환한다.
// 필요속성6 : 특정거리

// 목표7 : 플레이어의 공격을 받으면 hitDamage만큼 에너미의 hp를 감소시킨다
// 필요속성7 : hp

// 목표8 : 에너미의 체력이 0보다 크면 피격상태로 전환
// 목표9 : 그렇지 않으면 죽음으로 전환
public class EnemyFSM : MonoBehaviour
{
    // 필요속성1 : 적 상태 
    enum EnemyState //열거형
    {
        Idle,
        Move,
        Attack,
        Return,
        Damage,
        Die
    }
    EnemyState enemyState;

    // 필요속성2 : 플레이어와의 거리, 플레이어 트랜스폼
    public float findDistance = 5;
    Transform player;

    // 필요속성3 : 이동속도, 적의 이동을 위한 캐릭터컨트롤러
    public float moveSpeed;
    CharacterController characterController;
    public float attackDistance = 2.0f;

    // 필요속성4 : 현재시간, 특정공격딜레이
    float currentTime;
    public float attackDelay = 2.0f;
    // 에너미의 공격력
    public int attackPower = 3;

    // 필요속성5 : 초기위치, 이동가능 범위
    Vector3 originPos;
    public float moveDistance;

    // 필요속성6 : 특정거리
    float returnDistance = 0.1f;

    // 필요속성7 : hp
    public float hp = 3f;

    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.Idle;

        player = GameObject.Find("Player").transform;

        characterController = GetComponent<CharacterController>();

        originPos = player.position;
    }

    // Update is called once per frame
    void Update()
    {

        // 목적 : 적의 FSM 다이어그램에 따라 동작시키고 싶다.
        switch (enemyState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damage:
                /*Damaged();*/
                break;
            case EnemyState.Die:
                /*Die();*/
                break;
        }
    }

    IEnumerator DieProcess()
    {
        yield return new WaitForSeconds(1);

        print("사망");
        Destroy(gameObject);
    }


    // 목표7 : 플레이어의 공격을 받으면 Damage만큼 에너미의 hp를 감소시킨다
    // 목표8 : 에너미의 체력이 0보다 크면 피격상태로 전환
    // 목표9 : 그렇지 않으면 죽음으로 전환
    public void DamageAction(int damage)
    {
        // 만약, 이미 에너미가 피격됐거나, 사망 상태라면 데미지를 주지 않는다.
        if (enemyState == EnemyState.Damage || enemyState == EnemyState.Die)
        {
            return;
        }
        // 플레이어의 공격력 만큼 hp를 감소
        hp -= damage;

        // 목표8 : 에너미의 체력이 0보다 크면 피격상태로 전환
        if (hp > 0)
        {
            enemyState = EnemyState.Damage;
            print("상태 전환: Any state -> Damage");
            Damaged();

        }
        // 목표9 : 그렇지 않으면 죽음으로 전환
        else
        {
            enemyState = EnemyState.Die;
            print("상태 전환: Any state -> Die");
            StartCoroutine(DieProcess());

        }

    }

    private void Damaged()
    {
        // 피격 모션 0.5

        // 피격 상태 처리를 위한 코루팅 실행
        StartCoroutine(DamageProssece());
    }

    // 데미지 처리용
    IEnumerator DamageProssece()
    {
        // 피격 모션 시간만큼 기다린다.
        yield return new WaitForSeconds(0.5f);

        //현재 상태를 이동 상태로 전환한다.
        enemyState = EnemyState.Move;
        print("상태 전환: Damage -> Move");
    }

    // 목표5 : 플레이어를 따라가다가 초기 위치에서 일정 거리를 벗어나면 Return 상태로 전환한다.
    private void Return()
    {
        float distanceToOriginPos = (originPos - transform.position).magnitude;
        //특정 거리 이내면, Idle 상태로 전환하다.
        if (distanceToOriginPos > returnDistance)
        {
            Vector3 dir = (originPos - transform.position).normalized;
            characterController.Move(dir * moveSpeed * Time.deltaTime);
        }
        else
        {
            enemyState = EnemyState.Idle;
            print("상태 전환: Return -> Idle");

        }

    }

    // 목표4 : 플레이어가 공격 범위 안에 들어올시 특정 시간에 한번씩 공격한다.
    private void Attack()
    {
        // 플레이어와의 거리가 공격 범위 내에 있으면 적이 플레이어를 따라간다.
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < attackDistance)
        {
            currentTime += Time.deltaTime;
            //특정 시간에 한번씩 공격한다
            if (currentTime > attackDelay)
            {
                player.GetComponent<PlayerMove>().DamageAction(attackPower);
                print("공격!");
                currentTime = 0;
            }
        }
        else
        {
            // 그렇지 않으면 Move로 상태를 전환한다.
            enemyState = EnemyState.Move;
            print("상태 전환: Attack -> Move");
            currentTime = 0;

        }
    }

    // 목표3 : 적의 상태가 Move일 때, 플레이어와의 거리가 공격 범위 밖이면 적이 플레이어를 따라간다.
    private void Move()
    {
        // 플레이어와의 거리가 공격 범위 내에 있으면 적이 플레이어를 따라간다.
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 목표5 : 플레이어를 따라가다가 초기 위치에서 일정 거리를 벗어나면 초기위치로 돌아온다.
        float distanceToOriginPos = (originPos - transform.position).magnitude;
        if (distanceToOriginPos > moveDistance)
        {
            enemyState = EnemyState.Return;
            print("상태 전환: Move -> Return");
        }
        else
        {
            if (distanceToPlayer > attackDistance)
            {
                Vector3 dir = (player.position - transform.position).normalized;

                // 플레이어를 따라간다.
                characterController.Move(dir * moveSpeed * Time.deltaTime);
            }
            else
            {
                enemyState = EnemyState.Attack;
                print("상태 전환: Move -> Attack");
                currentTime = attackDelay;
            }
        }


        /*        if (distanceToPlayer > findDistance)
                {
                    enemyState = EnemyState.Idle;
                    print("상태 전환: Move -> Idle");
                }*/



    }

    // 목표2 : 플레이어와의 거리를 측정해서 특정 상태로 만들어 준다.
    private void Idle()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        /*float distanceToPlayer = (player.position - transform.position).magnitude;*/

        //현재 플레이어와의 거리가 특정 범위 내면 상태를 Move로 바꿔준다.
        if (distanceToPlayer < findDistance)
        {
            enemyState = EnemyState.Move;
            print("상태전환: Idle -> Move");
        }
    }
}
