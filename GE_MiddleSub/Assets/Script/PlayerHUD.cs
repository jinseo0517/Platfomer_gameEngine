using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Image[] heartIcons;      // ��Ʈ �̹��� �迭
    [SerializeField] private Sprite fullHeartSprite; // ä���� ��Ʈ �̹���
    [SerializeField] private Sprite emptyHeartSprite; // �� ��Ʈ �̹���
    [SerializeField] private int playerLives = 3;    // �ʱ� ���� ��

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
            Debug.Log("�÷��̾� ���!");
            // ���� ���� ó�� �߰� ����
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