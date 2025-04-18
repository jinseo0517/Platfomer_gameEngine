using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ���� AI�� �����ϴ� Ŭ�����Դϴ�.
public class EnemyTraceController : MonoBehaviour
{
    public float moveSpeed = .1f;  // ���� �̵� �ӵ� ����
    public float raycastDistance = 50f;  // Raycast�� �ִ� �Ÿ�
    public float traceDistance = 50f;  // �÷��̾ ������ �ִ� �Ÿ�

    private Transform player;  // �÷��̾��� ��ġ�� �����ϴ� ����

    // Start�� ���� ���� �� �� �� ȣ��˴ϴ�.
    private void Start()
    {
        // "Player" �±װ� �ִ� ������Ʈ�� ã�Ƽ� �÷��̾��� Transform�� �����մϴ�.
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update�� �� �����Ӹ��� ȣ��˴ϴ�.
    private void Update()
    {
        // �÷��̾�� ���� ��ġ ���̸� ����Ͽ� ������ ���մϴ�.
        Vector2 direction = player.position - transform.position;

        // ���� �÷��̾ ���� �Ÿ�(traceDistance)�� ����ٸ� �������� �ʰ� ��ȯ�մϴ�.
        if (direction.magnitude > traceDistance)
            return;

        // ������ ����ȭ�Ͽ� ũ�Ⱑ 1�� ���ͷ� ��ȯ�մϴ�.
        Vector2 directionNormalized = direction.normalized;

        // Raycast�� �̿��� ���� �÷��̾� ������ ��ֹ�(Obstacle)�� �����մϴ�.
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, directionNormalized, raycastDistance);
        Debug.DrawRay(transform.position, directionNormalized * raycastDistance, Color.red);  // Ray�� �ð������� ǥ��

        // Raycast ����� ó���մϴ�.
        foreach (RaycastHit2D rHit in hits)
        {
            if (rHit.collider != null && rHit.collider.CompareTag("Obstacle"))  // ��ֹ��� �ִ� ���
            {
                // -90�� ȸ���� �������� �̵��Ͽ� ��ֹ��� ���մϴ�.
                Vector3 alternativeDirection = Quaternion.Euler(0f, 0f, -90f) * direction;
                transform.Translate(alternativeDirection * moveSpeed * Time.deltaTime);
            }
            else  // ��ֹ��� ������ �÷��̾� �������� �̵��մϴ�.
            {
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
        }
    }
}