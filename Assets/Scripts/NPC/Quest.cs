using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public int playProgress; // �÷��� ��ô���� ���� // ����Ʈ ����
    public string questString; // �� ���Ǿ����� ����Ʈ�� �ؽ�Ʈ
    public string questComplete; // ����Ʈ �Ϸ�


    public void GetQuest(int progress)
    {
        if (progress >= playProgress)
        {
            // ����Ʈ Ȯ�ΰ���
        }
        else
        {
            // ����Ʈ Ȯ�� �Ұ���
        }

    }
}
