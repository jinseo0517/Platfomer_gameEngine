using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton 인스턴스
    public AudioClip bgmClip; // 배경음악 클립
    private AudioSource audioSource; // 오디오 소스

    void Awake()
    {
        // Singleton 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 전환되어도 오브젝트 유지
        }
        else
        {
            Destroy(gameObject); // 기존 인스턴스가 있으면 새로 생성하지 않음
        }
    }

    void Start()
    {
        // AudioSource 초기화 및 BGM 재생
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = bgmClip;
        audioSource.loop = true; // BGM 반복
        audioSource.playOnAwake = false; // Awake 시 재생 비활성화
        audioSource.Play(); // 배경음악 재생
    }

    // 볼륨 조정 함수
    public void SetVolume(float volume)
    {
        audioSource.volume = Mathf.Clamp(volume, 0f, 1f); // 볼륨 범위 제한
    }

    // 배경음악을 멈추는 함수
    public void StopBGM()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    // 배경음악을 다시 재생하는 함수
    public void PlayBGM()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
