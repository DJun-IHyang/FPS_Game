using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


// 목적 : 게임의 상태(Ready, Start, GameOver)를 구별하고, 게임의 시작과 끝을 TextUI로 표현하고 싶다.
// 필요속성1 : 게임상태 열거형 변수, TextUI

// 목적2 : 2초 후 Ready상태에서 Start상태로 변경되며 게임이 시작된다.

// 목적3 : 플레이어의 hp가 0보다 작으면 상태 텍스트와 상태를 GameOver로 바꿔준다.
// 필요속성3 : hp가 들어있는 PlayerMove클래스

// 목적4 : 플레이어의 hp가 0 이하라면, 플레이어의 애니메이션을 멈춘다
// 필요속성 : 플레이어의 애니메이터 컴포넌트

// 목적5 : Option 버튼을 누르면 Option UI가 켜진다. 동시에 게임 속도를 조절한다.(0 or 1)
// 필요속성5 : OptionUI 게임오브젝트, 일시정지 상태

// 목적6 : 게임 오버시 Retry와 Quit 버튼을 활성화 한다

// 목적7 : 게임 오버시, HP Bar, Weapon Mode Text를 비활성화 한다

// 목적8 : 게임이 시작되면 PhotonView를 사용하여 Player를 생성한다.
// 필요속성8 : Player PhotonView

// 목적9 : 게임이 시작되면 PhotonNetwork에 접속한 Player들을 확인해서 내 번호를 정한다.

// 목적10 : Client로서 접속이 되면 (GameManager가 태어나면) PhotonNetwork에 접속한 Player들을 확인해서 내 번호를 정한다.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    Animator animator;

    // 필요속성1 : 게임상태 열거형 변수, TextUI
    public enum GameState // 열거형
    {
        Ready,
        Start,
        Pause,
        GameOver
    }

    public GameState state = GameState.Ready;
    public TMP_Text stateText;

    // 필요속성3 : hp가 들어있는 PlayerMove클래스
    PlayerMove player;

    // 필요속성5 : OptionUI 게임오브젝트, 일시정지 상태
    public GameObject optionUI;

    public PhotonView playerPrefab;
    private int myPlayerNumber = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        // 목적10 : Client로서 접속이 되면 (GameManager가 태어나면) PhotonNetwork에 접속한 Player들을 확인해서 내 번호를 정한다.
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

    // 목적2 : 2초 후 Ready상태에서 Start상태로 변경되며 게임이 시작된다.
    IEnumerator GameStart()    //코루틴 함수
    {
        while (!MainGameManager.Instance.isGameStarted)
        {
            yield return null;
        }

        // 목적8 : 게임이 시작되면 PhotonView를 사용하여 Player를 생성한다.
        PhotonNetwork.Instantiate((playerPrefab.name), MainGameManager.Instance.spawnPoints[myPlayerNumber].position, Quaternion.identity);

        stateText.text = "Game Start";
        stateText.color = new Color32(0, 255, 0, 255);

        // 0.5초를 기다린다.
        yield return new WaitForSeconds(0.5f);

        // 상태 text 비활성
        stateText.gameObject.SetActive(false);

        player.GetComponent<PlayerFire>().weaponModeText.gameObject.SetActive(true);

        state = GameState.Start;
    }

    // 목적3 : 플레이어의 hp가 0보다 작으면 상태 텍스트와 상태를 GameOver로 바꿔준다.
    void CheckGameOver()
    {
        if (player.hp <= 0)
        {
            // 목적4 : 플레이어의 hp가 0 이하라면, 플레이어의 애니메이션을 멈춘다
            /*animator.SetFloat("MoveMotion");*/

            //상태 텍스트 ON
            stateText.gameObject.SetActive(true);

            //상태 텍스트를 GameOver로 변경
            stateText.text = "Game Over";

            //상태 텍스트의 색상을 노랑으로 변경
            stateText.color = new Color(255, 255, 0, 255);

            // 목적6 : 게임 오버시 Retry와 Quit 버튼을 활성화 한다
            GameObject retryBtn = stateText.transform.GetChild(0).gameObject;
            GameObject quitBtn = stateText.transform.GetChild(1).gameObject;
            retryBtn.SetActive(true);
            quitBtn.SetActive(true);

            // 목적7 : 게임 오버시, HP Bar, Weapon Mode Text를 비활성화 한다
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

    // 목적5 : Option 버튼을 누르면 Option UI가 켜진다. 동시에 게임 속도를 조절한다.(0 or 1)
    // Option화면 켜기
    public void OpenOptionWindow()
    {
        //옵션UI가 켜진다
        optionUI.SetActive(true);
        //동시에 게임 속도를 조절한다(0 or 1)
        Time.timeScale = 0;
        //게임 상태를 변경한다
        state = GameState.Pause;
    }

    // 계속하기
    public void CloseOptionWindow()
    {
        //옵션UI가 꺼진다
        optionUI.SetActive(false);
        //동시에 게임 속도를 조절한다(0 or 1)
        Time.timeScale = 1;
        //게임 상태를 변경한다
        state = GameState.Start;
    }

    //다시하기 옵션
    public void RestartGame()
    {
        //동시에 게임 속도를 조절한다(1)
        Time.timeScale = 1;

        //현재 씬 번호를 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    //게임 종료 옵션
    public void QuitGame()
    {
        Application.Quit();
    }
}
