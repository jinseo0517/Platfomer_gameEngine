using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    private int m_ColCount = 0; // �浹�� ������Ʈ ���� ����
    private float m_DisableTime; // ��Ȱ��ȭ �ð�

    public bool State()
    {
        return m_DisableTime <= 0 && m_ColCount > 0;
    }

    public void Disable(float duration)
    {
        m_DisableTime = duration;
        Debug.Log("GroundSensor ��Ȱ��ȭ��: " + duration + "�� ����");
    }

    void Update()
    {
        if (m_DisableTime > 0)
        {
            m_DisableTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            m_ColCount++;
            Debug.Log("GroundSensor �浹 ������: " + other.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            m_ColCount--;
            Debug.Log("GroundSensor �浹 ������: " + other.gameObject.name);
        }
    }
}