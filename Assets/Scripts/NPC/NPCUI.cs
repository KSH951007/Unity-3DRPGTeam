using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class NPCUI : MonoBehaviour // npc가 가지고 있는 UI
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


    public void Ontalk(NPC npc) // 대화
    {
        dia.GiveComponent(npc.bubble.nTD);
    }
}