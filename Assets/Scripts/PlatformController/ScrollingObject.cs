using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed = 10f;           //바닥의 이동 속도

    // Update is called once per frame
    void Update()
    {
        //게임 시작 전일 때 동작하지 않음
        if (!GameManager.isGameStart) return;

        if (!GameManager.instance.isGameOver )
        {
            //게임 오브젝트가 일정 속도로 왼쪽으로 평행이동하는 로직
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
