using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;             //싱글턴을 할당할 전역변수

    Vector3 spawnPosition = new Vector3(-7, 1, 0); // 시작위치

    public bool isGameOver = false;                 //게임오버 상태
    [SerializeField] TextMeshProUGUI scoreText;     //점수를 출력한 TextUI
    //[SerializeField] GameObject titleText;          //게임 시작할 때 보여지는 오브젝트
    [SerializeField] TextMeshProUGUI recordText;    //게임 종료 후 최고 기록 출력
    public GameObject gameOverUI;                   //게임오버일 때 활성화 될 오브젝트

    public static int score = 0;                                  //게임 점수
    public int publicScore = 0; // 전역 스코어

    public GameObject basicCharacter; // 기본캐릭터
    // 기본설정
    public static float speed = 10; // 스피드(이속 설정)
    public static float jumpForce = 500f; // 점프힘

    // 라운드 변경 스코어
    public static int labelUp = 10; // 10 스코어부터 변경

    public static bool isGameStart = false;         //게임 시작 여부 확인 변수
    [SerializeField] BackGroundLoop backGroundLoop; // 백그라운드





    //게임 시작과 인스턴스를 할당하고
    void Awake()
    {
        SelectCharacter.instance.intputPlayer();
        if (SelectCharacter.instance.Player != null)
        {
            Instantiate(SelectCharacter.instance.Player, spawnPosition, Quaternion.identity);
        }
        else
        {
            basicCharacter.SetActive(true); // 캐릭터가 선택안되어있을때 기본캐릭터 출력
        }

        //싱글턴 변수 instance가 null인지 확인
        if (instance == null)
        {
            //자기자신을 할당
            instance = this;
        }
        else
        {
            //자신의 게임 오브젝트를 파괴
            Destroy(gameObject);
        }

        if (isGameStart)
        {
            //titleText.SetActive(false);
            scoreText.gameObject.SetActive(true);
        }
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) // ESC 누를경우 게임종료
        {
            Application.Quit();
        }

        //게임오버상태에서 게임을 재시작할 수 있게 처리
        if (isGameOver)
        {
            if (Input.GetMouseButtonDown(0)) { SceneManager.LoadScene("StartScen"); } // 좌클릭
            if (Input.touchCount > 0) { SceneManager.LoadScene("StartScen"); } // 터치 감지

        }
    }

    /// <summary>
    /// StartButton클릭 시 isGameStart변수를 변경
    /// </summary>
    public void OnClickStartButton()
    {
        isGameStart = true;
    }

    /// <summary>
    /// 점수를 증가시키는 메서드
    /// </summary>
    /// <param name="newScore"></param>
    public void AddScore(int newScore)
    {
        //게임오버가 아니라면 처리 진행
        if (!isGameOver)
        {
            //점수 증가
            score += newScore;
            //점수 ui표시
            scoreText.text = $"Score : {score}";
            publicScore = score;
            backGroundLoop.BackgrounChage();
        }
    }

    /// <summary>
    /// 플레이어 캐릭터 사망 시 게임오버를 실행하는 메서드
    /// </summary>
    public void OnPlayerDead()
    {
        isGameOver = true;

        //PlayerPrefs를 활용해서 최고 점수를 기록하고
        //현재 점수가 최고 점수 이상이면 교체하는 로직 구현
        //최고 점수를 가져와서 변수로 담아두는 역할
        int bestScore = PlayerPrefs.GetInt("BestRecord");
        if (score > bestScore)
        {
            //최고 점수 갱신
            bestScore = score;

            //PlayerPrefs에 최고 점수를 갱신함
            PlayerPrefs.SetInt("BestRecord", bestScore);
        }
        recordText.text = "최고기록 : " + bestScore;

        gameOverUI.SetActive(true);
    }
}
