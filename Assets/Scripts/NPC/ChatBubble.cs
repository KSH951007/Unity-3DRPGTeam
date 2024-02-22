using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ChatBubble : MonoBehaviour
{
    public Transform interactBubble;

    public static void Create(Transform parent, Vector3 Direction,IconType Icon, string des)
    {
        if(Icon == IconType.FirstMet)
        {
             // TODO : 첫만남 구현
        }
        else
        {
            // TODO : 이후 만남
        }
    }
    
    public void showInteractKey(Transform parent, Vector3 Direction, IconType Icon, string des)
    {
        Transform InteractBubble = Instantiate(interactBubble, parent);
        InteractBubble.localPosition = Direction;
    }

    public enum IconType
    {
        FirstMet = 0,
        other = 1
    }
}