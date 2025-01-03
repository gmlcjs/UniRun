using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;             //�̱����� �Ҵ��� ��������

    Vector3 spawnPosition = new Vector3(-7, 1, 0); // ������ġ

    public bool isGameOver = false;                 //���ӿ��� ����
    [SerializeField] TextMeshProUGUI scoreText;     //������ ����� TextUI
    public GameObject RankObject; // ��ŷ UI
    //[SerializeField] GameObject titleText;          //���� ������ �� �������� ������Ʈ
    [SerializeField] TextMeshProUGUI recordText;    //���� ���� �� �ְ� ��� ���
    public GameObject gameOverUI;                   //���ӿ����� �� Ȱ��ȭ �� ������Ʈ
    [SerializeField] TMP_InputField userNameInput; // �÷��̾��� ������ �̸��� �Է¹޴� ����

    public static int score = 0;                                  //���� ����
    public int publicScore = 0; // ���� ���ھ�
    public Dictionary<string, int> rankingDictionary = new Dictionary<string, int>(); // ��ŷ �𽺳ʸ�
    int saveCount = 5; //  ���� ��
    List<string> ranking = new List<string>(new string[5]);
    List<int> scores = new List<int>(new int[5]);

    public GameObject basicCharacter; // �⺻ĳ����
    // �⺻����
    public static float speed = 10; // ���ǵ�(�̼� ����)
    public static float jumpForce = 500f; // ������

    // ���� ���� ���ھ�
    public static int labelUp = 10; // 10 ���ھ���� ����

    public static bool isGameStart = false;         //���� ���� ���� Ȯ�� ����
    [SerializeField] BackGroundLoop backGroundLoop; // ��׶���

    //���� ���۰� �ν��Ͻ��� �Ҵ��ϰ�
    void Awake()
    {
        //PlayerPrefs.DeleteAll(); // �ʱ�ȭ
        //PlayerPrefs.SetInt("BestRecord", 0);
        SelectCharacter.instance.intputPlayer();
        if (SelectCharacter.instance.Player != null)
        {
            Instantiate(SelectCharacter.instance.Player, spawnPosition, Quaternion.identity);
        }
        else
        {
            basicCharacter.SetActive(true); // ĳ���Ͱ� ���þȵǾ������� �⺻ĳ���� ���
        }

        //�̱��� ���� instance�� null���� Ȯ��
        if (instance == null)
        {
            //�ڱ��ڽ��� �Ҵ�
            instance = this;
        }
        else
        {
            //�ڽ��� ���� ������Ʈ�� �ı�
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
        if (Input.GetKey(KeyCode.Escape)) // ESC ������� ��������
        {
            Application.Quit();
        }

        //���ӿ������¿��� ������ ������� �� �ְ� ó�� �׾�����,
        if (isGameOver)
        {
            RankObject.SetActive(true);
            //if (Input.GetMouseButtonDown(0)) { SceneManager.LoadScene("StartScen"); } // ��Ŭ��
            //if (Input.touchCount > 0) { SceneManager.LoadScene("StartScen"); } // ��ġ ����
        }
    }

    /// <summary>
    /// StartButtonŬ�� �� isGameStart������ ����
    /// </summary>
    public void OnClickStartButton()
    {
        isGameStart = true;
    }

    /// <summary>
    /// ������ ������Ű�� �޼���
    /// </summary>
    /// <param name="newScore"></param>
    public void AddScore(int newScore)
    {
        //���ӿ����� �ƴ϶�� ó�� ����
        if (!isGameOver)
        {
            //���� ����
            score += newScore;
            //���� uiǥ��
            scoreText.text = $"Score : {score}";
            publicScore = score;
            backGroundLoop.BackgrounChage();
        }
    }

    /// <summary>
    /// �÷��̾� ĳ���� ��� �� ���ӿ����� �����ϴ� �޼���
    /// </summary>
    public void OnPlayerDead()
    {
        isGameOver = false;
        isGameOver = true;

        //PlayerPrefs�� Ȱ���ؼ� �ְ� ������ ����ϰ�
        //���� ������ �ְ� ���� �̻��̸� ��ü�ϴ� ���� ����
        //�ְ� ������ �����ͼ� ������ ��Ƶδ� ����
        int bestScore = PlayerPrefs.GetInt("BestRecord");
        if (score > bestScore)
        {
            //�ְ� ���� ����
            bestScore = score;

            //PlayerPrefs�� �ְ� ������ ������
            PlayerPrefs.SetInt("BestRecord", bestScore);
        }
        recordText.text = "�ְ��� : " + bestScore;

        gameOverUI.SetActive(true); 
    }

    public void ScoreRecord()
    {
        string currentName = userNameInput.text;
        int currentScore = score;
        //PlayerPrefs.DeleteAll(); // �ʱ�ȭ
        for (int i = 0; i < saveCount; i++)
        {
            ranking[i] = PlayerPrefs.GetString("BestRecordString" + (i));
            scores[i] = PlayerPrefs.GetInt("BestRecordInt" + (i));
            //Debug.Log(i + "��° : " +PlayerPrefs.GetString("BestRecordString" + (i))+ PlayerPrefs.GetInt("BestRecordString" + (i)));

        }

        for (int i = 0; i < saveCount; i++)
        {
            Debug.Log(i);
            //Debug.Log(i+"��° : " +currentScore +":"+ scores[i - 1]);
            if (currentScore > scores[i])
            {
                ranking.Insert(i, currentName);
                scores.Insert(i, currentScore);
                break;
            }
        }

        // ���� ��ŷ ����
        for (int i = 0; i < saveCount; i++)
        {
            PlayerPrefs.SetString("BestRecordString" + i, ranking[i]);
            PlayerPrefs.SetInt("BestRecordInt" + i, scores[i]);
            if (i == 1) PlayerPrefs.SetInt("BestRecord", scores[0]); // �� ���� ���� ����

        }

        gameOverUI.SetActive(true);

        // ó�� ���� ���ư�
        SceneManager.LoadScene("StartScen");

    }
    //TextMeshProUGUI[] rankingTexts;
    //// ��ŷ ���� ���
    //public void ShowRanking()
    //{
    //    for (int i = 0; i < ranking.Length; i++)
    //    {
    //        // ����� ����, �̸�, ������ �����Ͽ� �ϳ��� ���ڿ��� ����
    //        string rankingInfo = $"{i + 1}. {ranking[i]} - {scores[i]}��";

    //        // �� TextMeshProUGUI UI ��ҿ� ���� �� ���� ������ ���
    //        if (i < rankingTexts.Length)
    //        {
    //            rankingTexts[i].text = rankingInfo;
    //        }
    //    }
    //}


}
