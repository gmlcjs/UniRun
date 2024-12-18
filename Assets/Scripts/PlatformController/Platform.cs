using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject[] obstacles;      //��ֹ� ������Ʈ�� �迭
    bool isStepped = false;             //�÷����� ĳ���� �浹�ߴ��� Ȯ���ϴ� �뵵
    int Probability = 11;

    private void OnEnable()
    {
        //������ �����ϴ� ����
        //�ʱ�ȭ
        isStepped = false;

        

        // ������ ������ 20�� ������� Ȯ���� 1���� 
        if(GameManager.score % 20 == 0 && Probability > 1){
            Probability--;
            // Debug.Log("���� ���� : " +GameManager.score);
            // Debug.Log("Ȯ��"+Probability);
        }

        //��ֹ��� �� ��ŭ ������
        for (int i = 0; i < obstacles.Length; i++)
        {
            //���� ������ ��ֹ��� 1/Probability�� Ȯ���� Ȱ��ȭ
            if (Random.Range(0, Probability) == 0)
            {
                obstacles[i].SetActive(true);
            }
            else
            {
                obstacles[i].SetActive(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�÷��̾ �÷����� ��Ҵ��� Ȯ���ϴ� �뵵
        //�÷��̾�� �浹�ߴ��� Ȯ��, isStepped�� false���� Ȯ��
        if (collision.collider.tag == "Player" && !isStepped)
        {
            isStepped = true;
            GameManager.instance.AddScore(1);
        }
    }
}
