using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Runtime.CompilerServices;

public class DialogueWindow : MonoBehaviour
{
    public TextMeshProUGUI NPCName;
    public TextMeshProUGUI dialogue;
    public TextMeshProUGUI dialogueSnd;

    NPCTalkData talkData;
    private bool isTyping;
    string curText;
    string otherText;
    int questDialogueInt;

    public GameObject questAcceptWindow;
    public QuestView questWindow;
    //public DialogueWindowView window;

    private void Awake()
    {

    }

    public void GiveComponent(NPCTalkData Data)
    {
        Data.Run();
        talkData = Data;
        NPCName.text = Data.Name;
        curText = Data.des;
    }

    private void Update()
    {
        dialogue.text = curText;
        if (GameManager.Instance.plin.playerID >= talkData.questID)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                ++questDialogueInt;
                isTyping = true;
                if (talkData.questDialogue.Length == questDialogueInt)
                {
                    transform.gameObject.SetActive(false);
                    questAcceptWindow.SetActive(true); // TODO : ����Ʈ ���� onclicked �̺�Ʈ ��� ����� questaccpetwindow�� ��Ȱ��
                }
                StartCoroutine(Typing(curText));
            }
        }
        if (isTyping)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Skip();
            }
        }
    }

    private void Skip() // ��ŵ��
    {
        isTyping = false;
    }
    IEnumerator Typing(string st)
    {
        foreach (char letter in st)
        {
            if (isTyping)
            {
                curText += letter;
            }
            else // TODO : ��ŵ ���
            {
                curText = st;
            }
            yield return new WaitForSeconds(.1f); // �ƴ� typingspeed ���� �־��ֱ� // npc Ÿ���� �ӵ�
        }
        isTyping = false;
    }

    public void changeDialogue()
    {
        curText = otherText;
        otherText = "";
    }
    public void ifSayQuestdenied()
    {
        questDialogueInt = 0;
    }
}
//            isTyping = true;
//            
//            
//        }
//       
//       
//       
//       
//       
//       
//       
//    }
//}

//IEnumerator runThree()
//{
//    talkData.runForQuest(curText, questDialogueInt);
//    Typing(curText);
//    yield return new WaitForSeconds(1);
//}

















    //
    //
    //
    //

    ////npc ����Ʈ ����â onclicked // TODO : ����Ʈ ������
    //public void GetQuest()
    //{
    //    if (talkData.npcSubQuest.IsAcceptable && !QuestSystem.Instance.ContainsInCompleteQuests(talkData.npcSubQuest))
    //           QuestSystem.Instance.Register(talkData.npcSubQuest);
    //}
