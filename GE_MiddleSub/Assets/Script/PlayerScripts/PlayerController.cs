using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_speed = 4.0f; // 이동 속도
    [SerializeField] private float m_jumpForce = 7.5f; // 점프 힘
    private Animator m_animator; // 애니메이터 컴포넌트
    private Rigidbody2D m_body2d; // Rigidbody2D 컴포넌트
    private GroundSensor m_groundSensor; // 땅 감지 센서
    private bool m_grounded = false; // 땅에 닿아 있는지 여부

    void Start()
    {
        // 컴포넌트 초기화
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        Transform sensorTransform = transform.Find("GroundSensor");
        if (sensorTransform != null)
        {
            m_groundSensor = sensorTransform.GetComponent<GroundSensor>();
        }
        else
        {
            Debug.LogError("GroundSensor를 찾을 수 없습니다!");
        }
        m_body2d.gravityScale = 2.0f; // 중력 설정
    }

    void Update()
    {
        // Ground 상태 업데이트
        UpdateGroundedStatus();

        // Y축 속도를 Animator에 전달
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // 좌우 이동 처리
        float inputX = Input.GetAxis("Horizontal");
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
        HandleMovement(inputX);

        // 캐릭터 방향 전환
        UpdateCharacterDirection(inputX);

        // 점프 처리
        if (Input.GetKeyDown("space") && m_grounded)
        {
            Jump();
        }
    }

    void UpdateGroundedStatus()
    {
        if (!m_grounded && m_groundSensor != null && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
            Debug.Log("캐릭터 착지: Grounded = true");
        }
        else if (m_grounded && (m_groundSensor == null || !m_groundSensor.State()))
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            Debug.Log("캐릭터 공중 상태: Grounded = false");
        }
    }

    void HandleMovement(float inputX)
    {
        if (Mathf.Abs(inputX) > Mathf.Epsilon) // 이동 중
        {
            m_animator.SetInteger("AnimState", 1); // Run 상태
        }
        else
        {
            m_animator.SetInteger("AnimState", 0); // Idle 상태
        }
    }

    void UpdateCharacterDirection(float inputX)
    {
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false; // 오른쪽 방향
        }
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true; // 왼쪽 방향
        }
    }

    void Jump()
    {
        Debug.Log("Jump 메서드 실행");

        // 점프 애니메이션 실행
        m_animator.SetTrigger("Jump");

        // Rigidbody를 사용하여 점프 동작 실행
        m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);

        // Ground 상태 false로 설정
        m_grounded = false;
        m_animator.SetBool("Grounded", m_grounded);

        // GroundSensor를 임시 비활성화 (착지 처리 문제 방지)
        if (m_groundSensor != null)
        {
            m_groundSensor.Disable(0.1f);
        }
    }
}