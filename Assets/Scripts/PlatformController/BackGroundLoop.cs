using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    //float width = 15f;            //����� ���� ���� ����
    public GameObject[] backgroundList; // ��渮��Ʈ
    private int backgrounIndex = 0; // ��׶��� �ε���

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        
    }

    public void BackgrounChage()
    {
        // ��� ��ȯ ����
        if (GameManager.instance.publicScore % GameManager.labelUp == 0 && GameManager.instance.publicScore !=0) 
        {
            backgroundList[backgrounIndex].SetActive(false);
            if (backgrounIndex >= backgroundList.Length -1) backgrounIndex = -1; // index�ݺ� �ϱ����� �ʱ�ȭ
                
            backgroundList[++backgrounIndex].SetActive(true);
            GameManager.instance.publicScore++; // �ݺ�����
        }
    }

}
