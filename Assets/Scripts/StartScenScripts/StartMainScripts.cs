using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMainScripts : MonoBehaviour
{
    public GameObject infoText;  // InfoText ������Ʈ�� ������ ����
    [SerializeField] TextMeshProUGUI recordText;    //�ְ� ��� ���

    void Update()
    {
        // ���콺 ���� ��ư Ŭ�� ���� (0�� ���� ��ư)
        if (Input.GetMouseButtonDown(0)) { SceneManager.LoadScene("MainScen"); }

        // ��ġ ���� (ȭ�鿡 ��ġ�� ���� ��)
        if (Input.touchCount > 0) { SceneManager.LoadScene("MainScen"); }
    }

    void Start()
    {
        StartCoroutine(ToggleTextVisibility());
        recordText.text = "�ְ� ��� : " + PlayerPrefs.GetInt("BestRecord"); // �ְ� ��� ���
    }

    IEnumerator ToggleTextVisibility()
    {
        while (true)  // ��� �ݺ�
        {
            infoText.SetActive(!infoText.activeSelf);  // InfoText�� Ȱ��ȭ ���¸� ���
            yield return new WaitForSeconds(1f);  // 1�� ���
        }
    }
   

}
