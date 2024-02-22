using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUi : MonoBehaviour
{
    GameObject questWindow;
    GameObject questDetail;
    GameObject conversationMenu;


    private void Awake()
    {
        questWindow = GameObject.Find("QuestPanel");
        questDetail = GameObject.Find("QuestDetail");
        conversationMenu = GameObject.Find("NPCmenu");
    }
    private void Start()
    {
        questWindow.SetActive(false);
        questDetail.SetActive(false);
        conversationMenu.SetActive(false);
    }

    public void showQW(bool isOpen)
    {
        questWindow.SetActive(isOpen);
    }
    public void showQD()
    {
        questDetail.SetActive(false);
    }
    public void showConversation()
    {
        conversationMenu.SetActive(false);
    }
}
