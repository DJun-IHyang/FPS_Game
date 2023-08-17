using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� : ���콺 ������ ��ư�� ���� ��ź�� Ư�� �������� �߻��ϰ� �ʹ�.
// �ʿ�Ӽ� : ��ź ���ӿ�����Ʈ, �߻���ġ, ����
// 1-1. ���콺 ������ ��ư�� ������.
// 1-2. ��ź ���ӿ�����Ʈ�� �����ϰ� firePosition�� ��ġ��Ų��.
// 1-3. ��ź ������Ʈ�� rigidbody�� �����ͼ� ī�޶� ���� �������� ���� ���Ѵ�.
public class PlayerFire : MonoBehaviour
{
    // �ʿ�Ӽ� : ��ź ���ӿ�����Ʈ, �߻���ġ, ����
    public GameObject bomb;
    public GameObject firePosition;
    public float power;



    // Update is called once per frame
    void Update()
    {
        // ���� : ���콺 ������ ��ư�� ���� ��ź�� Ư�� �������� �߻��ϰ� �ʹ�.
        // 1-1. ���콺 ������ ��ư�� ������.
        if (Input.GetMouseButtonDown(1)) // ����(0), ������(1), ��(2)
        {
            // 1-2. ��ź ���ӿ�����Ʈ�� �����ϰ� firePosition�� ��ġ��Ų��.
            GameObject bombGO = Instantiate(bomb);
            bombGO.transform.position = firePosition.transform.position;

            // 1-3. ��ź ������Ʈ�� rigidbody�� �����ͼ� ī�޶� ���� �������� ���� ���Ѵ�.
            Rigidbody rigidbody = bombGO.GetComponent<Rigidbody>();
            rigidbody.AddForce(Camera.main.transform.forward * power,  ForceMode.Impulse);
        }



    }
}
