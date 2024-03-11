using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    private HeroManager heroManager;
    public enum ButtonType { Info, Exit, Setting }
    [SerializeField] private Button[] buttons;

    private void Start()
    {
        Transform hero = GameObject.Find("Player").transform.Find("Heros");
        heroManager = hero.GetComponent<HeroManager>();

        heroManager.onChangeCharacter += ChangeHero;

        SettingButton();
    }


    public void ChangeHero()
    {

    }
    public void SettingButton()
    {
        if (SceneLoader.Instance.GetSceneType() == SceneLoader.SceneType.Dungeon)
        {
            buttons[(int)ButtonType.Info].gameObject.SetActive(false);
        }
        else if (SceneLoader.Instance.GetSceneType() == SceneLoader.SceneType.Village)
        {
            buttons[(int)ButtonType.Exit].gameObject.SetActive(false);
        }
    }
    public void PressExitButton()
    {
        Debug.Log("s");
        GameObject masagebox = PoolManager.Instance.Get("MasageBoxUI");
        if (masagebox != null)
        {
            MasageBoxUI masageBoxUI = masagebox.GetComponent<MasageBoxUI>();

            masageBoxUI.SetMesageBox("중단/포기", "정말 포기 하시겠습니까?");
            masageBoxUI.onAccept += () => { StartCoroutine(SceneLoader.Instance.LoadScene(1)); };
        }
    }
}
