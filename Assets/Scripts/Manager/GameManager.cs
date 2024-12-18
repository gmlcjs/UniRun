using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;             //�̱����� �Ҵ��� ��������

    public bool isGameOver = false;                 //���ӿ��� ����
    [SerializeField] TextMeshProUGUI scoreText;     //������ ����� TextUI
    [SerializeField] GameObject titleText;          //���� ������ �� �������� ������Ʈ
    [SerializeField] TextMeshProUGUI recordText;    //���� ���� �� �ְ� ��� ���
    public GameObject gameOverUI;                   //���ӿ����� �� Ȱ��ȭ �� ������Ʈ

    public static int score = 0;                                  //���� ����
    public static bool isGameStart = false;         //���� ���� ���� Ȯ�� ����

    //���� ���۰� �ν��Ͻ��� �Ҵ��ϰ�
    void Awake()
    {
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
            titleText.SetActive(false);
            scoreText.gameObject.SetActive(true);
        }
        // ���ӽ��۽� ���ʿ��� ��� ����
        GameObject.Find("StartBackground").SetActive(false);
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        //���ӿ������¿��� ������ ������� �� �ְ� ó��
        if (isGameOver && Input.GetMouseButtonDown(0))
        {
            //���ӿ��� ���¿��� ���콺 ��Ŭ������ �� �����
            SceneManager.LoadScene("MainScene");
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
        }
    }

    /// <summary>
    /// �÷��̾� ĳ���� ��� �� ���ӿ����� �����ϴ� �޼���
    /// </summary>
    public void OnPlayerDead()
    {
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
        recordText.text = "Best Record : " + bestScore;

        gameOverUI.SetActive(true);
    }
}
