using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject[] obstacles;      //장애물 오브젝트의 배열
    bool isStepped = false;             //플랫폼에 캐릭터 충돌했는지 확인하는 용도
    int Probability = 11;

    private void OnEnable()
    {
        //발판을 리셋하는 로직
        //초기화
        isStepped = false;

        

        // 게임의 점수가 20의 배수마다 확률이 1증가 
        if(GameManager.score % 20 == 0 && Probability > 1){
            Probability--;
            // Debug.Log("현재 점수 : " +GameManager.score);
            // Debug.Log("확률"+Probability);
        }

        //장애물의 수 만큼 루프함
        for (int i = 0; i < obstacles.Length; i++)
        {
            //현재 순번의 장애물을 1/Probability의 확률로 활성화
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
        //플레이어가 플랫폼을 밟았는지 확인하는 용도
        //플레이어와 충돌했는지 확인, isStepped가 false인지 확인
        if (collision.collider.tag == "Player" && !isStepped)
        {
            isStepped = true;
            GameManager.instance.AddScore(1);
        }
    }
}
