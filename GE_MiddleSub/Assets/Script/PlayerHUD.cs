using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    // 플레이어의 생명을 나타내는 하트 이미지 배열
    [SerializeField] private Image[] heartIcons;
    // 채워진 하트를 나타내는 스프라이트
    [SerializeField] private Sprite fullHeartSprite;
    // 빈 하트를 나타내는 스프라이트
    [SerializeField] private Sprite emptyHeartSprite;
    // 플레이어의 초기 생명 개수
    [SerializeField] private int playerLives = 3;

    // 게임 시작 시 호출되는 함수
    void Start()
    {
        // UI를 초기 생명 상태로 업데이트
        UpdateLifeUI();
    }

    // 플레이어가 데미지를 받을 때 호출되는 함수
    public void TakeDamage()
    {
        playerLives--; // 플레이어 생명을 1 감소

        if (playerLives <= 0) // 생명이 0 이하가 되면
        {
            playerLives = 0; // 생명 값이 음수가 되지 않도록 설정
            Debug.Log("플레이어 사망!"); // 디버그 로그 출력
            // 여기서 게임 오버 처리를 추가할 수 있음
        }

        UpdateLifeUI(); // UI를 업데이트
    }

    // 플레이어의 생명을 1 증가시키는 함수
    public void PlusLife()
    {
        playerLives += 1; // 플레이어 생명을 1 증가
        if (playerLives > 3) // 최대 생명 개수는 3으로 제한
        {
            playerLives = 3; // 생명이 3을 초과하지 않도록 설정
        }
        UpdateLifeUI(); // UI를 업데이트
    }

    // 하트 UI를 현재 플레이어 생명 상태에 따라 업데이트하는 함수
    private void UpdateLifeUI()
    {
        for (int i = 0; i < heartIcons.Length; i++) // 하트 이미지 배열을 순회
        {
            if (i < playerLives) // 현재 생명 값보다 작은 인덱스는 채워진 하트를 표시
            {
                heartIcons[i].sprite = fullHeartSprite;
            }
            else // 그렇지 않으면 빈 하트를 표시
            {
                heartIcons[i].sprite = emptyHeartSprite;
            }
        }
    }
}