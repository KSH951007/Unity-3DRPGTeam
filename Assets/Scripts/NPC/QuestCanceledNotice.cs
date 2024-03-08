using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestCanceledNotice : MonoBehaviour
{
    [SerializeField]
    private string completeQuest;
    [SerializeField]
    private string RunningQuest;
    [SerializeField]
    private TextMeshProUGUI differentText;
    private bool forDifferecnce;


    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Notify(bool b)
    {
        gameObject.SetActive(true);
        forDifferecnce = b;
        StartCoroutine("Notice");
    }

    private IEnumerator Notice()
    {
        if (forDifferecnce == true)
            differentText.text = completeQuest;
        else
            differentText.text = RunningQuest;

        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
