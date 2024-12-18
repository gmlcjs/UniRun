using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed = 10f;           //�ٴ��� �̵� �ӵ�

    // Update is called once per frame
    void Update()
    {
        //���� ���� ���� �� �������� ����
        if (!GameManager.isGameStart) return;

        if (!GameManager.instance.isGameOver )
        {
            //���� ������Ʈ�� ���� �ӵ��� �������� �����̵��ϴ� ����
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
