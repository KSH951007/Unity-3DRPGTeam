using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public int playProgress; // 플레이 진척도에 따라 // 퀘스트 연계
    public string questString; // 각 엔피씨마다 퀘스트용 텍스트
    public string questComplete; // 퀘스트 완료


    public void GetQuest(int progress)
    {
        if (progress >= playProgress)
        {
            // 퀘스트 확인가능
        }
        else
        {
            // 퀘스트 확인 불가능
        }

    }
}
