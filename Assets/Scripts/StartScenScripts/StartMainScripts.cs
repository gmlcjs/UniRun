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
        // ���콺 ���� ��ư Ŭ�� ���� (0�� ���� ��ư)  or ��ġ ���� (ȭ�鿡 ��ġ�� ���� ��)
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Raycast2D�� ����� �浹�� ������Ʈ ����
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                //Debug.Log($"������ ������Ʈ : {hit.collider.gameObject.name}");
                SelectCharacter.selectCharacter = hit.collider.gameObject.name; // ������ ĳ���� �̸� ����
                SelectCharacter.instance.intputPlayer();
                SceneManager.LoadScene("MainScen");
            }

            
        }

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
