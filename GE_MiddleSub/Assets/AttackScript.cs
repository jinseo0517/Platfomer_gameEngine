using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    // ���� ���¸� ��Ÿ���� ����
    public bool isAttacking = false; // ���� ������ ���� Ȯ���� ���� �÷���

    // Trigger �̺�Ʈ ó��
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Trigger �̺�Ʈ ����� �α� ���
        Debug.Log($"Trigger �߻�! �浹�� ������Ʈ �̸�: {other.gameObject.name}, �±�: {other.gameObject.tag}");

        // Respawn �±� Ȯ�� �� �ı� ó��
        if (other.CompareTag("Respawn"))
        {
            Debug.Log("Respawn �±� Ȯ�� - ������Ʈ �ı� ����.");
            Destroy(other.gameObject); // Respawn ������Ʈ �ı�
        }

        // �÷��̾� ���� ���� �ƴ� ���� ü�� ���� ó��
        if (!isAttacking && other.CompareTag("Enemy"))
        {
            Debug.Log("�÷��̾ ���� ���� �ƴ� - ü�� ����.");

            // PlayerHUD�� ���� ü�� ���� ó��
            PlayerHUD playerHUD = FindObjectOfType<PlayerHUD>(); // PlayerHUD ��������
            if (playerHUD != null)
            {
                playerHUD.TakeDamage(); // ü�� ���� ó��
            }
            else
            {
                Debug.LogError("PlayerHUD�� ����Ǿ� ���� �ʾ� ü�� ���� ����.");
            }
        }
        else if (isAttacking)
        {
            Debug.Log("�÷��̾ ���� �� - ü�� ���� ����.");
        }
    }
}