using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour
{

    private static int count = 0;
    private static float damageFontSize = 0.5f;
    [SerializeField] private Sprite[] damageFonts;
    private List<Image> images;
    private float moveSpeed;
    private float remainingTime;
    private Transform activeDamageFontTr;
    private float offsetX;
    private int damageCount;
    [SerializeField] private Transform origineTr;
    private RectTransform rectTr;
    private Canvas canvas;
    void Awake()
    {
        canvas = GetComponent<Canvas>();
        rectTr = GetComponent<RectTransform>();

        images = new List<Image>();
        activeDamageFontTr = this.transform.GetChild(0);
        Transform fonts = transform.Find("DamageImages").transform;
        for (int i = 0; i < damageFonts.Length; i++)
        {
            images.Add(fonts.GetChild(i).GetComponent<Image>());
            images[i].enabled = false;
        }
        remainingTime = 3f;
        moveSpeed = 1f;

        offsetX = images[0].rectTransform.rect.width / 2;
    }
    public static void SetSize(float newSize)
    {
        damageFontSize = newSize;
    }

    private void OnEnable()
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, 1f);
        }
        count++;
        canvas.sortingOrder = count;
        rectTr.sizeDelta = new Vector2(damageFontSize, damageFontSize);
    }
    private void OnDisable()
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].enabled = false;
        }
        while (activeDamageFontTr.childCount > 0)
        {
            activeDamageFontTr.GetChild(0).SetParent(origineTr);
        }
    }

    public void GetDamageFont(Vector3 position, int damage)
    {

        this.transform.position = position;

        damageCount = damage.ToString().Length;
        if (images.Count < damageCount)
        {
            int insufficientCount = damageCount - images.Count;

            for (int i = 0; i < insufficientCount; i++)
            {
                GameObject gameObject = new GameObject("Image");
                images.Add(gameObject.AddComponent<Image>());
            }
        }

        for (int i = 0; i < damageCount; i++)
        {
            images[i].transform.SetParent(activeDamageFontTr);
        }
        for (int i = 0; i < damageCount; i++)
        {
            images[damageCount - i - 1].enabled = true;

            int digits = (int)Mathf.Pow(10, i);
            int number = damage / digits % 10;
            images[damageCount - i - 1].sprite = damageFonts[number];
            float newPosX = 0 + offsetX * ((float)damageCount / 2) - offsetX / 2 - i * offsetX;
            images[damageCount - i - 1].transform.localPosition = new Vector3(newPosX, 0f, 0f);
        }

        StartCoroutine(DamageFontEffect());
    }
    IEnumerator DamageFontEffect()
    {
        float currentTime = 0f;

        while (currentTime < remainingTime)
        {
            this.transform.position += Vector3.up * moveSpeed * Time.deltaTime;

            for (int i = 0; i < damageCount; i++)
            {
                images[i].color = new Color(images[i].color.r, images[i].color.g, images[i].color.b, Mathf.Lerp(images[i].color.a, 0f, currentTime * Time.deltaTime));
            }


            currentTime += Time.deltaTime;

            yield return null;
        }

        count--;
        PoolManager.Instance.ReturnPool(this.gameObject);
    }
}
