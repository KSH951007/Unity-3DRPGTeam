using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTimerUI : MonoBehaviour
{
    private Image timerImage;
    private TextMeshProUGUI timerText;
    private float rotateSpeed;
    private void Awake()
    {
        timerImage = transform.GetComponentInChildren<Image>();
        timerText = transform.GetComponentInChildren<TextMeshProUGUI>();
        rotateSpeed = 90f;
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void SetTimer(float time)
    {
        if (time > 0)
        {
            gameObject.SetActive(true);
            StartCoroutine(StartTimer(time));
        }
    }
    IEnumerator StartTimer(float time)
    {
        while (time > 0)
        {

            timerText.text = time.ToString("F1");
            timerImage.transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);

            time -= Time.deltaTime;
            yield return null;
        }
        time = 0f;
        timerImage.transform.rotation = Quaternion.identity;
        timerText.text = time.ToString("F0");
        gameObject.SetActive(false);
    }
}
