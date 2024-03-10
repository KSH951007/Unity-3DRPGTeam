using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private Image loadingBackgroundImage;
    [SerializeField] private Image loadginImage;
    [SerializeField] private TextMeshProUGUI loadingText;

    [SerializeField] private Sprite[] backgrounSprites;

    private void Start()
    {

        ActiveLoadingImage(false);
    }
    public void StartLoadingUI()
    {
        int rand = Random.Range(0, backgrounSprites.Length);
        loadingBackgroundImage.sprite = backgrounSprites[rand];
        ActiveLoadingImage(true);
        LoadingProgress(0f);
    }
    public void LoadingProgress(float progress)
    {
        loadginImage.fillAmount = progress;
        loadingText.text = $"Loading...({(progress * 100).ToString("F2")}%)";
    }
    public void ActiveLoadingImage(bool active)
    {
        this.transform.GetChild(0).gameObject.SetActive(active);
    }
}
