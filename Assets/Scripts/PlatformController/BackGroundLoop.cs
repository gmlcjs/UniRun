using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    float width;            //배경의 가로 길이 변수

    // Start is called before the first frame update
    void Awake()
    {
        //가로 길이를 측정하는 로직
        //BoxCollider2D 컴포넌트 size필드의 x값을 가로길이로 사용
        var backGroundCollider = GetComponent<BoxCollider2D>();
        width = backGroundCollider.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        //현재 위치가 원점에서 왼쪽으로 width만큼 이동했을 때 위치를 재배치
        if (transform.position.x <= -width)
        {
            RePosition();
        }
    }

    /// <summary>
    /// 위치를 재배치하는 매서드
    /// </summary>
    void RePosition()
    {
        //현재위치에서 오른쪽으로 가로 길이 * 2만큼 이동
        Vector2 offset = new Vector2(width * 2f, 0);
        transform.position = (Vector2)transform.position + offset;
    }
}
