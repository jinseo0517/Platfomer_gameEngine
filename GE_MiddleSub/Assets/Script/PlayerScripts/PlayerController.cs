using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    float score;

    [SerializeField] private float moveSpeed = 5f; // �̵� �ӵ�
    [SerializeField] private float m_jumpForce = 7.5f; // ���� ��

    private Animator m_animator; // �ִϸ��̼� ���� ������Ʈ
    private Rigidbody2D rb; // ���� ���� ���� ������Ʈ
    private GroundSensor m_groundSensor; // �� ���� ����

    public bool m_grounded = false; // ���� ����ִ��� ����
    public float speedBoostDuration = 7f; // ���ǵ� �ν�Ʈ ���� �ð�
    public GameObject m_AttackCollider; // ���� �ݶ��̴�
    public int life = 3; // �÷��̾� ���� ��

    public PlayerHUD playerHUD; // HUD ����
    [SerializeField] private bool isAttackable = true; // ���� ���� ����
    private bool isGiant = false; // �Ŵ�ȭ ����
    private bool isAttacking = false; // ���� ����
    [SerializeField] private string nextStageName = "Stage2"; // ���� �������� �̸�

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
            Debug.LogError("GroundSensor�� ã�� �� �����ϴ�!");
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
        isAttacking = true; // ���� ���� Ȱ��ȭ
        m_animator.SetTrigger("Attack1");
        m_AttackCollider.SetActive(true);

        Invoke("EndAttack", 0.5f); // ���� ���¸� 0.5�� �� ��Ȱ��ȭ
    }

    void EndAttack()
    {
        isAttacking = false; // ���� ���� ��Ȱ��ȭ
        m_AttackCollider.SetActive(false);
    }

    void UpdateGroundedStatus()
    {
        if (!m_grounded && m_groundSensor != null && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
            Debug.Log("ĳ���� ����: Grounded = true");
        }
        else if (m_grounded && (m_groundSensor == null || !m_groundSensor.State()))
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            Debug.Log("ĳ���� ���� ����: Grounded = false");
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
        Debug.Log("Jump �޼��� ����");
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
        Debug.Log("�浹�� ������Ʈ �±�: " + other.tag);

        if (other.CompareTag("NextStage"))
        {
            //HighScore.TrySet(SceneManager.GetActiveScene().buildIndex, (int)score);
            SceneManager.LoadScene(nextStageName);
            StageResulSaver.SaveStage (SceneManager.GetActiveScene().buildIndex, (int)score);
        }

        if (other.CompareTag("Respawn"))
        {
            Debug.Log("Respawn �±� �浹 ����!");

            if (!isAttacking) // ���� ���� �ƴ� ��쿡�� ���� ���� ó��
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
                            Debug.LogError("playerHUD�� null�Դϴ�. UI ���� ����!");
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
                Debug.Log("�÷��̾ ���� ���̾ ���� ���Ұ� �߻����� �ʽ��ϴ�.");
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
                Debug.LogError("playerHUD�� null�Դϴ�. UI ���� ����!");
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