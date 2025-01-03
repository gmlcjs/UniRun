using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class RankScripts : MonoBehaviour
{
    public GameObject Rank; // ��ũ ���

    public TextMeshProUGUI[] rankingTextsList;  // ���� ��¿� Text �迭
    public TextMeshProUGUI rankingTexts;  // ���� ��¿� Text �迭
    public string[] ranking;     // ����� �̸� �迭
    public int[] scores;         // ���� �迭


    // ��ũâ Ȱ��ȭ �� ��Ȱ��ȭ
    public void OnAButtonClick()
    {
        Rank.SetActive(!Rank.activeSelf);
        if (Rank.activeSelf) ShowRanking();
    }

    // ��ŷ ���� ���
    public void ShowRanking()
    {
        string rankingAill = "";
        Debug.Log(rankingTextsList.Length);
        for (int i = 0; i < rankingTextsList.Length; i++)
        {
            // ����� ����, �̸�, ������ �����Ͽ� �ϳ��� ���ڿ��� ����
            rankingAill += "�г��� : " + PlayerPrefs.GetString("BestRecordString" + i) + " \t���ھ� :" + PlayerPrefs.GetInt("BestRecordInt" + i)+ "\n\n";
            // �� TextMeshProUGUI UI ��ҿ� ���� �� ���� ������ ���
            //rankingTexts[i].text = rankingInfo;
        }

        // �ϳ��� ���ڷ� ���
        rankingTexts.text = rankingAill;
        Debug.Log(rankingTexts.text);

    }
}
