using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryToggle : MonoBehaviour
{
   
    private Image backgroundImage;
    private Toggle toggle;
    private void Awake()
    {
        backgroundImage = this.transform.GetChild(0).GetComponent<Image>();
        toggle = GetComponent<Toggle>();
    }
    public void ChangeBackgroundImage(Sprite sprite)
    {
        backgroundImage.sprite = sprite;
    }
    public bool IsOn()
    {
        return toggle.isOn;
    }
}
