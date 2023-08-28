using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


// ���� : ������ ����(Ready, Start, GameOver)�� �����ϰ�, ������ ���۰� ���� TextUI�� ǥ���ϰ� �ʹ�.
// �ʿ�Ӽ�1 : ���ӻ��� ������ ����, TextUI

// ����2 : 2�� �� Ready���¿��� Start���·� ����Ǹ� ������ ���۵ȴ�.

// ����3 : �÷��̾��� hp�� 0���� ������ ���� �ؽ�Ʈ�� ���¸� GameOver�� �ٲ��ش�.
// �ʿ�Ӽ�3 : hp�� ����ִ� PlayerMoveŬ����

// ����4 : �÷��̾��� hp�� 0 ���϶��, �÷��̾��� �ִϸ��̼��� �����
// �ʿ�Ӽ� : �÷��̾��� �ִϸ����� ������Ʈ
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    Animator animator;

    // �ʿ�Ӽ�1 : ���ӻ��� ������ ����, TextUI
    public enum GameState // ������
    {
        Ready,
        Start,
        GameOver
    }

    public GameState state = GameState.Ready;
    public TMP_Text stateText;

    // �ʿ�Ӽ�3 : hp�� ����ִ� PlayerMoveŬ����
    PlayerMove player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
    }

    // ����2 : 2�� �� Ready���¿��� Start���·� ����Ǹ� ������ ���۵ȴ�.
    IEnumerator GameStart()    //�ڷ�ƾ �Լ�
    {
        // 2�ʸ� ��ٸ���.
        yield return new WaitForSeconds(2);

        stateText.text = "Game Start";
        stateText.color = new Color32(0, 255, 0, 255);

        // 0.5�ʸ� ��ٸ���.
        yield return new WaitForSeconds(0.5f);

        // ���� text ��Ȱ��
        stateText.gameObject.SetActive(false);

        state = GameState.Start;
    }

    // ����3 : �÷��̾��� hp�� 0���� ������ ���� �ؽ�Ʈ�� ���¸� GameOver�� �ٲ��ش�.
    void CheckGameOver()
    {
        if(player.hp <= 0)
        {
            // ����4 : �÷��̾��� hp�� 0 ���϶��, �÷��̾��� �ִϸ��̼��� �����
            /*animator.SetFloat("MoveMotion");*/


            //���� �ؽ�Ʈ ON
            stateText.gameObject.SetActive(true);

            //���� �ؽ�Ʈ�� GameOver�� ����
            stateText.text = "Game Over";
            
            //���� �ؽ�Ʈ�� ������ ������� ����
            stateText.color = new Color(255, 255, 0, 255);

            state = GameState.GameOver;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameOver();
    }
}
