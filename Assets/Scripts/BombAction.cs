using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 목적 : 폭탄이 물체에 부딪히면 이펙트 만들고 파괴된다.
// 필요속성 : 폭발이펙트
public class BombAction : MonoBehaviour
{
    // 필요속성 : 폭발이펙트
    public GameObject bombEffect;

    // 목적 : 폭탄이 물체에 부딪히면 이펙트 만들고 파괴된다.
    private void OnCollisionEnter(Collision collision)
    {
        // 이펙트를 만든다
        GameObject bombEffectGO = Instantiate(bombEffect);

        // 이펙트의 위치를 나(폭탄)의 위치로 위치 시켜준다
        bombEffectGO.transform.position = transform.position;

        //나(폭탄)를 제거 한다.
        Destroy(gameObject);
    }
}
