using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton �ν��Ͻ�
    public AudioClip bgmClip; // ������� Ŭ��
    private AudioSource audioSource; // ����� �ҽ�

    void Awake()
    {
        // Singleton ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� ��ȯ�Ǿ ������Ʈ ����
        }
        else
        {
            Destroy(gameObject); // ���� �ν��Ͻ��� ������ ���� �������� ����
        }
    }

    void Start()
    {
        // AudioSource �ʱ�ȭ �� BGM ���
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = bgmClip;
        audioSource.loop = true; // BGM �ݺ�
        audioSource.playOnAwake = false; // Awake �� ��� ��Ȱ��ȭ
        audioSource.Play(); // ������� ���
    }

    // ���� ���� �Լ�
    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp(volume, 0f, 1f); // ���� ���� ����
    }

    // ��������� ���ߴ� �Լ�
    public void StopBGM()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    // ��������� �ٽ� ����ϴ� �Լ�
    public void PlayBGM()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
