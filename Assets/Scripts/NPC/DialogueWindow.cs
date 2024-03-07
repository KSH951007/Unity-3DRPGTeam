using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Runtime.CompilerServices;
using System.Linq;
using UnityEditor.Experimental.GraphView;

public class DialogueWindow : MonoBehaviour
{
    public TextMeshProUGUI NPCName;
    public TextMeshProUGUI dialogue;// �ΰ��� �ؽ�Ʈ�ٿ� �ؽ�Ʈ�� ��������
    public TextMeshProUGUI dialogueSnd;

    NPCData talkData;
    private bool isTyping;// ""
    bool typingSnd; // ��ȭâ ��Ȱ��ȭ�� ����
    string curText;
    string diaText;
    string otherText;
    public Queue<string> questSentence;

    public GameObject questAcceptWindow;
    public QuestGiver giver;
    public void GiveComponent(NPCData Data)
    {
        Data.Run();
        talkData = Data;
        NPCName.text = Data.Name;
        curText = Data.des;
        questDialogue(Data.questDialogue);
    }

    private void Update()
    {
        if (!typingSnd)
        {
            dialogue.text = curText;
        }
        else
        {
            dialogueSnd.text = curText;
            dialogue.text = otherText;
        }

        if (isTyping)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Skip();
            }
        }
    }

    public void Skip() // ��ŵ��
    {
        isTyping = false;
    }
    IEnumerator Typing(string st)
    {
        if (typingSnd)
        {
            otherText = curText;
        }
        curText = "";
        foreach (char letter in st)
        {
            if (isTyping)
            {
                curText += letter;
                yield return new WaitForSeconds(.2f);
            }
<<<<<<< HEAD
=======
            //else // TODO : ��ŵ ���
            //{
            //    curText = "";
            //    curText = st;
            //}
            //yield return null;
>>>>>>> Sample
        }
        if(!isTyping)
        {
            curText = st;
        }

        typingSnd = true;
        isTyping = false;
    }    
    private void nowTyping()
    {
        isTyping = true;
        StartCoroutine(Typing(diaText));
    }
    public void OnDialogue()
    {
        if (GameManager.Instance.plin.playerID >= talkData.questID)// TODO : player Ŭ����
        {
            dialogueFlow();
        }
        else if(talkData.npcSubQuest.IsComplete)
        {
            curText = "completed quest.";
        }
        else
        {
            curText = "you can't accept Quest.(Level Issue)";
        }
    }
    private void questDialogue(string[]lines)
    {
        questSentence = new Queue<string>();
        questSentence.Clear();
        foreach(var line in lines)
        {
            questSentence.Enqueue(line);
        }
    }
    public void dialogueFlow()
    {
        if (GameManager.Instance.plin.playerID >= talkData.questID)// TODO : player Ŭ����
        {
            if (questSentence.Count > 0)
            {
                diaText = questSentence.Dequeue();
                nowTyping();
            }
            else if (questSentence.Count == 0)
            {
                isQuit();
                questAcceptWindow.SetActive(true);
                transform.gameObject.SetActive(false);
            }
        }
        else
        {
            isQuit();
            transform.gameObject.SetActive(false);
        }
    }
    public void isQuit() // ��Ŭ�� �̺�Ʈ
    {
        otherText = "";
        curText = "";
        isTyping = false;
        typingSnd = false;
    }
    public void QuestAccept()
    {
        QuestSystem.Instance.Register(talkData.npcSubQuest);
<<<<<<< HEAD
        giver.giveQuest(talkData.npcSubQuest);
=======
        giver.addQuest(talkData.npcSubQuest);
>>>>>>> Sample
        otherText = "";
        curText = "";
        isTyping = false;
        typingSnd = false;
    }
}