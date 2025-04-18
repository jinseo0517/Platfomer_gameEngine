using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 적 추적 AI를 구현하는 클래스입니다.
public class EnemyTraceController : MonoBehaviour
{
    public float moveSpeed = .1f;  // 적의 이동 속도 설정
    public float raycastDistance = 50f;  // Raycast의 최대 거리
    public float traceDistance = 50f;  // 플레이어를 추적할 최대 거리

    private Transform player;  // 플레이어의 위치를 저장하는 변수

    // Start는 게임 시작 시 한 번 호출됩니다.
    private void Start()
    {
        // "Player" 태그가 있는 오브젝트를 찾아서 플레이어의 Transform을 저장합니다.
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update는 매 프레임마다 호출됩니다.
    private void Update()
    {
        // 플레이어와 적의 위치 차이를 계산하여 방향을 구합니다.
        Vector2 direction = player.position - transform.position;

        // 만약 플레이어가 추적 거리(traceDistance)를 벗어났다면 추적하지 않고 반환합니다.
        if (direction.magnitude > traceDistance)
            return;

        // 방향을 정규화하여 크기가 1인 벡터로 변환합니다.
        Vector2 directionNormalized = direction.normalized;

        // Raycast를 이용해 적과 플레이어 사이의 장애물(Obstacle)을 감지합니다.
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, directionNormalized, raycastDistance);
        Debug.DrawRay(transform.position, directionNormalized * raycastDistance, Color.red);  // Ray를 시각적으로 표시

        // Raycast 결과를 처리합니다.
        foreach (RaycastHit2D rHit in hits)
        {
            if (rHit.collider != null && rHit.collider.CompareTag("Obstacle"))  // 장애물이 있는 경우
            {
                // -90도 회전한 방향으로 이동하여 장애물을 피합니다.
                Vector3 alternativeDirection = Quaternion.Euler(0f, 0f, -90f) * direction;
                transform.Translate(alternativeDirection * moveSpeed * Time.deltaTime);
            }
            else  // 장애물이 없으면 플레이어 방향으로 이동합니다.
            {
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
        }
    }
}