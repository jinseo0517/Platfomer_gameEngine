using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    // �÷��̾��� ������ ��Ÿ���� ��Ʈ �̹��� �迭
    [SerializeField] private Image[] heartIcons;
    // ä���� ��Ʈ�� ��Ÿ���� ��������Ʈ
    [SerializeField] private Sprite fullHeartSprite;
    // �� ��Ʈ�� ��Ÿ���� ��������Ʈ
    [SerializeField] private Sprite emptyHeartSprite;
    // �÷��̾��� �ʱ� ���� ����
    [SerializeField] private int playerLives = 3;

    // ���� ���� �� ȣ��Ǵ� �Լ�
    void Start()
    {
        // UI�� �ʱ� ���� ���·� ������Ʈ
        UpdateLifeUI();
    }

    // �÷��̾ �������� ���� �� ȣ��Ǵ� �Լ�
    public void TakeDamage()
    {
        playerLives--; // �÷��̾� ������ 1 ����

        if (playerLives <= 0) // ������ 0 ���ϰ� �Ǹ�
        {
            playerLives = 0; // ���� ���� ������ ���� �ʵ��� ����
            Debug.Log("�÷��̾� ���!"); // ����� �α� ���
            // ���⼭ ���� ���� ó���� �߰��� �� ����
        }

        UpdateLifeUI(); // UI�� ������Ʈ
    }

    // �÷��̾��� ������ 1 ������Ű�� �Լ�
    public void PlusLife()
    {
        playerLives += 1; // �÷��̾� ������ 1 ����
        if (playerLives > 3) // �ִ� ���� ������ 3���� ����
        {
            playerLives = 3; // ������ 3�� �ʰ����� �ʵ��� ����
        }
        UpdateLifeUI(); // UI�� ������Ʈ
    }

    // ��Ʈ UI�� ���� �÷��̾� ���� ���¿� ���� ������Ʈ�ϴ� �Լ�
    private void UpdateLifeUI()
    {
        for (int i = 0; i < heartIcons.Length; i++) // ��Ʈ �̹��� �迭�� ��ȸ
        {
            if (i < playerLives) // ���� ���� ������ ���� �ε����� ä���� ��Ʈ�� ǥ��
            {
                heartIcons[i].sprite = fullHeartSprite;
            }
            else // �׷��� ������ �� ��Ʈ�� ǥ��
            {
                heartIcons[i].sprite = emptyHeartSprite;
            }
        }
    }
}