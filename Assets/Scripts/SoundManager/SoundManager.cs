using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    // 씬에 있는 모든 오디오 소스를 가질 딕셔너리
    private Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded; // 씬이 로드될 때 OnSceneLoaded함수 호출
    }

    // 특정 오디오 소스를 딕셔너리에 추가
    private void AddSound(string soundName, AudioSource audioSource)
    {
        if (!audioSources.ContainsKey(soundName))
        {
            audioSource.volume = 0.2f;
            audioSources.Add(soundName, audioSource);
        }
        else
        {
            Debug.LogWarning($"{soundName} 와 같은 이름의 오디오 소스가 이미 존재합니다.");
        }
    }

    // 해당 씬에 필요한 오디오 소스를 가지고 있는 자식들을
    // 가진 부모 객체에서 AddAllSounds(해당 객체)를 하면 모든 오디오 소스를 딕셔너리에 추가
    public void AddAllSounds(GameObject soundSources)
    {
        AudioSource[] sources = soundSources.GetComponentsInChildren<AudioSource>(true);
        foreach (AudioSource source in sources)
        {
            AddSound(source.gameObject.name, source);
        }
    }

    /// <summary>
    /// 노래 재생
    /// </summary>
    /// <param name="soundName"></param>
    public void PlaySound(string soundName)
    {
        if (audioSources.ContainsKey(soundName))
        {
            audioSources[soundName].Play();
        }
        else
        {
            Debug.LogWarning($"{soundName}와 같은 이름의 오디오 소스가 존재하지 않습니다.");
        }
    }

    /// <summary>
    /// 노래 재생 멈춤
    /// </summary>
    /// <param name="soundName"></param>
    public void StopSound(string soundName)
    {
        if (audioSources.ContainsKey(soundName))
        {
            audioSources[soundName].Stop();
        }
    }

    /// <summary>
    /// 딕셔너리에서 특정 오디오소스를 제거하는 함수
    /// </summary>
    /// <param name="soundName"></param>
    public void RemoveSound(string soundName)
    {
        if (audioSources.ContainsKey(soundName))
        {
            audioSources.Remove(soundName);
        }
    }

    public void ClearAllSounds()
    {
        audioSources.Clear();
    }

    // SoundManager의 딕셔너리에 있는 모든 audioSource를 제거하는 함수를 호출
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ClearAllSounds();
    }

    // Destory될 때, 씬을 옮겨도 OnSceneLoaded를 호출하지 않도록 함
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
