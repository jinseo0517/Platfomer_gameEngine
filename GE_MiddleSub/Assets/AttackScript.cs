using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    // 공격 상태를 나타내는 변수
    public bool isAttacking = false; // 공격 중인지 상태 확인을 위한 플래그

    // Trigger 이벤트 처리
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Trigger 이벤트 디버깅 로그 출력
        Debug.Log($"Trigger 발생! 충돌한 오브젝트 이름: {other.gameObject.name}, 태그: {other.gameObject.tag}");

        // Respawn 태그 확인 및 파괴 처리
        if (other.CompareTag("Respawn"))
        {
            Debug.Log("Respawn 태그 확인 - 오브젝트 파괴 실행.");
            Destroy(other.gameObject); // Respawn 오브젝트 파괴
        }

        // 플레이어 공격 중이 아닐 때만 체력 감소 처리
        if (!isAttacking && other.CompareTag("Enemy"))
        {
            Debug.Log("플레이어가 공격 중이 아님 - 체력 감소.");

            // PlayerHUD를 통해 체력 감소 처리
            PlayerHUD playerHUD = FindObjectOfType<PlayerHUD>(); // PlayerHUD 가져오기
            if (playerHUD != null)
            {
                playerHUD.TakeDamage(); // 체력 감소 처리
            }
            else
            {
                Debug.LogError("PlayerHUD가 연결되어 있지 않아 체력 감소 실패.");
            }
        }
        else if (isAttacking)
        {
            Debug.Log("플레이어가 공격 중 - 체력 감소 없음.");
        }
    }
}