using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryToggleController : MonoBehaviour
{
    [SerializeField] private Sprite disableSprite;
    [SerializeField] private Sprite enableSprite;
    private ToggleGroup toggleGroup;
    private CategoryToggle[] toggles;
    private void Awake()
    {
        toggleGroup = GetComponent<ToggleGroup>();
        toggles = new CategoryToggle[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            toggles[i] = transform.GetChild(i).GetComponent<CategoryToggle>();
        }
    }
    private void Start()
    {

    }

    public void ActiveToggle(bool active)
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].IsOn())
            {
                toggles[i].ChangeBackgroundImage(enableSprite);
            }
            else
            {
                toggles[i].ChangeBackgroundImage(disableSprite);
            }
           

        }
    }
}
