using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 적 AI의 이동을 관리하는 클래스입니다.
public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;  // 적의 이동 속도
    public Rigidbody2D rb;  // 적의 Rigidbody2D 컴포넌트
    private bool isMovingRight = true;  // 적이 오른쪽으로 이동 중인지 여부를 나타내는 플래그

    // Start는 게임 시작 시 한 번 호출됩니다.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // 적의 Rigidbody2D 컴포넌트를 가져옵니다.
    }

    // Update는 매 프레임마다 호출됩니다.
    void Update()
    {
        // 적의 이동 방향에 따라 속도를 설정합니다.
        if (isMovingRight)
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);  // 오른쪽으로 이동
        else
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);  // 왼쪽으로 이동
    }

    // 충돌 감지: 적이 "Boundary" 태그가 있는 오브젝트와 충돌했을 때 호출됩니다.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))  // 충돌한 오브젝트가 "Boundary" 태그를 가지고 있는지 확인
        {
            isMovingRight = !isMovingRight;  // 이동 방향을 반대로 바꿉니다.
        }
    }
}