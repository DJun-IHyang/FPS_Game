using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


// ���� : ������ ����(Ready, Start, GameOver)�� �����ϰ�, ������ ���۰� ���� TextUI�� ǥ���ϰ� �ʹ�.
// �ʿ�Ӽ�1 : ���ӻ��� ������ ����, TextUI

// ����2 : 2�� �� Ready���¿��� Start���·� ����Ǹ� ������ ���۵ȴ�.

// ����3 : �÷��̾��� hp�� 0���� ������ ���� �ؽ�Ʈ�� ���¸� GameOver�� �ٲ��ش�.
// �ʿ�Ӽ�3 : hp�� ����ִ� PlayerMoveŬ����

// ����4 : �÷��̾��� hp�� 0 ���϶��, �÷��̾��� �ִϸ��̼��� �����
// �ʿ�Ӽ� : �÷��̾��� �ִϸ����� ������Ʈ

// ����5 : Option ��ư�� ������ Option UI�� ������. ���ÿ� ���� �ӵ��� �����Ѵ�.(0 or 1)
// �ʿ�Ӽ�5 : OptionUI ���ӿ�����Ʈ, �Ͻ����� ����

// ����6 : ���� ������ Retry�� Quit ��ư�� Ȱ��ȭ �Ѵ�

// ����7 : ���� ������, HP Bar, Weapon Mode Text�� ��Ȱ��ȭ �Ѵ�

// ����8 : ������ ���۵Ǹ� PhotonView�� ����Ͽ� Player�� �����Ѵ�.
// �ʿ�Ӽ�8 : Player PhotonView

// ����9 : ������ ���۵Ǹ� PhotonNetwork�� ������ Player���� Ȯ���ؼ� �� ��ȣ�� ���Ѵ�.

// ����10 : Client�μ� ������ �Ǹ� (GameManager�� �¾��) PhotonNetwork�� ������ Player���� Ȯ���ؼ� �� ��ȣ�� ���Ѵ�.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    Animator animator;

    // �ʿ�Ӽ�1 : ���ӻ��� ������ ����, TextUI
    public enum GameState // ������
    {
        Ready,
        Start,
        Pause,
        GameOver
    }

    public GameState state = GameState.Ready;
    public TMP_Text stateText;

    // �ʿ�Ӽ�3 : hp�� ����ִ� PlayerMoveŬ����
    PlayerMove player;

    // �ʿ�Ӽ�5 : OptionUI ���ӿ�����Ʈ, �Ͻ����� ����
    public GameObject optionUI;

    public PhotonView playerPrefab;
    private int myPlayerNumber = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        // ����10 : Client�μ� ������ �Ǹ� (GameManager�� �¾��) PhotonNetwork�� ������ Player���� Ȯ���ؼ� �� ��ȣ�� ���Ѵ�.
        Player[] players = PhotonNetwork.PlayerList;
        foreach(var p in players)
        {
            if(p != PhotonNetwork.LocalPlayer)
            {
                myPlayerNumber++;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        stateText.text = "Ready";
        stateText.color = new Color32(255, 185, 0, 255);

        StartCoroutine(GameStart());

        player = GameObject.Find("Player").GetComponent<PlayerMove>();

        animator = player.GetComponentInChildren<Animator>();

        player.GetComponent<PlayerFire>().weaponModeText.gameObject.SetActive(false);

    }

    // ����2 : 2�� �� Ready���¿��� Start���·� ����Ǹ� ������ ���۵ȴ�.
    IEnumerator GameStart()    //�ڷ�ƾ �Լ�
    {
        while (!MainGameManager.Instance.isGameStarted)
        {
            yield return null;
        }

        // ����8 : ������ ���۵Ǹ� PhotonView�� ����Ͽ� Player�� �����Ѵ�.
        PhotonNetwork.Instantiate((playerPrefab.name), MainGameManager.Instance.spawnPoints[myPlayerNumber].position, Quaternion.identity);

        stateText.text = "Game Start";
        stateText.color = new Color32(0, 255, 0, 255);

        // 0.5�ʸ� ��ٸ���.
        yield return new WaitForSeconds(0.5f);

        // ���� text ��Ȱ��
        stateText.gameObject.SetActive(false);

        player.GetComponent<PlayerFire>().weaponModeText.gameObject.SetActive(true);

        state = GameState.Start;
    }

    // ����3 : �÷��̾��� hp�� 0���� ������ ���� �ؽ�Ʈ�� ���¸� GameOver�� �ٲ��ش�.
    void CheckGameOver()
    {
        if (player.hp <= 0)
        {
            // ����4 : �÷��̾��� hp�� 0 ���϶��, �÷��̾��� �ִϸ��̼��� �����
            /*animator.SetFloat("MoveMotion");*/

            //���� �ؽ�Ʈ ON
            stateText.gameObject.SetActive(true);

            //���� �ؽ�Ʈ�� GameOver�� ����
            stateText.text = "Game Over";

            //���� �ؽ�Ʈ�� ������ ������� ����
            stateText.color = new Color(255, 255, 0, 255);

            // ����6 : ���� ������ Retry�� Quit ��ư�� Ȱ��ȭ �Ѵ�
            GameObject retryBtn = stateText.transform.GetChild(0).gameObject;
            GameObject quitBtn = stateText.transform.GetChild(1).gameObject;
            retryBtn.SetActive(true);
            quitBtn.SetActive(true);

            // ����7 : ���� ������, HP Bar, Weapon Mode Text�� ��Ȱ��ȭ �Ѵ�
            player.hpSlider.gameObject.SetActive(false);
            player.GetComponent<PlayerFire>().weaponModeText.gameObject.SetActive(false);

            state = GameState.GameOver;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameOver();
    }

    // ����5 : Option ��ư�� ������ Option UI�� ������. ���ÿ� ���� �ӵ��� �����Ѵ�.(0 or 1)
    // Optionȭ�� �ѱ�
    public void OpenOptionWindow()
    {
        //�ɼ�UI�� ������
        optionUI.SetActive(true);
        //���ÿ� ���� �ӵ��� �����Ѵ�(0 or 1)
        Time.timeScale = 0;
        //���� ���¸� �����Ѵ�
        state = GameState.Pause;
    }

    // ����ϱ�
    public void CloseOptionWindow()
    {
        //�ɼ�UI�� ������
        optionUI.SetActive(false);
        //���ÿ� ���� �ӵ��� �����Ѵ�(0 or 1)
        Time.timeScale = 1;
        //���� ���¸� �����Ѵ�
        state = GameState.Start;
    }

    //�ٽ��ϱ� �ɼ�
    public void RestartGame()
    {
        //���ÿ� ���� �ӵ��� �����Ѵ�(1)
        Time.timeScale = 1;

        //���� �� ��ȣ�� �ٽ� �ε�
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    //���� ���� �ɼ�
    public void QuitGame()
    {
        Application.Quit();
    }
}
