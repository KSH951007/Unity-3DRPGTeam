using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NPCUI : MonoBehaviour // npc가 가지고 있는 UI
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
            // TODO : 첫만남 구현
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