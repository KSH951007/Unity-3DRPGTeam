using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MasageBoxUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    public event Action onAccept;


    public void SetMesageBox(string title, string description)
    {
        titleText.text = title;
        descriptionText.text = description;
    }


    public void PressAcceptButton()
    {
        onAccept?.Invoke();
        Debug.Log("asdsad");
        onAccept = null;
        PoolManager.Instance.ReturnPool(gameObject);
    }

    public void PressCancleButton()
    {
        onAccept = null;

        PoolManager.Instance.ReturnPool(gameObject);
    }
}
