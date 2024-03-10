using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Xml;
using UnityEngine.Rendering;

public class DungeonClearTime : MonoBehaviour
{
    [SerializeField]
    private float timeF;
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI clearTimeText;
    [SerializeField]
    private ShowClearDetail detailView;

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
        showClearTime = string.Format("{0:N2}", timeF);
        timeText.text = showClearTime;
    }

    public void StopTimer()
    {
        clearTime = timeF;
        timeText.text = clearTime.ToString();
        min = (int)(clearTime / 60);
        sec = clearTime - (float)(60 * min);

        showClearTime = $"{min}�� {string.Format("{0:N2}", timeF)}��";
        //clearTimeText.text = showClearTime;
        detailView.gameObject.SetActive(true);
        detailView.getClearTime(showClearTime);
        gameObject.SetActive(false);
    }
}
