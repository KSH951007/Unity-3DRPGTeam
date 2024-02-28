using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using static ChangeEffect;

public class ChangeEffect : MonoBehaviour
{

    public enum ChangeType { Appearance, Leave }
    [SerializeField] private VisualEffect meshEffect;
    [SerializeField] private Material cutEdgeMat;
    [SerializeField] private VisualEffect spawnEffect;
    private Material originMat;
    private Material cutEdgeMatInstance;
    //private SkinnedMeshRenderer meshRenderer;
    private float appearanceValue;
    private float leaveValue;

    private void Awake()
    {
        appearanceValue = 1f;
        leaveValue = 0f;
        cutEdgeMatInstance = Instantiate(cutEdgeMat);
    }
    public void ChangeMaterial(ChangeType changeType, SkinnedMeshRenderer meshRenderer, Vector3 position, float changeTime)
    {

        this.originMat = meshRenderer.material;


        Texture albedoTex = originMat.GetTexture("_BaseMap");
        Texture normalTex = originMat.GetTexture("_BumpMap");
        Texture specularTex = originMat.GetTexture("_SpecGlossMap");



        if (albedoTex != null)
        {
            cutEdgeMat.SetTexture("_Albedo", albedoTex);
            Debug.Log("cleart");
        }
        if (normalTex != null)
            cutEdgeMat.SetTexture("Normal", normalTex);
        if (specularTex != null)
            cutEdgeMat.SetTexture("Specular", specularTex);

        meshEffect.SetSkinnedMeshRenderer("SkinnedMeshRenderer", meshRenderer);
        meshRenderer.material = cutEdgeMat;
        transform.position = position;

        float startValue;
        float targetValue;
        if (changeType == ChangeType.Appearance)
        {
            startValue = appearanceValue;
            targetValue = leaveValue;
        }
        else
        {
            startValue = leaveValue;
            targetValue = appearanceValue;
        }

        StartCoroutine(ChangeRoutine(startValue, targetValue, changeTime));

    }
    private IEnumerator ChangeRoutine(float startValue, float targetValue, float changeTime)
    {
        cutEdgeMat.SetFloat("CutEdge", startValue);
        meshEffect.SetFloat("Particle Edge", startValue);

        float currentValue = startValue;
        float velocity = 0f;
        spawnEffect.Play();
        while (!Mathf.Approximately(currentValue, targetValue))
        {
            currentValue = Mathf.SmoothDamp(currentValue, targetValue, ref velocity, changeTime/4);
            cutEdgeMat.SetFloat("_CutEdge", currentValue);
            meshEffect.SetFloat("Particle Edge", currentValue);

            yield return null;

        }


      
    }
}
