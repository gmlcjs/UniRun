using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    public static SelectCharacter instance;
    public List<GameObject> characterList = new List<GameObject>();

    public static string selectCharacter; // 선택한 캐릭터 프리팹이름

    public GameObject Player; // 플레이어할 캐릭터

    void Start()
    {
        
        instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    public void intputPlayer()
    {
        if (Player == null)
        {
            for (int i =0; i < characterList.Count; i++)
            {
                if (characterList[i].name == selectCharacter)
                {
                    Player = characterList[i];
                }
            }
        }
    }



}
