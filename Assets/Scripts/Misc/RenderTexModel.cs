using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTexModel : MonoBehaviour
{
    [SerializeField] private PreviewModel[] models;
    public enum PreviewModelType { ShopNPC, InvenNPC, HeroDin, HeroJulia, HeroAaren }

    public void SetAnimation(PreviewModelType modelType, string animationName)
    {
        Animator animator = models[(int)modelType].GetComponent<Animator>();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {

            return;
        }

        animator.SetTrigger(animationName);

    }


    public void ActvieModel(PreviewModelType modelType)
    {
        for (int i = 0; i < models.Length; i++)
        {
            models[i].gameObject.SetActive(false);
        }

        models[(int)modelType].gameObject.SetActive(true);

    }
}
