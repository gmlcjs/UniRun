using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMainScripts : MonoBehaviour
{
    public GameObject infoText;  // InfoText 오브젝트를 연결할 변수
    [SerializeField] TextMeshProUGUI recordText;    //최고 기록 출력

    void Update()
    {
        // 마우스 왼쪽 버튼 클릭 감지 (0은 왼쪽 버튼)
        if (Input.GetMouseButtonDown(0)) { SceneManager.LoadScene("MainScen"); }

        // 터치 감지 (화면에 터치가 있을 때)
        if (Input.touchCount > 0) { SceneManager.LoadScene("MainScen"); }
    }

    void Start()
    {
        StartCoroutine(ToggleTextVisibility());
        recordText.text = "최고 기록 : " + PlayerPrefs.GetInt("BestRecord"); // 최고 기록 출력
    }

    IEnumerator ToggleTextVisibility()
    {
        while (true)  // 계속 반복
        {
            infoText.SetActive(!infoText.activeSelf);  // InfoText의 활성화 상태를 토글
            yield return new WaitForSeconds(1f);  // 1초 대기
        }
    }
   

}
