using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ǥ : ���� FSM ���̾�׷��� ���� ���۽�Ű�� �ʹ�.
// �ʿ�Ӽ�1 : �� ���� 

// ��ǥ2 : �÷��̾���� �Ÿ��� �����ؼ� Ư�� ���·� ����� �ش�.
// �ʿ�Ӽ�2 : �÷��̾���� �Ÿ�, �÷��̾� Ʈ������

// ��ǥ3 : ���� ���°� Move�� ��, �÷��̾���� �Ÿ��� ���� ���� ���̸� ���� �÷��̾ ���󰡰�
// ���� ���� ���� ������ �������� ���¸� ��ȯ�Ѵ�.
// �ʿ�Ӽ�3 : �̵��ӵ�, ���� �̵��� ���� ĳ������Ʈ�ѷ�, ���ݹ���

// ��ǥ4 : �÷��̾ ���� ���� �ȿ� ���ý� Ư�� �ð��� �ѹ��� attackPower�� ������ �����Ѵ�.
// �ʿ�Ӽ�4 : ����ð�, Ư�����ݵ�����, attackPower

// ��ǥ5 : �÷��̾ ���󰡴ٰ� �ʱ� ��ġ���� ���� �Ÿ��� ����� Return ���·� ��ȯ�Ѵ�.
// �ʿ�Ӽ�5 : �ʱ���ġ, �̵����� ����

// ��ǥ6 : �ʱ� ��ġ�� ���ƿ´�. Ư���Ÿ� �̳���, Idle ���·� ��ȯ�Ѵ�.
// �ʿ�Ӽ�6 : Ư���Ÿ�

// ��ǥ7 : �÷��̾��� ������ ������ hitDamage��ŭ ���ʹ��� hp�� ���ҽ�Ų��
// �ʿ�Ӽ�7 : hp

// ��ǥ8 : ���ʹ��� ü���� 0���� ũ�� �ǰݻ��·� ��ȯ
// ��ǥ9 : �׷��� ������ �������� ��ȯ
public class EnemyFSM : MonoBehaviour
{
    // �ʿ�Ӽ�1 : �� ���� 
    enum EnemyState //������
    {
        Idle,
        Move,
        Attack,
        Return,
        Damage,
        Die
    }
    EnemyState enemyState;

    // �ʿ�Ӽ�2 : �÷��̾���� �Ÿ�, �÷��̾� Ʈ������
    public float findDistance = 5;
    Transform player;

    // �ʿ�Ӽ�3 : �̵��ӵ�, ���� �̵��� ���� ĳ������Ʈ�ѷ�
    public float moveSpeed;
    CharacterController characterController;
    public float attackDistance = 2.0f;

    // �ʿ�Ӽ�4 : ����ð�, Ư�����ݵ�����
    float currentTime;
    public float attackDelay = 2.0f;
    // ���ʹ��� ���ݷ�
    public int attackPower = 3;

    // �ʿ�Ӽ�5 : �ʱ���ġ, �̵����� ����
    Vector3 originPos;
    public float moveDistance;

    // �ʿ�Ӽ�6 : Ư���Ÿ�
    float returnDistance = 0.1f;

    // �ʿ�Ӽ�7 : hp
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

        // ���� : ���� FSM ���̾�׷��� ���� ���۽�Ű�� �ʹ�.
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

        print("���");
        Destroy(gameObject);
    }


    // ��ǥ7 : �÷��̾��� ������ ������ Damage��ŭ ���ʹ��� hp�� ���ҽ�Ų��
    // ��ǥ8 : ���ʹ��� ü���� 0���� ũ�� �ǰݻ��·� ��ȯ
    // ��ǥ9 : �׷��� ������ �������� ��ȯ
    public void DamageAction(int damage)
    {
        // ����, �̹� ���ʹ̰� �ǰݵưų�, ��� ���¶�� �������� ���� �ʴ´�.
        if (enemyState == EnemyState.Damage || enemyState == EnemyState.Die)
        {
            return;
        }
        // �÷��̾��� ���ݷ� ��ŭ hp�� ����
        hp -= damage;

        // ��ǥ8 : ���ʹ��� ü���� 0���� ũ�� �ǰݻ��·� ��ȯ
        if (hp > 0)
        {
            enemyState = EnemyState.Damage;
            print("���� ��ȯ: Any state -> Damage");
            Damaged();

        }
        // ��ǥ9 : �׷��� ������ �������� ��ȯ
        else
        {
            enemyState = EnemyState.Die;
            print("���� ��ȯ: Any state -> Die");
            StartCoroutine(DieProcess());

        }

    }

    private void Damaged()
    {
        // �ǰ� ��� 0.5

        // �ǰ� ���� ó���� ���� �ڷ��� ����
        StartCoroutine(DamageProssece());
    }

    // ������ ó����
    IEnumerator DamageProssece()
    {
        // �ǰ� ��� �ð���ŭ ��ٸ���.
        yield return new WaitForSeconds(0.5f);

        //���� ���¸� �̵� ���·� ��ȯ�Ѵ�.
        enemyState = EnemyState.Move;
        print("���� ��ȯ: Damage -> Move");
    }

    // ��ǥ5 : �÷��̾ ���󰡴ٰ� �ʱ� ��ġ���� ���� �Ÿ��� ����� Return ���·� ��ȯ�Ѵ�.
    private void Return()
    {
        float distanceToOriginPos = (originPos - transform.position).magnitude;
        //Ư�� �Ÿ� �̳���, Idle ���·� ��ȯ�ϴ�.
        if (distanceToOriginPos > returnDistance)
        {
            Vector3 dir = (originPos - transform.position).normalized;
            characterController.Move(dir * moveSpeed * Time.deltaTime);
        }
        else
        {
            enemyState = EnemyState.Idle;
            print("���� ��ȯ: Return -> Idle");

        }

    }

    // ��ǥ4 : �÷��̾ ���� ���� �ȿ� ���ý� Ư�� �ð��� �ѹ��� �����Ѵ�.
    private void Attack()
    {
        // �÷��̾���� �Ÿ��� ���� ���� ���� ������ ���� �÷��̾ ���󰣴�.
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < attackDistance)
        {
            currentTime += Time.deltaTime;
            //Ư�� �ð��� �ѹ��� �����Ѵ�
            if (currentTime > attackDelay)
            {
                player.GetComponent<PlayerMove>().DamageAction(attackPower);
                print("����!");
                currentTime = 0;
            }
        }
        else
        {
            // �׷��� ������ Move�� ���¸� ��ȯ�Ѵ�.
            enemyState = EnemyState.Move;
            print("���� ��ȯ: Attack -> Move");
            currentTime = 0;

        }
    }

    // ��ǥ3 : ���� ���°� Move�� ��, �÷��̾���� �Ÿ��� ���� ���� ���̸� ���� �÷��̾ ���󰣴�.
    private void Move()
    {
        // �÷��̾���� �Ÿ��� ���� ���� ���� ������ ���� �÷��̾ ���󰣴�.
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ��ǥ5 : �÷��̾ ���󰡴ٰ� �ʱ� ��ġ���� ���� �Ÿ��� ����� �ʱ���ġ�� ���ƿ´�.
        float distanceToOriginPos = (originPos - transform.position).magnitude;
        if (distanceToOriginPos > moveDistance)
        {
            enemyState = EnemyState.Return;
            print("���� ��ȯ: Move -> Return");
        }
        else
        {
            if (distanceToPlayer > attackDistance)
            {
                Vector3 dir = (player.position - transform.position).normalized;

                // �÷��̾ ���󰣴�.
                characterController.Move(dir * moveSpeed * Time.deltaTime);
            }
            else
            {
                enemyState = EnemyState.Attack;
                print("���� ��ȯ: Move -> Attack");
                currentTime = attackDelay;
            }
        }


        /*        if (distanceToPlayer > findDistance)
                {
                    enemyState = EnemyState.Idle;
                    print("���� ��ȯ: Move -> Idle");
                }*/



    }

    // ��ǥ2 : �÷��̾���� �Ÿ��� �����ؼ� Ư�� ���·� ����� �ش�.
    private void Idle()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        /*float distanceToPlayer = (player.position - transform.position).magnitude;*/

        //���� �÷��̾���� �Ÿ��� Ư�� ���� ���� ���¸� Move�� �ٲ��ش�.
        if (distanceToPlayer < findDistance)
        {
            enemyState = EnemyState.Move;
            print("������ȯ: Idle -> Move");
        }
    }
}
