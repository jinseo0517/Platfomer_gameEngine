using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� AI�� �̵��� �����ϴ� Ŭ�����Դϴ�.
public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;  // ���� �̵� �ӵ�
    public Rigidbody2D rb;  // ���� Rigidbody2D ������Ʈ
    private bool isMovingRight = true;  // ���� ���������� �̵� ������ ���θ� ��Ÿ���� �÷���

    // Start�� ���� ���� �� �� �� ȣ��˴ϴ�.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // ���� Rigidbody2D ������Ʈ�� �����ɴϴ�.
    }

    // Update�� �� �����Ӹ��� ȣ��˴ϴ�.
    void Update()
    {
        // ���� �̵� ���⿡ ���� �ӵ��� �����մϴ�.
        if (isMovingRight)
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);  // ���������� �̵�
        else
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);  // �������� �̵�
    }

    // �浹 ����: ���� "Boundary" �±װ� �ִ� ������Ʈ�� �浹���� �� ȣ��˴ϴ�.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))  // �浹�� ������Ʈ�� "Boundary" �±׸� ������ �ִ��� Ȯ��
        {
            isMovingRight = !isMovingRight;  // �̵� ������ �ݴ�� �ٲߴϴ�.
        }
    }
}