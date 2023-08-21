using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 목적 : W, A, S, D키를 누르면 캐릭터를 그 방향으로 이동시키고 싶다.
// 필요속성1 : 이동속도
// 1-1. 사용자의 입력을 받는다.
// 1-2. 이동방향을 설정한다.
// 1-3. 이동속도에 따라 나를 이동시킨다.

// 목적2 : 스페이스를 누르면 수직으로 점프 하고싶다.
// 필요속성2 : 캐릭터 컨트롤러, 중력 변수, 수직 속력 변수, 점프파워, 점프 상태 변수
// 2-1. 캐릭터 수직 속도에 중력을 적용하고 싶다.
// 2-2. 캐릭터 컨트롤러로 나를 이동시키고 싶다.
// 2-3. 스페이스 키를 누르면 수직속도에 점프파워를 적용하고싶다.

// 목적3 : 점프 중인지 확인 하고, 점프 중이면 점프 전상태로 초기화 하고 싶다.
// 필요속성3 : 점프상태
// 3-1. 바닥에 닿아 있을 때, 수직 속도를 초기화 하고 싶다.

// 목적4 : 플레이어가 피격을 당하면 hp를 damage만큼 깎는다.
// 필요속성4 : hp
public class PlayerMove : MonoBehaviour
{
    // 필요속성1 : 이동속도
    public float speed = 10;

    // 필요속성2 : 캐릭터 컨트롤러, 중력 변수, 수직 속력 변수, 점프파워, 점프 상태 변수
    CharacterController characterController;
    float gravity = -20f;
    float yVelocity = 0;
    public float jumpPower = 5;
    // 필요속성3 : 점프 상태
    public bool isJumping = false;
    // 필요속성4 : hp
    public float hp = 3;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 1-1. 사용자의 입력을 받는다.
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 목적3. 점프 중인지 확인 하고, 점프 중이면 점프 전상태로 초기화 하고 싶다.
        if (isJumping && characterController.collisionFlags == CollisionFlags.Below)
        {
            isJumping = false;

            yVelocity = 0;
        }
        // 3-1. 바닥에 닿아 있을 때, 수직 속도를 초기화 하고 싶다.
        else if (characterController.collisionFlags == CollisionFlags.Below)
        {
            yVelocity = 0;
        }

        // 2-3. 스페이스 키를 누르면 수직속도에 점프파워를 적용하고싶다.
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            yVelocity = jumpPower;
            isJumping = true;
        }

        // 1-2. 이동방향을 설정한다.
        Vector3 dir = new Vector3(h, 0, v);
        dir = Camera.main.transform.TransformDirection(dir);

        // 2-1. 캐릭터 수직 속도에 중력을 적용하고 싶다.
        yVelocity += +gravity * Time.deltaTime;
        dir.y = yVelocity;

        // 1-3. 이동속도에 따라 나를 이동시킨다.
        /*transform.position += dir * speed * Time.deltaTime;*/

        // 2-2. 캐릭터 컨트롤러로 나를 이동시키고 싶다.
        characterController.Move(dir * speed * Time.deltaTime);
    }

    // 목적4 : 플레이어가 피격을 당하면 hp를 damage만큼 깎는다.
    public void DamageAction(int damage)
    {
        hp -= damage;
    }
}
