using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // �̵� �ӵ�
    [SerializeField] private float m_jumpForce = 7.5f; // ���� ��
    private Animator m_animator; // �ִϸ����� ������Ʈ
    private Rigidbody2D rb; // Rigidbody2D ������Ʈ
    private GroundSensor m_groundSensor; // �� ���� ����
    private bool m_grounded = false; // ���� ��� �ִ��� ����
    public float speedBoostDuration = 7f; //���� ���� ��ü�ð�
    private int life = 3;

    public PlayerHUD playerHUD;


    private bool isGiant = false;
    // Inspector â���� ���� ������ ���� �������� �̸� (�⺻��: "Stage1")
    [SerializeField] private string nextStageName = "Stage2";

    void Start()
    {
        // ������Ʈ �ʱ�ȭ
        m_animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Transform sensorTransform = transform.Find("GroundSensor");
        if (sensorTransform != null)
        {
            m_groundSensor = sensorTransform.GetComponent<GroundSensor>();
        }
        else
        {
            Debug.LogError("GroundSensor�� ã�� �� �����ϴ�!");
        }
        rb.gravityScale = 2.0f; // �߷� ����
    }

    void Update()
    {
        // Ground ���� ������Ʈ
        UpdateGroundedStatus();

        // Y�� �ӵ��� Animator�� ����
        m_animator.SetFloat("AirSpeedY", rb.velocity.y);

        // �¿� �̵� ó��
        float inputX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
        HandleMovement(inputX);

        // ĳ���� ���� ��ȯ
        UpdateCharacterDirection(inputX);

        // ���� ó��
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
        if (Mathf.Abs(inputX) > Mathf.Epsilon) // �̵� ��
        {
            m_animator.SetInteger("AnimState", 1); // Run ����
        }
        else
        {
            m_animator.SetInteger("AnimState", 0); // Idle ����
        }
    }

    void UpdateCharacterDirection(float inputX)
    {
        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false; // ������ ����
        }
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true; // ���� ����
        }
    }

    void Jump()
    {
        Debug.Log("Jump �޼��� ����");

        // ���� �ִϸ��̼� ����
        m_animator.SetTrigger("Jump");

        // Rigidbody�� ����Ͽ� ���� ���� ����
        rb.velocity = new Vector2(rb.velocity.x, m_jumpForce);

        // Ground ���� false�� ����
        m_grounded = false;
        m_animator.SetBool("Grounded", m_grounded);

        // GroundSensor�� �ӽ� ��Ȱ��ȭ (���� ó�� ���� ����)
        if (m_groundSensor != null)
        {
            m_groundSensor.Disable(0.1f);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("�浹�� ������Ʈ �±�: " + other.tag); // �浹�� ������Ʈ�� �±� ���

        // "NextStage" �±׸� ���� ������Ʈ�� �浹�� ���
        if (other.CompareTag("NextStage"))
        {
            Debug.Log(nextStageName + " ������ �̵�"); // ����� �޽���
            SceneManager.LoadScene(other.name); // ���� ���������� �̵�
        }
        //������
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
                    playerHUD.TakeDamage();
                    life--;
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
                
        }
        //�������
        if (other.CompareTag("PawerUp"))
        {
            isGiant = true;
            Invoke("PowerUpBack", speedBoostDuration);
           
            Destroy(other.gameObject);
        }
        if (other.CompareTag("SpeedUp"))
        {
            StartCoroutine(SpeedBoost());
            Destroy(other.gameObject); // ������ ����
        }
        if (other.CompareTag("heal"))
        {
            life += 1;
            if (life > 3)
            {
                life = 3;
            }
            playerHUD.PlusLife();
            Destroy(other.gameObject); // ������ ����
        }

    }

    private void PowerUpBack()
    {
        isGiant = false;
    }

    IEnumerator SpeedBoost()
    {
        float originalSpeed = moveSpeed;
        moveSpeed *= 3f; // �ӵ� ���� (���ϴ� ������ ���� ����)
        yield return new WaitForSeconds(speedBoostDuration); // ���� �ð� ���
        moveSpeed = originalSpeed; // ���� �ӵ��� ����
    }
}