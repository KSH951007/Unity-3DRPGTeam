using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCUI : MonoBehaviour // npc�� ������ �ִ� UI
{
    public GameObject interactCanvas;
    public GameObject pressSpace;
    public NPCTalkData nTD;
    
    private bool firstMet = false; // WILLDO : NPC ù�λ� ���� // chatBubble Ŭ����
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
            // TODO : ù���� ����
            else { curSentence = nTD.des; }
            // ù���� ����
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
            yield return new WaitForSeconds(.1f); // �ƴ� typingspeed ���� �־��ֱ� // npc Ÿ���� �ӵ�

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