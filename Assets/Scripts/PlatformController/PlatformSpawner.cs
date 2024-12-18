using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platform;                 //플랫폼 프리팹
    public int count = 3;                       //생성할 발판 수
    public float timeBetSpawnMin = 0.25f;       //다음 배치까지의 시간 간격 최솟값
    public float timeBetSpawnMax = 2.25f;       //다음 배치까지의 시간 간격 최댓값

    float timeBetSpawn;                         //다음 배치까지의 시간 간격
    float yMin = -2.5f;                         //배치할 위치의 최소 y값
    float yMax = 1.5f;                          //배치할 위치의 최대 y값
    float xPos = 20f;                           //배치할 위치의 x값

    GameObject[] platforms;                     //미리 생성한 발판들을 할당할 변수
    int currentIndex = 0;                       //사용할 현재 순번의 발판

    //초반에 생성한 발판들을 숨겨둘 위치
    Vector2 poolPostion = new Vector2(0, -25);
    float lastSpawnTime;                        //마지막 배치 시점

    // Start is called before the first frame update
    void Start()
    {
        //count만큼 공간을 가지는 새로운 발판 배열을 생성
        platforms = new GameObject[count];

        //count만큼 루프하면서 발판을 생성
        for(int i = 0; i < count; i++)
        {
            //platform원본으로 새 발판을 poolPosition위치에 복제 생성
            //생성한 발판을 platforms배열에 할당
            platforms[i] = Instantiate(platform, poolPostion, Quaternion.identity);
        }

        //마지막 배치 시점을 초기화
        lastSpawnTime = 0f;
        //다음번 배치까지의 시간 간격을 0으로 초기화
        timeBetSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //게임오버 상태일때는 동작하지 않음
        if (GameManager.instance.isGameOver) return;
        //게임 시작 전일 때 동작하지 않음
        if (!GameManager.isGameStart) return;

        //마지막 배치 시점에서 timeBetSpawn이상 시간이 흘렀다면
        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            //기록된 마지막 배치 시점을 현재 시점으로 갱신
            lastSpawnTime = Time.time;

            //다음 배치까지의 시간 간격을 timeBetSpawnMin, timeBetSpawnMax사이에서 랜덤 설정
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            //배치할 위치의 높이 yMin, yMax사이 값으로 랜덤 설정
            float yPos = Random.Range(yMin, yMax);

            //사용할 현재 순번의 발판 게임 오브젝트를 비활성화하고 즉시 활성화
            //이때 발판의 플랫폼 컴포넌트의 OnEnable메서드가 실행됨
            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);

            //현재 순번의 발판을 화면 오른쪽에 배치
            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);
            //순번 넘기기
            currentIndex++;

            //마지막 순번에 도달했을 경우 순번을 리셋
            if (currentIndex >= count)
            {
                currentIndex = 0;
            }
        }
    }
}
