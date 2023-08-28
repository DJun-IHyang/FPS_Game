using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 목적 : HP bar가 타겟의 앞 방향으로 향한다.
// 필요속성 : 타겟
public class HpBarTarget : MonoBehaviour
{
    // 필요속성 : 타겟
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.forward = target.transform.forward;
    }
}
