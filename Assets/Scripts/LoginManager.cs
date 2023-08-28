using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


// ��ǥ1 : ������� ���������� �Է��Ͽ� �����ϰų�(ȸ������) ����� �����͸� �о �������� ���� ������ ���� �α��� �ϰ� �ʹ�.
// �ʿ�Ӽ� : ID Inputfield, PW Inputfield, �����ؽ�Ʈ
// IO - �÷��̾� ������

// ��ǥ2 : ���̵�� �н����带 �����ؼ� ȸ������ �ϰ� �ʹ�.

//  ��ǥ3 : �Է��� ���� ���, �Է��� ä���޶�� �޽����� �����ؽ�Ʈ�� ����.
public class LoginManager : MonoBehaviour
{
    // �ʿ�Ӽ� : ID InputField, PW InputField, �����ؽ�Ʈ
    public TMP_InputField id;
    public TMP_InputField pw;
    public TMP_Text authTxt;

    // Start is called before the first frame update
    void Start()
    {
        authTxt.text = string.Empty;
    }

    // ��ǥ2 : ���� �ý��ۿ� ������ ���ٸ� ���̵�� �н����带 �����ؼ� ȸ������ �ϰ� �ʹ�.
    public void SighUp()
    {
        if (!CheckInput(id.text, pw.text))
        {
            return;
        }
        //���� �ý��ۿ� ������ ���ٸ� ȸ�������� �ϰ� �ʹ�.
        if (!PlayerPrefs.HasKey(id.text))
        {
            PlayerPrefs.SetString(id.text, pw.text);
            authTxt.text = "ȸ�������� �Ϸ�Ǿ����ϴ�.";
        }
        else
        {
            authTxt.text = "�̹� �����ϴ� ID�Դϴ�.";
        }
    }

    // ��ǥ1 : ������� ���������� �Է��Ͽ� �����ϰų�(ȸ������) ����� �����͸� �о �������� ���� ������ ���� �α��� �ϰ� �ʹ�.
    public void Login()
    {
        if (!CheckInput(id.text, pw.text))
        {
            return;
        }

        string password = PlayerPrefs.GetString(id.text);

        if (PlayerPrefs.HasKey(id.text))
        {
            //��й�ȣ�� ��ġ�ϸ�
            if (password == pw.text)
            {
                SceneManager.LoadScene(1);
                authTxt.text = "�α��� ����.";
            }
            else
            {
                authTxt.text = "�Է��Ͻ� ���̵�� �н����尡 ��ġ���� �ʽ��ϴ�.";
            }
        }
        else
        {
            authTxt.text = "���̵� �������� �ʽ��ϴ�. ȸ������ ����";
        }
    }

    //  ��ǥ3 : �Է��� ���� ���, �Է��� ä���޶�� �޽����� �����ؽ�Ʈ�� ����.
    bool CheckInput(string _id, string _pw)
    {
        if(_id == "" || _pw == "")
        {
            authTxt.text = "���̵� �Ǵ� �н����带 �Է����ּ���";

            return false;
        }
        else
        {
            return true;
        }
    }
}
