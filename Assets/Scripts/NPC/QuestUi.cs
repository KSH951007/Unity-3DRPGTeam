using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuestUi : MonoBehaviour
{
    GameObject questWindow;
    bool qwIsOpen = false;
    
    GameObject questDetail;
    bool qdIsOpen = false;
    
    GameObject conversationMenu;
    bool cmIsOpen = false;
    
    GameObject talkWindow;
    bool twIsOpen = false;
    TextMeshPro NPCname;

    private void Awake()
    {
        questWindow = GameObject.Find("QuestPanel");
        questDetail = GameObject.Find("QuestDetail");
        conversationMenu = GameObject.Find("NPCmenu");
        talkWindow = GameObject.Find("TalkWindow");
        NPCname = GameObject.Find("Name").GetComponent<TextMeshPro>();
        qwIsOpen = false;
        qdIsOpen = false;
        cmIsOpen = false;
        twIsOpen = false;
    }
    private void Start()
    {
        questWindow.SetActive(false);
        questDetail.SetActive(false);
        conversationMenu.SetActive(false);
        talkWindow.SetActive(false);
    }

    public void showQW() // npc가 대화 내용과 퀘스트 내용을 가지고 있으면 파라미터를 쓸 필요 없이 사용가능
    {
        qwIsOpen = !qwIsOpen;
        questWindow.SetActive(qwIsOpen);
    }
    public void showQD()
    {
        qdIsOpen = !qdIsOpen;
        questDetail.SetActive(qdIsOpen);
    }
    public void showConversation()
    {
        cmIsOpen = !cmIsOpen;
        conversationMenu.SetActive(cmIsOpen);
    }
    public void talkWithNPC()
    {
        twIsOpen = !twIsOpen;
        talkWindow.SetActive(twIsOpen);
    }
}