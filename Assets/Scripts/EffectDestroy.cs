using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 목적 : 내(이펙트)가 특정 시간이 지나면 제거된다.
// 필요속성 : 현재시간, 특정시간
public class EffectDestroy : MonoBehaviour
{
    float currentTime;
    public float destroyTime = 2.0f;

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        // 목적 : 내(이펙트)가 특정 시간이 지나면 제거된다.
        if (currentTime > destroyTime)
        {
            Destroy(gameObject);
        }
    }
}
