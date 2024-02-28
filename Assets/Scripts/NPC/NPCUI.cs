using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class NPCUI : MonoBehaviour // npc�� ������ �ִ� UI
{
    public GameObject interactCanvas;
    public GameObject pressSpace;
    public NPCTalkData nTD;
    
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    //public Queue<string> sentence;
    //public string curSentence;
    //private bool isTyping;
    //public GameObject nextText;

    public DialogueWindow dia;
    private void Awake()
    {
        nameText = dia.NPCName;
        dialogueText = dia.dialogue;
        nameText.text = nTD.Name;
        dialogueText.text = nTD.introduce;
    }
    //private void Start()
    //{
    //    sentence = new Queue<string>();
    //}

    public void Ontalk(NPC npc) // ��ȭ
    {
        dia.GiveComponent(npc.bubble.nTD);
    }
    //public void OnDialogue(string[] lines)
    //{
    //    sentence.Clear();
    //    foreach(string line in lines)
    //    {
    //        sentence.Enqueue(line);
    //    }
    //    dia.dGroup.alpha = 1;
    //    dia.dGroup.blocksRaycasts = true;
    //    nextSentence();
    //}

    //public void nextSentence()
    //{
    //    if(sentence.Count != 0)
    //    {
    //        if (alreadyMet == false) { alreadyMet = true; }
    //        // TODO : ù���� ����
    //        else { dialogueText.text = nTD.des; }
    //        // ù���� ����
    //        nTD.Run();
    //        curSentence = dialogueText.text;
    //        isTyping = true;
    //        nextText.SetActive(false);
    //        curSentence = sentence.Dequeue();
    //        StartCoroutine(Typing(curSentence));
    //    }
    //    else
    //    {
    //        dia.dGroup.alpha = 0;
    //        dia.dGroup.blocksRaycasts = false;
    //    }

    //}

    //IEnumerator Typing(string line)
    //{
    //    dialogueText.text = "";
    //    foreach(char letter in line.ToCharArray())
    //    {
    //        dialogueText.text += letter;
    //        yield return new WaitForSeconds(.1f); // �ƴ� typingspeed ���� �־��ֱ� // npc Ÿ���� �ӵ�

    //    }
    //}
    //private void Update()
    //{
    //    if(dialogueText.text.Equals(curSentence) )
    //    {
    //        nextText.SetActive(true);
    //        isTyping = false;
            
    //        if(Input.GetKeyDown(KeyCode.Space)) 
    //        {
    //            if (!isTyping)
    //            nextSentence();
    //        }
    //    }
    //}

}