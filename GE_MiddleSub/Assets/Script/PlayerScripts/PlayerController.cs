using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_speed = 4.0f; // �̵� �ӵ�
    [SerializeField] private float m_jumpForce = 7.5f; // ���� ��
    private Animator m_animator; // �ִϸ����� ������Ʈ
    private Rigidbody2D m_body2d; // Rigidbody2D ������Ʈ
    private GroundSensor m_groundSensor; // �� ���� ����
    private bool m_grounded = false; // ���� ��� �ִ��� ����
    // Inspector â���� ���� ������ ���� �������� �̸� (�⺻��: "Stage1")
    [SerializeField] private string nextStageName = "Stage2";

    void Start()
    {
        // ������Ʈ �ʱ�ȭ
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        Transform sensorTransform = transform.Find("GroundSensor");
        if (sensorTransform != null)
        {
            m_groundSensor = sensorTransform.GetComponent<GroundSensor>();
        }
        else
        {
            Debug.LogError("GroundSensor�� ã�� �� �����ϴ�!");
        }
        m_body2d.gravityScale = 2.0f; // �߷� ����
    }

    void Update()
    {
        // Ground ���� ������Ʈ
        UpdateGroundedStatus();

        // Y�� �ӵ��� Animator�� ����
        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        // �¿� �̵� ó��
        float inputX = Input.GetAxis("Horizontal");
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
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
        m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);

        // Ground ���� false�� ����
        m_grounded = false;
        m_animator.SetBool("Grounded", m_grounded);

        // GroundSensor�� �ӽ� ��Ȱ��ȭ (���� ó�� ���� ����)
        if (m_groundSensor != null)
        {
            m_groundSensor.Disable(0.1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("�浹�� ������Ʈ �±�: " + other.tag); // �浹�� ������Ʈ�� �±� ���

        // "NextStage" �±׸� ���� ������Ʈ�� �浹�� ���
        if (other.CompareTag("NextStage"))
        {
            Debug.Log(nextStageName + " ������ �̵�"); // ����� �޽���
            SceneManager.LoadScene(nextStageName); // ���� ���������� �̵�
        }
        else
        {
            Debug.Log("�浹�� �±װ� 'NextStage'�� �ƴ�."); // �±װ� �ٸ��� �� �޽��� ���
        }
    }
}