using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // 이동 속도
    [SerializeField] private float m_jumpForce = 7.5f; // 점프 힘
    private Animator m_animator; // 애니메이터 컴포넌트
    private Rigidbody2D rb; // Rigidbody2D 컴포넌트
    private GroundSensor m_groundSensor; // 땅 감지 센서
    private bool m_grounded = false; // 땅에 닿아 있는지 여부
    public float speedBoostDuration = 7f; //무적 지속 전체시간
    public GameObject m_AttackCollider; // 공격 콜라이더
    private int life = 3;

    public PlayerHUD playerHUD;

    [SerializeField] private bool isAttackable = true;
    private bool isGiant = false;
    // Inspector 창에서 설정 가능한 다음 스테이지 이름 (기본값: "Stage1")
    [SerializeField] private string nextStageName = "Stage2";

    void Start()
    {
        // 컴포넌트 초기화
        m_animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        m_AttackCollider.SetActive(false);
        Transform sensorTransform = transform.Find("GroundSensor");
        if (sensorTransform != null)
        {
            m_groundSensor = sensorTransform.GetComponent<GroundSensor>();
        }
        else
        {
            Debug.LogError("GroundSensor를 찾을 수 없습니다!");
        }
        rb.gravityScale = 2.0f; // 중력 설정
    }

    void Update()
    {
        // Ground 상태 업데이트
        UpdateGroundedStatus();

        // Y축 속도를 Animator에 전달
        m_animator.SetFloat("AirSpeedY", rb.velocity.y);

        // 좌우 이동 처리
        float inputX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
        HandleMovement(inputX);

        // 캐릭터 방향 전환
        UpdateCharacterDirection(inputX);

        // 점프 처리
        if (Input.GetKeyDown("space") && m_grounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Return) && isAttackable)
        {
            Attack();
        }
    }

    void Attack()
    {
        m_animator.SetTrigger("Attack1");
        m_AttackCollider.SetActive(true);
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
        rb.velocity = new Vector2(rb.velocity.x, m_jumpForce);

        // Ground 상태 false로 설정
        m_grounded = false;
        m_animator.SetBool("Grounded", m_grounded);

        // GroundSensor를 임시 비활성화 (착지 처리 문제 방지)
        if (m_groundSensor != null)
        {
            m_groundSensor.Disable(0.1f);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("충돌한 오브젝트 태그: " + other.tag); // 충돌한 오브젝트의 태그 출력

        // "NextStage" 태그를 가진 오브젝트와 충돌한 경우
        if (other.CompareTag("NextStage"))
        {
            Debug.Log(nextStageName + " 씬으로 이동"); // 디버깅 메시지
            SceneManager.LoadScene(other.name); // 다음 스테이지로 이동
        }
        //리스폰
        if (other.CompareTag("Respawn"))
        {
            if (isGiant)
            {
                Destroy(other.gameObject);
            }
            else
            {
                if (life > 1)
                {
                    //Debug.Log("1");
                    //playerHUD.TakeDamage();
                    life--;
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
                
        }
        //무적모드
        if (other.CompareTag("PawerUp"))
        {
            isGiant = true;
            Invoke("PowerUpBack", speedBoostDuration);
           
            Destroy(other.gameObject);
        }
        if (other.CompareTag("SpeedUp"))
        {
            StartCoroutine(SpeedBoost());
            Destroy(other.gameObject); // 아이템 삭제
        }
        if (other.CompareTag("heal"))
        {
            life += 1;
            if (life > 3)
            {
                life = 3;
            }
            playerHUD.PlusLife();
            Destroy(other.gameObject); // 아이템 삭제
        }

        //점프력증가 아이템
        if (other.CompareTag("JumpUp"))
        {
            m_jumpForce *= 2f; // 점프력 두 배 증가
            Invoke("ResetJumpForce", 5f); // 5초 후 원래 값으로 복구
            Destroy(other.gameObject); // 아이템 삭제
        }

    }

    void ResetJumpForce()
    {
        m_jumpForce /= 2f; // 원래 점프력으로 복구
    }

    private void PowerUpBack()
    {
        isGiant = false;
    }

    IEnumerator SpeedBoost()
    {
        float originalSpeed = moveSpeed;
        moveSpeed *= 3f; // 속도 증가 (원하는 값으로 변경 가능)
        yield return new WaitForSeconds(speedBoostDuration); // 지속 시간 대기
        moveSpeed = originalSpeed; // 원래 속도로 복구
    }
}