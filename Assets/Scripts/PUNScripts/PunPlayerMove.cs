using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 목적: 플레이어를 키입력에 따라 이동시킨다.
// 필요속성 : 스피드
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
            // 목적: 플레이어를 키입력에 따라 이동시킨다.
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
