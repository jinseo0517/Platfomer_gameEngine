using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    float score;

    [SerializeField] private float moveSpeed = 5f; // 이동 속도
    [SerializeField] private float m_jumpForce = 7.5f; // 점프 힘

    private Animator m_animator; // 애니메이션 제어 컴포넌트
    private Rigidbody2D rb; // 물리 엔진 제어 컴포넌트
    private GroundSensor m_groundSensor; // 땅 감지 센서

    public bool m_grounded = false; // 땅에 닿아있는지 여부
    public float speedBoostDuration = 7f; // 스피드 부스트 지속 시간
    public GameObject m_AttackCollider; // 공격 콜라이더
    public int life = 3; // 플레이어 생명 수

    public PlayerHUD playerHUD; // HUD 관리
    [SerializeField] private bool isAttackable = true; // 공격 가능 여부
    private bool isGiant = false; // 거대화 상태
    private bool isAttacking = false; // 공격 상태
    [SerializeField] private string nextStageName = "Stage2"; // 다음 스테이지 이름

    private void Awake()
    {
        score = 1000f;
    }
    void Start()
    {
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

        rb.gravityScale = 2.0f;
    }

    void Update()
    {
        UpdateGroundedStatus();
        m_animator.SetFloat("AirSpeedY", rb.velocity.y);

        float inputX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
        HandleMovement(inputX);
        UpdateCharacterDirection(inputX);

        if (Input.GetKeyDown("space") && m_grounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Return) && isAttackable)
        {
            Attack();
        }

        score -= Time.deltaTime;
    }

    void Attack()
    {
        isAttacking = true; // 공격 상태 활성화
        m_animator.SetTrigger("Attack1");
        m_AttackCollider.SetActive(true);

        Invoke("EndAttack", 0.5f); // 공격 상태를 0.5초 후 비활성화
    }

    void EndAttack()
    {
        isAttacking = false; // 공격 상태 비활성화
        m_AttackCollider.SetActive(false);
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
        if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            m_animator.SetInteger("AnimState", 1);
        }
        else
        {
            m_animator.SetInteger("AnimState", 0);
        }
    }

    void UpdateCharacterDirection(float inputX)
    {
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void Jump()
    {
        Debug.Log("Jump 메서드 실행");
        m_animator.SetTrigger("Jump");
        rb.velocity = new Vector2(rb.velocity.x, m_jumpForce);
        m_grounded = false;
        m_animator.SetBool("Grounded", m_grounded);

        if (m_groundSensor != null)
        {
            m_groundSensor.Disable(0.1f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("충돌한 오브젝트 태그: " + other.tag);

        if (other.CompareTag("NextStage"))
        {
            //HighScore.TrySet(SceneManager.GetActiveScene().buildIndex, (int)score);
            SceneManager.LoadScene(nextStageName);
            StageResulSaver.SaveStage (SceneManager.GetActiveScene().buildIndex, (int)score);
        }

        if (other.CompareTag("Respawn"))
        {
            Debug.Log("Respawn 태그 충돌 감지!");

            if (!isAttacking) // 공격 중이 아닌 경우에만 생명 감소 처리
            {
                if (isGiant)
                {
                    if (other.gameObject != null) Destroy(other.gameObject);
                }
                else
                {
                    if (life > 1)
                    {
                        life--;
                        if (playerHUD != null)
                        {
                            playerHUD.TakeDamage();
                        }
                        else
                        {
                            Debug.LogError("playerHUD가 null입니다. UI 갱신 실패!");
                        }
                    }
                    else
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                }
            }
            else
            {
                Debug.Log("플레이어가 공격 중이어서 생명 감소가 발생하지 않습니다.");
            }
        }

        if (other.CompareTag("PawerUp"))
        {
            isGiant = true;
            Invoke("PowerUpBack", speedBoostDuration);
            if (other.gameObject != null) Destroy(other.gameObject);
        }

        if (other.CompareTag("SpeedUp"))
        {
            StartCoroutine(SpeedBoost());
            if (other.gameObject != null) Destroy(other.gameObject);
        }

        if (other.CompareTag("heal"))
        {
            life += 1;
            if (life > 3) life = 3;

            if (playerHUD != null)
            {
                playerHUD.PlusLife();
            }
            else
            {
                Debug.LogError("playerHUD가 null입니다. UI 갱신 실패!");
            }

            if (other.gameObject != null) Destroy(other.gameObject);
        }

        if (other.CompareTag("JumpUp"))
        {
            m_jumpForce *= 2f;
            Invoke("ResetJumpForce", 5f);
            if (other.gameObject != null) Destroy(other.gameObject);
        }
    }

    void ResetJumpForce()
    {
        m_jumpForce /= 2f;
    }

    private void PowerUpBack()
    {
        isGiant = false;
    }

    IEnumerator SpeedBoost()
    {
        float originalSpeed = moveSpeed;
        moveSpeed *= 3f;
        yield return new WaitForSeconds(speedBoostDuration);
        moveSpeed = originalSpeed;
    }
}