using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    float width;            //����� ���� ���� ����

    // Start is called before the first frame update
    void Awake()
    {
        //���� ���̸� �����ϴ� ����
        //BoxCollider2D ������Ʈ size�ʵ��� x���� ���α��̷� ���
        var backGroundCollider = GetComponent<BoxCollider2D>();
        width = backGroundCollider.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        //���� ��ġ�� �������� �������� width��ŭ �̵����� �� ��ġ�� ���ġ
        if (transform.position.x <= -width)
        {
            RePosition();
        }
    }

    /// <summary>
    /// ��ġ�� ���ġ�ϴ� �ż���
    /// </summary>
    void RePosition()
    {
        //������ġ���� ���������� ���� ���� * 2��ŭ �̵�
        Vector2 offset = new Vector2(width * 2f, 0);
        transform.position = (Vector2)transform.position + offset;
    }
}
