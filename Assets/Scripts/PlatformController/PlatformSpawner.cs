using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platform;                 //�÷��� ������
    public int count = 3;                       //������ ���� ��
    public float timeBetSpawnMin = 0.25f;       //���� ��ġ������ �ð� ���� �ּڰ�
    public float timeBetSpawnMax = 2.25f;       //���� ��ġ������ �ð� ���� �ִ�

    float timeBetSpawn;                         //���� ��ġ������ �ð� ����
    float yMin = -2.5f;                         //��ġ�� ��ġ�� �ּ� y��
    float yMax = 1.5f;                          //��ġ�� ��ġ�� �ִ� y��
    float xPos = 20f;                           //��ġ�� ��ġ�� x��

    GameObject[] platforms;                     //�̸� ������ ���ǵ��� �Ҵ��� ����
    int currentIndex = 0;                       //����� ���� ������ ����

    //�ʹݿ� ������ ���ǵ��� ���ܵ� ��ġ
    Vector2 poolPostion = new Vector2(0, -25);
    float lastSpawnTime;                        //������ ��ġ ����

    // Start is called before the first frame update
    void Start()
    {
        //count��ŭ ������ ������ ���ο� ���� �迭�� ����
        platforms = new GameObject[count];

        //count��ŭ �����ϸ鼭 ������ ����
        for(int i = 0; i < count; i++)
        {
            //platform�������� �� ������ poolPosition��ġ�� ���� ����
            //������ ������ platforms�迭�� �Ҵ�
            platforms[i] = Instantiate(platform, poolPostion, Quaternion.identity);
        }

        //������ ��ġ ������ �ʱ�ȭ
        lastSpawnTime = 0f;
        //������ ��ġ������ �ð� ������ 0���� �ʱ�ȭ
        timeBetSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //���ӿ��� �����϶��� �������� ����
        if (GameManager.instance.isGameOver) return;
        //���� ���� ���� �� �������� ����
        if (!GameManager.isGameStart) return;

        //������ ��ġ �������� timeBetSpawn�̻� �ð��� �귶�ٸ�
        if (Time.time >= lastSpawnTime + timeBetSpawn)
        {
            //��ϵ� ������ ��ġ ������ ���� �������� ����
            lastSpawnTime = Time.time;

            //���� ��ġ������ �ð� ������ timeBetSpawnMin, timeBetSpawnMax���̿��� ���� ����
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);

            //��ġ�� ��ġ�� ���� yMin, yMax���� ������ ���� ����
            float yPos = Random.Range(yMin, yMax);

            //����� ���� ������ ���� ���� ������Ʈ�� ��Ȱ��ȭ�ϰ� ��� Ȱ��ȭ
            //�̶� ������ �÷��� ������Ʈ�� OnEnable�޼��尡 �����
            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);

            //���� ������ ������ ȭ�� �����ʿ� ��ġ
            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);
            //���� �ѱ��
            currentIndex++;

            //������ ������ �������� ��� ������ ����
            if (currentIndex >= count)
            {
                currentIndex = 0;
            }
        }
    }
}
