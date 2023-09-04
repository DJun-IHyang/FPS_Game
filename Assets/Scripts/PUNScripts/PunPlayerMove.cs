using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����: �÷��̾ Ű�Է¿� ���� �̵���Ų��.
// �ʿ�Ӽ� : ���ǵ�
public class PunPlayerMove : MonoBehaviour
{

    public float speed = 3;

    PhotonView pv;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pv.IsMine)
        {
            // ����: �÷��̾ Ű�Է¿� ���� �̵���Ų��.
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(-speed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(0, speed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(0, -speed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
        }
    }
}
