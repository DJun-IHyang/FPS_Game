using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

// 목적 : 로비에 방을 만든다.
// 필요속성 : 방이름을 넣을 Inputfield
public class LobbyManager : MonoBehaviourPunCallbacks //네트워크에서 MonoBehaviour를 사용 할 수 있는 함수
{
    // 필요속성 : 방이름을 넣을 Inputfield
    public TMP_InputField roomNameInput;
    public int maxPlayerNum = 5;
    public TMP_Text logText;
    public string tempTxt;
    public string sceneName = "LoadingScene";
    public GameObject mainGameManager;

    string updateTxt(out string _tempTxt, string input)
    {
        _tempTxt = input;
        return tempTxt += _tempTxt;
    }
    // 목적: 로비에 방을 만든다
    public void CreateRoom()
    {
        // inputField에 내용이 있을때, 방을 해당 inputField의 내용으로 만든다
        if(roomNameInput.text != "")
        {
            PhotonNetwork.JoinOrCreateRoom(roomNameInput.text, new RoomOptions { MaxPlayers = maxPlayerNum}, null);
            print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

    }
    
    // 방에 입장후 호출됨
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        logText.text = updateTxt(out tempTxt, "enter success\n");

        // MainGameManager를 생성한다.
        DontDestroyOnLoad(mainGameManager);

        SceneManager.LoadScene(sceneName);
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();

        DontDestroyOnLoad(mainGameManager);
        mainGameManager.SetActive(true);
    }

    // 방 개설 실패시 호출
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        logText.text = updateTxt(out tempTxt, "create room failed\n");

    }

    // 방 입장 실패시 호출
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        logText.text = updateTxt(out tempTxt, "join room failed\n");

    }
}
