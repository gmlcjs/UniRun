using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;             //�̱����� �Ҵ��� ��������

    Vector3 spawnPosition = new Vector3(-7, 1, 0); // ������ġ

    public bool isGameOver = false;                 //���ӿ��� ����
    [SerializeField] TextMeshProUGUI scoreText;     //������ ����� TextUI
    //[SerializeField] GameObject titleText;          //���� ������ �� �������� ������Ʈ
    [SerializeField] TextMeshProUGUI recordText;    //���� ���� �� �ְ� ��� ���
    public GameObject gameOverUI;                   //���ӿ����� �� Ȱ��ȭ �� ������Ʈ

    public static int score = 0;                                  //���� ����
    public int publicScore = 0; // ���� ���ھ�

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

        //���ӿ������¿��� ������ ������� �� �ְ� ó��
        if (isGameOver)
        {
            if (Input.GetMouseButtonDown(0)) { SceneManager.LoadScene("StartScen"); } // ��Ŭ��
            if (Input.touchCount > 0) { SceneManager.LoadScene("StartScen"); } // ��ġ ����

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
}
