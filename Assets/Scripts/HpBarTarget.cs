using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� : HP bar�� Ÿ���� �� �������� ���Ѵ�.
// �ʿ�Ӽ� : Ÿ��
public class HpBarTarget : MonoBehaviour
{
    // �ʿ�Ӽ� : Ÿ��
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.forward = target.transform.forward;
    }
}
