using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NPCUI : MonoBehaviour // npc�� ������ �ִ� UI
{
    public GameObject interactCanvas;
    public GameObject pressSpace;


    private void Awake()
    {
        interactCanvas = GameObject.Find("InteractCanvas");
        pressSpace = GameObject.Find("PressSpace");
    }
    public static void Create(Transform parent, Vector3 Direction,IconType Icon, string des) // 
    {
        if(Icon == IconType.FirstMet)
        {
            //Transform InteractBubble = Instantiate(PressSpace, parent);
            //InteractBubble.localPosition = Direction;
            //
            // TODO : ù���� ����
        }
        else
        {
            //GameManager.Instance.ui.QUI

        }
    }

    public enum IconType
    {
        FirstMet = 0,
        other = 1
    }
}