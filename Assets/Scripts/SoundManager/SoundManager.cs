using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    // ���� �ִ� ��� ����� �ҽ��� ���� ��ųʸ�
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

        SceneManager.sceneLoaded += OnSceneLoaded; // ���� �ε�� �� OnSceneLoaded�Լ� ȣ��
    }

    // Ư�� ����� �ҽ��� ��ųʸ��� �߰�
    private void AddSound(string soundName, AudioSource audioSource)
    {
        if (!audioSources.ContainsKey(soundName))
        {
            audioSource.volume = 0.2f;
            audioSources.Add(soundName, audioSource);
        }
        else
        {
            Debug.LogWarning($"{soundName} �� ���� �̸��� ����� �ҽ��� �̹� �����մϴ�.");
        }
    }

    // �ش� ���� �ʿ��� ����� �ҽ��� ������ �ִ� �ڽĵ���
    // ���� �θ� ��ü���� AddAllSounds(�ش� ��ü)�� �ϸ� ��� ����� �ҽ��� ��ųʸ��� �߰�
    public void AddAllSounds(GameObject soundSources)
    {
        AudioSource[] sources = soundSources.GetComponentsInChildren<AudioSource>(true);
        foreach (AudioSource source in sources)
        {
            AddSound(source.gameObject.name, source);
        }
    }

    /// <summary>
    /// �뷡 ���
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
            Debug.LogWarning($"{soundName}�� ���� �̸��� ����� �ҽ��� �������� �ʽ��ϴ�.");
        }
    }

    /// <summary>
    /// �뷡 ��� ����
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
    /// ��ųʸ����� Ư�� ������ҽ��� �����ϴ� �Լ�
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

    // SoundManager�� ��ųʸ��� �ִ� ��� audioSource�� �����ϴ� �Լ��� ȣ��
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ClearAllSounds();
    }

    // Destory�� ��, ���� �Űܵ� OnSceneLoaded�� ȣ������ �ʵ��� ��
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
