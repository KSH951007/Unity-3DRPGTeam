using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Xml;

public class DungeonClearTime : MonoBehaviour
{
    [SerializeField]
    private float timeF;
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI clearTimeText;

    private float clearTime;
    private int min;
    private float sec;

    [HideInInspector]
    public string showClearTime;

    private void Start()
    {
        gameObject.SetActive(true);
    }
    private void FixedUpdate()
    {
        timeF += Time.deltaTime;
        timeText.text = string.Format("{0:N2}", timeF);
    }


    public void StopTimer()
    {
        clearTime = timeF;
        timeText.text = clearTime.ToString();
        min = (int)(clearTime / 60);
        sec = clearTime - (float)(60 * min);

        showClearTime = $"{min}Ка {string.Format("{0:N2}", timeF)}УЪ";
        clearTimeText.text = showClearTime;
        gameObject.SetActive(false);
    }
}
