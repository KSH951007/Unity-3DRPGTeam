using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryController : MonoBehaviour
{
    [SerializeField] protected Sprite disableSprite;
    [SerializeField] protected Sprite enableSprite;
    private ToggleGroup toggleGroup;
    private CategoryToggle[] toggles;
    private void Awake()
    {
        toggleGroup = GetComponent<ToggleGroup>();
        toggles = new CategoryToggle[transform.childCount];

    }
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            toggles[i] = transform.GetChild(i).GetComponent<CategoryToggle>();
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