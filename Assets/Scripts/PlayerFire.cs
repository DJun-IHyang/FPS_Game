using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 목적 : 마우스 오른쪽 버튼을 눌러 폭탄을 특정 방향으로 발사하고 싶다.
// 필요속성1 : 폭탄 게임오브젝트, 발사위치, 방향
// 1-1. 마우스 오른쪽 버튼을 누른다.
// 1-2. 폭탄 게임오브젝트를 생성하고 firePosition에 위치시킨다.
// 1-3. 폭탄 오브젝트의 rigidbody를 가져와서 카메라 정면 방향으로 힘을 가한다.

// 목적2 : 마우스 왼쪽 버튼을 누르면 시선 방향으로 총을 발사하고 싶다.
// 필요속성2 : 피격효과 게임오브젝트, 이펙트의 파티클 시스템
// 2-1. 마우스 왼쪽 버튼을 누른다.
// 2-2. 레이를 생성하고 발사위치와 발사 방향을 설정한다.
// 2-3. 레이가 부딪힌 대상의 정보를 저장할 수 있는 변수를 만든다.
// 2-4. 레이를 발사하고 부딪힌 물체가 있으면 그 위치에 피격 효과를 만든다.

// 목적3 : 레이가 부딪힌 대상이 Enemy라면 Enemy에게 데미지를 주겠다.
public class PlayerFire : MonoBehaviour
{
    // 필요속성 : 폭탄 게임오브젝트, 발사위치, 방향
    public GameObject bomb;
    public GameObject firePosition;
    public float power;
    private PlayerFire playerFire;

    // 필요속성 : 피격효과 게임오브젝트, 이펙트의 파티클 시스템
    public GameObject hitEffect;
    ParticleSystem particleSystem;
    public int weaponPower = 2;

    void Awake()
    {
        playerFire = GameObject.Find("Player").GetComponent<PlayerFire>();
    }

    private void Start()
    {
        particleSystem = hitEffect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // 목적 : 마우스 오른쪽 버튼을 눌러 폭탄을 특정 방향으로 발사하고 싶다.
        // 1-1. 마우스 오른쪽 버튼을 누른다.
        if (Input.GetMouseButtonDown(1)) // 왼쪽(0), 오른쪽(1), 휠(2)
        {
            // 1-2. 폭탄 게임오브젝트를 생성하고 firePosition에 위치시킨다.
            GameObject bombGO = Instantiate(bomb);
            bombGO.transform.position = firePosition.transform.position;

            // 1-3. 폭탄 오브젝트의 rigidbody를 가져와서 카메라 정면 방향으로 힘을 가한다.
            Rigidbody rigidbody = bombGO.GetComponent<Rigidbody>();
            rigidbody.AddForce(Camera.main.transform.forward * power, ForceMode.Impulse);
        }

        // 2-1. 마우스 왼쪽 버튼을 누른다.
        if (Input.GetMouseButtonDown(0)) // 왼쪽(0), 오른쪽(1), 휠(2)
        {
            // 2-2. 레이를 생성하고 발사위치와 발사 방향을 설정한다.
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            // 2-3. 레이가 부딪힌 대상의 정보를 저장할 수 있는 변수를 만든다.
            RaycastHit hitInfo = new RaycastHit();

            // 2-4. 레이를 발사하고
            if (Physics.Raycast(ray, out hitInfo))   // ref & out
            {
                
                print("충돌체와의 거리: " + hitInfo.distance);
                // 부딪힌 물체가 있으면 그 위치에 피격 효과를(법선벡터 방향으로) 만든다.
                hitEffect.transform.position = hitInfo.point;
                hitEffect.transform.forward = hitInfo.normal;

                // 피격 이펙트를 재생한다.
                particleSystem.Play();

                // 목적3 : 레이가 부딪힌 대상이 Enemy라면 Enemy에게 데미지를 주겠다.
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    EnemyFSM enemyFSM = hitInfo.transform.GetComponent<EnemyFSM>();
                    enemyFSM.DamageAction(weaponPower);
                }

            }

        }



    }


}
