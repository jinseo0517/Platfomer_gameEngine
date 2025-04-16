using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Image[] heartIcons;      // 하트 이미지 배열
    [SerializeField] private Sprite fullHeartSprite; // 채워진 하트 이미지
    [SerializeField] private Sprite emptyHeartSprite; // 빈 하트 이미지
    [SerializeField] private int playerLives = 3;    // 초기 생명 수

    void Start()
    {
        UpdateLifeUI();
    }

    public void TakeDamage()
    {
        playerLives--;

        if (playerLives <= 0)
        {
            playerLives = 0;
            Debug.Log("플레이어 사망!");
            // 게임 오버 처리 추가 가능
        }

        UpdateLifeUI();
    }

    public void PlusLife()
    {
        playerLives += 1;
        if(playerLives > 3) 
        { 
            playerLives = 3;
        }
        UpdateLifeUI();
    }

    private void UpdateLifeUI()
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            if (i < playerLives)
            {
                heartIcons[i].sprite = fullHeartSprite;
            }
            else
            {
                heartIcons[i].sprite = emptyHeartSprite;
            }
        }
    }
}