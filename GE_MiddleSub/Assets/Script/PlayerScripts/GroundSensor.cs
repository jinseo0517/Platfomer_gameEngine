using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    private int m_ColCount = 0; // 충돌된 오브젝트 수를 추적
    private float m_DisableTime; // 비활성화 시간

    public bool State()
    {
        return m_DisableTime <= 0 && m_ColCount > 0;
    }

    public void Disable(float duration)
    {
        m_DisableTime = duration;
        Debug.Log("GroundSensor 비활성화됨: " + duration + "초 동안");
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
            Debug.Log("GroundSensor 충돌 감지됨: " + other.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            m_ColCount--;
            Debug.Log("GroundSensor 충돌 해제됨: " + other.gameObject.name);
        }
    }
}