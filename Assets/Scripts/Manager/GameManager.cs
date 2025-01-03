using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;             //싱글턴을 할당할 전역변수

    Vector3 spawnPosition = new Vector3(-7, 1, 0); // 시작위치

    public bool isGameOver = false;                 //게임오버 상태
    [SerializeField] TextMeshProUGUI scoreText;     //점수를 출력한 TextUI
    public GameObject RankObject; // 랭킹 UI
    //[SerializeField] GameObject titleText;          //게임 시작할 때 보여지는 오브젝트
    [SerializeField] TextMeshProUGUI recordText;    //게임 종료 후 최고 기록 출력
    public GameObject gameOverUI;                   //게임오버일 때 활성화 될 오브젝트
    [SerializeField] TMP_InputField userNameInput; // 플레이어한 유저의 이름을 입력받는 로직

    public static int score = 0;                                  //게임 점수
    public int publicScore = 0; // 전역 스코어
    public Dictionary<string, int> rankingDictionary = new Dictionary<string, int>(); // 랭킹 디스너리
    int saveCount = 5; //  저장 수
    List<string> ranking = new List<string>(new string[5]);
    List<int> scores = new List<int>(new int[5]);

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
        //PlayerPrefs.DeleteAll(); // 초기화
        //PlayerPrefs.SetInt("BestRecord", 0);
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

        //게임오버상태에서 게임을 재시작할 수 있게 처리 죽었을때,
        if (isGameOver)
        {
            RankObject.SetActive(true);
            //if (Input.GetMouseButtonDown(0)) { SceneManager.LoadScene("StartScen"); } // 좌클릭
            //if (Input.touchCount > 0) { SceneManager.LoadScene("StartScen"); } // 터치 감지
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
        isGameOver = false;
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

    public void ScoreRecord()
    {
        string currentName = userNameInput.text;
        int currentScore = score;
        //PlayerPrefs.DeleteAll(); // 초기화
        for (int i = 0; i < saveCount; i++)
        {
            ranking[i] = PlayerPrefs.GetString("BestRecordString" + (i));
            scores[i] = PlayerPrefs.GetInt("BestRecordInt" + (i));
            //Debug.Log(i + "번째 : " +PlayerPrefs.GetString("BestRecordString" + (i))+ PlayerPrefs.GetInt("BestRecordString" + (i)));

        }

        for (int i = 0; i < saveCount; i++)
        {
            Debug.Log(i);
            //Debug.Log(i+"번째 : " +currentScore +":"+ scores[i - 1]);
            if (currentScore > scores[i])
            {
                ranking.Insert(i, currentName);
                scores.Insert(i, currentScore);
                break;
            }
        }

        // 최종 랭킹 저장
        for (int i = 0; i < saveCount; i++)
        {
            PlayerPrefs.SetString("BestRecordString" + i, ranking[i]);
            PlayerPrefs.SetInt("BestRecordInt" + i, scores[i]);
            if (i == 1) PlayerPrefs.SetInt("BestRecord", scores[0]); // 젤 높은 점수 저장

        }

        gameOverUI.SetActive(true);

        // 처음 으로 돌아감
        SceneManager.LoadScene("StartScen");

    }
    //TextMeshProUGUI[] rankingTexts;
    //// 랭킹 순위 출력
    //public void ShowRanking()
    //{
    //    for (int i = 0; i < ranking.Length; i++)
    //    {
    //        // 출력할 순위, 이름, 점수를 결합하여 하나의 문자열로 만듦
    //        string rankingInfo = $"{i + 1}. {ranking[i]} - {scores[i]}점";

    //        // 각 TextMeshProUGUI UI 요소에 순위 및 점수 정보를 출력
    //        if (i < rankingTexts.Length)
    //        {
    //            rankingTexts[i].text = rankingInfo;
    //        }
    //    }
    //}


}
