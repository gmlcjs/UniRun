using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    //float width = 15f;            //배경의 가로 길이 변수
    public GameObject[] backgroundList; // 배경리스트
    private int backgrounIndex = 0; // 백그라운드 인덱스

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        
    }

    public void BackgrounChage()
    {
        // 배경 전환 로직
        if (GameManager.instance.publicScore % GameManager.labelUp == 0 && GameManager.instance.publicScore !=0) 
        {
            backgroundList[backgrounIndex].SetActive(false);
            if (backgrounIndex >= backgroundList.Length -1) backgrounIndex = -1; // index반복 하기위한 초기화
                
            backgroundList[++backgrounIndex].SetActive(true);
            GameManager.instance.publicScore++; // 반복방지
        }
    }

}
