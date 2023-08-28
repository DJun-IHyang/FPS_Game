using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 목적 : 마우스의 입력을 받아 카메라를 회전시킨다.
// 필요속성 : 마우스 입력 X, Y, 속도
// 순서1. 사용자의 마우스 입력을 받는다.
// 순서2. 마우스 입력에 따라 방향을 설정한다.
// 순서3. 물체를 회전시킨다.

// 목적2 : GameManager가 Ready 상태일 때는 플레이어 카메라가 움직일 수 없도록 한다. 
public class CamRotate : MonoBehaviour
{
    // 필요속성 : 마우스 입력 X, Y, 속도
    public float speed = 10.0f;
    float mX = 0;
    float mY = 0;

    // Update is called once per frame
    void Update()
    {
        // 목적8 : GameManager가 Ready 상태 이거나 GameOver 상태 일 때는 플레이어, 적이 움직일 수 없도록 한다. 
        if (GameManager.Instance.state != GameManager.GameState.Start)
        {
            return;
        }
        

        // 순서1. 사용자의 마우스 입력을 받는다.
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        mX += mouseX * speed * Time.deltaTime;
        mY += mouseY * speed * Time.deltaTime;
        //상하로 보는 각도를 고정시킨다
        mY = Mathf.Clamp(mY, -90f, 90f);

        // 순서2. 마우스 입력에 따라 방향을 설정한다.
        Vector3 dir = new Vector3(-mY, mX, 0);

        // 순서3. 물체를 회전시킨다.
        // r = r0 + vt
        transform.eulerAngles = dir;

    }
}
