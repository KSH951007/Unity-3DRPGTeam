using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class ParticleControlPlayableAsset : PlayableAsset
{   
    // 사용자가 편집할 수 있는 파티클 시스템 변수
    public ParticleSystem particleSystem;

    // Playable Asset을 생성하고 반환합니다.
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        // Playable Behaviour를 생성하여 커스텀 동작을 정의합니다.
        var playable = ScriptPlayable<ParticleSystemControlPlayableBehaviour>.Create(graph);

        // Playable Behaviour에 파티클 시스템을 연결합니다.
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
        // 여기에 원하는 시간에 따라 파티클 시스템을 제어하는 로직을 작성합니다.
        // 예를 들어, 시간에 따라 파티클을 활성화하거나 비활성화할 수 있습니다.
        if (time > 2.0f && time < 4.0f)
            particleSystem.Play();
        else
            particleSystem.Stop();
    }
}