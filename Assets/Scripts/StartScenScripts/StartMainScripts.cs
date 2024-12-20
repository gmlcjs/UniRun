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
        // 마우스 왼쪽 버튼 클릭 감지 (0은 왼쪽 버튼)  or 터치 감지 (화면에 터치가 있을 때)
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Raycast2D를 사용해 충돌한 오브젝트 감지
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                //Debug.Log($"선택한 오브젝트 : {hit.collider.gameObject.name}");
                SelectCharacter.selectCharacter = hit.collider.gameObject.name; // 선택한 캐릭터 이름 저장
                SelectCharacter.instance.intputPlayer();
                SceneManager.LoadScene("MainScen");
            }

            
        }

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
