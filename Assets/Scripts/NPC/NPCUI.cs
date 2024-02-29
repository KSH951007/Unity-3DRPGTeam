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

    public DialogueWindow dia;
    private void Awake()
    {
        nameText = dia.NPCName;
        nameText.text = nTD.Name;
    }


    public void Ontalk(NPC npc) // ��ȭ
    {
        dia.GiveComponent(npc.bubble.nTD);
    }
}