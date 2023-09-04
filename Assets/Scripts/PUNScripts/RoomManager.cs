using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// ���� : ���� ������ �����ְ�, Leave room ��ư�� ������ ���� ���� �� �ִ�.
// �ʿ�Ӽ� : �� ���� Text, Leave room ��ư

// ����2 : Photon view�� ���� �÷��̾ �����Ѵ�.
// �ʿ�Ӽ�2 : Photon View �÷��̾�
public class RoomManager : MonoBehaviourPunCallbacks //��Ʈ��ũ���� MonoBehaviour�� ��� �� �� �ִ� �Լ�
{
    // �ʿ�Ӽ� : �� ���� Text, Leave room ��ư
    public TMP_Text infoText;

    // �ʿ�Ӽ�2 : Photon View �÷��̾�
    public PhotonView playerPrefab;

    private void Start()
    {
        // ����2 : Photon view�� ���� �÷��̾ �����Ѵ�.
        PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);

    }

    // ���� ������ �����ְ� �ʹ�.
    public void ShowRoomInfo()
    {
        if(PhotonNetwork.InRoom)
        {
            //�ʿ� ���� : �� �̸�, �� �ο���, �ִ� �ο���, �÷��̾� �̸�            
            string roomName = PhotonNetwork.CurrentRoom.Name;
            int playerCnt = PhotonNetwork.CurrentRoom.PlayerCount;
            int maxPlayerCnt = PhotonNetwork.CurrentRoom.MaxPlayers;

            string playerNames = "< Player ��� >\n";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                playerNames += PhotonNetwork.PlayerList[i].NickName + "\n";
            }

            infoText.text = string.Format("Room: {0}\nPlayer number: {1}\nMax player number: {2}\n{3}", roomName, playerCnt, maxPlayerCnt);


        }
        else
        {
            infoText.text = null;
        }
    }

    // Leave room ��ư�� ������ ���� ���� �� �ִ�.
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();

        SceneManager.LoadScene(1);
    }
}
