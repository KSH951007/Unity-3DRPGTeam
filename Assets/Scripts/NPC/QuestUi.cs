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
    
    GameObject conversationMenu;
    bool cmIsOpen = false;
    
    GameObject talkWindow;
    bool twIsOpen = false;
    GameObject QuestAccept;
    bool qaIsOpen = false;

<<<<<<< HEAD
    QuestDetailView detailView;

=======
>>>>>>> Sample
    public DialogueWindow dia;
    private void Awake()
    {
        questWindow = GameObject.Find("QuestPanel");
        conversationMenu = GameObject.Find("NPCmenu");
        talkWindow = GameObject.Find("TalkWindow");
        QuestAccept = GameObject.Find("QuestAccept");
        qwIsOpen = false;
        cmIsOpen = false;
        twIsOpen = false;
        qaIsOpen = false;
    }
    private void Start()
    {
        conversationMenu.SetActive(false);
        talkWindow.SetActive(false);
        QuestAccept.SetActive(false);
    }

    public void showQW() // npc가 대화 내용과 퀘스트 내용을 가지고 있으면 파라미터를 쓸 필요 없이 사용가능
    {
        qwIsOpen = !qwIsOpen;
        questWindow.SetActive(qwIsOpen);
    }
    public void showConversation()
    {
        cmIsOpen = !cmIsOpen;
        conversationMenu.SetActive(cmIsOpen);
    }
    public void talkWithNPC()
    {
        talkWindow.SetActive(true);
    }
    public void AcceptQuest()
    {
        qaIsOpen = !qaIsOpen;
        QuestAccept.SetActive(qaIsOpen);
    }
}