using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RenderModelViewUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI talkText;
    [SerializeField] private GameObject talkBox;
    private Coroutine talkCorutine;
    private float talkTime;
    [SerializeField] private RenderTexModel model;
    private RenderTexModel.PreviewModelType previewModelType;
    private void Awake()
    {
        talkTime = 2f;
    }  

    public void ActiveModel(RenderTexModel.PreviewModelType modelType)
    {
        previewModelType = modelType;
        model.ActvieModel(modelType);
    }
    public void SetTalkText(string text, string animationName = null)
    {
        if (talkCorutine != null)
            StopCoroutine(talkCorutine);
        if (animationName != null)
        {
            model.SetAnimation(previewModelType, animationName);
        }
        if (talkTime == 0f)
            talkTime = 2f;

        talkBox.SetActive(true);
        talkText.text = text;
        talkCorutine = StartCoroutine(StartTalk());

    }

    public IEnumerator StartTalk()
    {
        yield return new WaitForSeconds(talkTime);
        talkBox.SetActive(false);
    }
}
