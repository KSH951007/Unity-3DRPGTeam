using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;

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
    public void OnDialogue(string[] lines)
    {
        sentence.Clear();
        foreach (string line in lines)
        {
            sentence.Enqueue(line);
        }
        dGroup.alpha = 1;
        dGroup.blocksRaycasts = true;
        nextSentence();
    }

    public void nextSentence()
    {
        if (sentence.Count != 0)
        {
            curSentence = dialogue.text;
            isTyping = true;
            nextText.SetActive(false);
            curSentence = sentence.Dequeue();
            StartCoroutine(Typing(curSentence));
        }
        else
        {
            dGroup.alpha = 0;
            dGroup.blocksRaycasts = false;
        }

    }

    IEnumerator Typing(string line)
    {
        dialogue.text = "";
        foreach (char letter in line.ToCharArray())
        {
            dialogue.text += letter;
            yield return new WaitForSeconds(.1f); // 아님 typingspeed 변수 넣어주기 // npc 타이핑 속도
        }
    }
    private void Update()
    {
        if (dialogue.text.Equals(curSentence))
        {
            nextText.SetActive(true);
            isTyping = false;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!isTyping)
                    nextSentence();
            }
        }
    }

}