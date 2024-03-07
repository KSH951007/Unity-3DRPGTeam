using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class ParticleControlPlayableAsset : PlayableAsset
{   
    // ����ڰ� ������ �� �ִ� ��ƼŬ �ý��� ����
    public ParticleSystem particleSystem;

    // Playable Asset�� �����ϰ� ��ȯ�մϴ�.
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        // Playable Behaviour�� �����Ͽ� Ŀ���� ������ �����մϴ�.
        var playable = ScriptPlayable<ParticleSystemControlPlayableBehaviour>.Create(graph);

        // Playable Behaviour�� ��ƼŬ �ý����� �����մϴ�.
        var behaviour = playable.GetBehaviour();
        behaviour.particleSystem = particleSystem;

        return playable;
    }
}
public class ParticleSystemControlPlayableBehaviour : PlayableBehaviour
{
    public ParticleSystem particleSystem;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (particleSystem == null)
            return;

        float time = (float)playable.GetTime();
        // ���⿡ ���ϴ� �ð��� ���� ��ƼŬ �ý����� �����ϴ� ������ �ۼ��մϴ�.
        // ���� ���, �ð��� ���� ��ƼŬ�� Ȱ��ȭ�ϰų� ��Ȱ��ȭ�� �� �ֽ��ϴ�.
        if (time > 2.0f && time < 4.0f)
            particleSystem.Play();
        else
            particleSystem.Stop();
    }
}