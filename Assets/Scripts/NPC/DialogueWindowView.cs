using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting;

public class DialogueWindowView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI elementTextPrefab;
    private List<GameObject> textGroup;

    public void showNextText(TextMeshProUGUI text)
    {
        var element = Instantiate(elementTextPrefab, transform);
        textGroup.Add(element.gameObject);
        text = element.GetComponent<TextMeshProUGUI>();

        if (textGroup.Count == 2)
        {
            RemoveText();
        }
    }

    public void RemoveText() // TODO : 오브젝트 풀링
    {
        textGroup.RemoveAt(0);
        Destroy(textGroup[0].gameObject);
    }
}
