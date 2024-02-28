using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueWindow : MonoBehaviour
{
    public TextMeshProUGUI NPCName;
    public TextMeshProUGUI dialogue;
    public CanvasGroup dGroup;
    NPCTalkData talkData;

    public Queue<string> sentence;
    public string curSentence;
    private bool isTyping;
    public GameObject nextText;

    public GameObject questAcceptWindow;
    public QuestView questWindow;

    private void Awake()
    {
        dGroup = GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        sentence = new Queue<string>();
    }

    public void GiveComponent(NPCTalkData Data)
    {
        Data.Run();
        talkData = Data;
        NPCName.text = Data.Name;
        dialogue.text = Data.des;
    }
    //public void OnDialogue(string[] lines)
    //{
    //    sentence.Clear();
    //    foreach (string line in lines)
    //    {
    //        sentence.Enqueue(line);
    //    }
    //    dGroup.alpha = 1;
    //    dGroup.blocksRaycasts = true;
    //    nextSentence();
    //}

    //public void nextSentence()
    //{
    //    if (sentence.Count != 0)
    //    {
    //        curSentence = dialogue.text;
    //        isTyping = true;
    //        nextText.SetActive(false);
    //        curSentence = sentence.Dequeue();
    //        StartCoroutine(Typing(curSentence));
    //    }
    //    else
    //    {
    //        dGroup.alpha = 0;
    //        dGroup.blocksRaycasts = false;
    //    }

    //}


    private void Update()
    {
        if (!isTyping)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                questDialogueWithNPC();
            }
        }
    }

    public void questDialogueWithNPC() // onclicked �̺�Ʈ ���
    {
        StartCoroutine(QuestTyping());
    }
    public void AddElement(Quest quest, UnityAction<bool> onClicked)
    {
        //var element = Instantiate(elementTextPrefab, transform);
        //element.text = quest.DisplayName;

        //var toggle = element.GetComponent<Toggle>();
        //toggle.group = toggleGroup;
        //toggle.onValueChanged.AddListener(onClicked);

        //elementsByQuest.Add(quest, element.gameObject);
    }
    IEnumerator QuestTyping()
    {
        dialogue.text = "";
        foreach (string s in talkData.questDialogue)
        {
            talkData.des = s;
            foreach (char letter in s)
            {
                if (isTyping)
                {
                    dialogue.text += letter;
                }
                yield return new WaitForSeconds(.1f); // �ƴ� typingspeed ���� �־��ֱ� // npc Ÿ���� �ӵ�
            }
            yield return new WaitForSeconds(10f);                                  // npc�� ���� ����Ʈ ����â�� �ڷ�ƾ�� ������ �Լ����� Ȱ��ȭ
        }
        questAcceptWindow.SetActive(true); // TODO : ����Ʈ ���� onclicked �̺�Ʈ ��� ����� questaccpetwindow�� ��Ȱ��ȭ
    }
    private void Skip(string s) // ��ŵ��
    {
        dialogue.text = s;
        isTyping = false;
    }

    //npc ����Ʈ ����â onclicked // TODO : ����Ʈ ������ // questGiver�� ���ؼ� ����
    public void GetQuest()
    {
        questWindow.AddQuestToActiveListView(talkData.npcSubQuest);
    }

}