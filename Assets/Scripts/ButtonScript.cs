using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

// ��ǥ : �÷��̾ ��ư�� ������ �ٸ��� ������, �׺���̼� �Ž��� �ٽ� �����.
// �ʿ�Ӽ� : �ٸ� ���ӿ�����Ʈ, navMeshSurface
public class ButtonScript : MonoBehaviour
{
    // �ʿ�Ӽ� : �ٸ� ���ӿ�����Ʈ, navMeshSurface
    public GameObject bridge;
    public NavMeshSurface navMeshSurface;   


    // Start is called before the first frame update
    void Start()
    {
        bridge.gameObject.SetActive(false);
    }


    //isTrigger�̿�
    private void OnTriggerEnter(Collider other)
    {
        // ��ǥ : �÷��̾ ��ư�� ������ �ٸ��� ������, �׺���̼� �Ž��� �ٽ� �����.
        //CompareTag - �±� ��
        if (other.CompareTag("Player"))
        {
            //�ٸ��� ������
            bridge.SetActive(true);
            //�׺���̼� �Ž��� �ٽ� �����.
            navMeshSurface.BuildNavMesh();
        }
    }
}
