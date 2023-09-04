using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

// ��ǥ : ���� ���� �񵿱� ������� �ε��ϰ� �ʹ�.
// �ʿ�Ӽ�1 : ���� ������ �� ��ȣ

// ��ǥ2 : ���� ���� �ε� ������� �����̴��� ǥ���ϰ� �ʹ�.
// �ʿ�Ӽ�2 : �ε� �����̴�, �ε��ؽ�Ʈ
public class LoadingNextScene : MonoBehaviour
{
    // �ʿ�Ӽ�1 : ���� ������ �� ��ȣ
    public string sceneName = "InGameScene";

    // �ʿ�Ӽ�2 : �ε� �����̴�, �ε��ؽ�Ʈ
    public Slider loadingSlider;
    public TMP_Text loadingText;

    private void Start()
    {
        //�񵿱� ���� �ڷ�ƾ �Լ��� �����Ѵ�.
        StartCoroutine(AsyncNextScene(sceneName));
    }

    // ��ǥ : ���� ���� �񵿱� ������� �ε��ϰ� �ʹ�.
    IEnumerator AsyncNextScene(string num)
    {
        // ������ ���� �񵿱� ������� ����� �ʹ�.
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(num);

        //���� ȭ�鿡 ������ �ʰ� �ϰ� �ʹ�.
        asyncOperation.allowSceneActivation = false;

        // ��ǥ2 : ���� ���� �ε� ������� �����̴��� ǥ���ϰ� �ʹ�.
        while (!asyncOperation.isDone)
        {
            loadingSlider.value = asyncOperation.progress;
            loadingText.text = (asyncOperation.progress * 100).ToString() + "%";

            //Ư�� ������� �� ���� ���� ���� �ְ� �ʹ�
            if(asyncOperation.progress >= 0.9f )
            {
                // ���� ȭ�鿡 �����ְ� �ʹ�.
                asyncOperation.allowSceneActivation = true;

                MainGameManager.Instance.StartTimer();
            }

            yield return null;
        }
    }
}
