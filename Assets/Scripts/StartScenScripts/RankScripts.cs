using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class RankScripts : MonoBehaviour
{
    public GameObject Rank; // 랭크 출력

    public TextMeshProUGUI[] rankingTextsList;  // 순위 출력용 Text 배열
    public TextMeshProUGUI rankingTexts;  // 순위 출력용 Text 배열
    public string[] ranking;     // 사용자 이름 배열
    public int[] scores;         // 점수 배열


    // 랭크창 활성화 및 비활성화
    public void OnAButtonClick()
    {
        Rank.SetActive(!Rank.activeSelf);
        if (Rank.activeSelf) ShowRanking();
    }

    // 랭킹 순위 출력
    public void ShowRanking()
    {
        string rankingAill = "";
        Debug.Log(rankingTextsList.Length);
        for (int i = 0; i < rankingTextsList.Length; i++)
        {
            // 출력할 순위, 이름, 점수를 결합하여 하나의 문자열로 만듦
            rankingAill += "닉네임 : " + PlayerPrefs.GetString("BestRecordString" + i) + " \t스코어 :" + PlayerPrefs.GetInt("BestRecordInt" + i)+ "\n\n";
            // 각 TextMeshProUGUI UI 요소에 순위 및 점수 정보를 출력
            //rankingTexts[i].text = rankingInfo;
        }

        // 하나의 문자로 출력
        rankingTexts.text = rankingAill;
        Debug.Log(rankingTexts.text);

    }
}
