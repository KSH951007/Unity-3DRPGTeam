using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCUI : MonoBehaviour // npc가 가지고 있는 UI
{
    public GameObject interactCanvas;
    public GameObject pressSpace;
    public NPCTalkData nTD;
    
    private bool firstMet = false; // WILLDO : NPC 첫인사 구현 // chatBubble 클래스
    public TextMeshPro nameText;
    public TextMeshPro dialogueText;

    public GameObject nextText;

    public Queue<string> sentence;
    public string curSentence;
    private bool isTyping;

    private void Awake()
    {
        interactCanvas = GameObject.Find("InteractCanvas");
        pressSpace = GameObject.Find("PressSpace");
        nameText.text = nTD.Name;
        curSentence = nTD.introduce;
    }
    private void Start()
    {
        sentence = new Queue<string>();
    }

    public void OnDialogue(string[] lines)
    {
        sentence.Clear();
        foreach(string line in lines)
        {
            sentence.Enqueue(line);
        }
    }

    public void nextSentence()
    {
        if(sentence.Count != 0)
        {
            if (firstMet == false) { firstMet = true; }
            // TODO : 첫만남 구현
            else { curSentence = nTD.des; }
            // 첫만남 이후
            isTyping = true;
            nextText.SetActive(false);
            curSentence = sentence.Dequeue();
            StartCoroutine(Typing(curSentence));
        }
    }

    IEnumerator Typing(string line)
    {
        dialogueText.text = "";
        foreach(char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(.1f); // 아님 typingspeed 변수 넣어주기 // npc 타이핑 속도

        }
    }
    private void Update()
    {
        if(dialogueText.text.Equals(curSentence) )
        {
            nextText.SetActive(true);
            isTyping = false;
            
            if(Input.GetKeyDown(KeyCode.Space)) 
            {
                if (!isTyping)
                nextSentence();
            }
        }
    }

}