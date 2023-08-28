using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ���� : ���� A���� B���� 3�ʸ��� ���ڴ�
// �ʿ�Ӽ� : pointA, pointB, Ư���ð�, ����ð�
public class LerpMove : MonoBehaviour
{
    // �ʿ�Ӽ� : pointA, pointB, Ư���ð�, ����ð�
    public Transform pointA;
    public Transform pointB;
    public float duration;
    float currentTime;

    // Update is called once per frame
    void Update()
    {
        // ���� : ���� A���� B���� 3�ʸ��� ���ڴ�
        currentTime += Time.deltaTime;

        transform.position = Vector3.Lerp(pointA.position, pointB.position, currentTime / duration);
        
    }
}