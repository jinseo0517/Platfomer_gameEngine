using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // 설명 배경과 텍스트를 담고 있는 패널
    public GameObject 설명배경;

    // Inspector 창에서 연결할 다음 씬 이름
    [SerializeField] private string nextStageName; // 씬 이름을 직접 드래그 앤 드롭 방식으로 연결

    // 배경과 텍스트를 화면에 표시
    public void ShowExplanation()
    {
        설명배경.SetActive(true); // 배경과 텍스트 활성화
    }

    // 배경과 텍스트를 화면에서 숨김
    public void HideExplanation()
    {
        설명배경.SetActive(false); // 배경과 텍스트 비활성화
    }

    // 게임 시작 버튼
    public void StartGame()
    {
        SceneManager.LoadScene("Stage1"); // 첫 번째 스테이지 로드
    }

    // 게임 종료 버튼
    public void QuitGame()
    {
        Application.Quit(); // 게임 종료
        Debug.Log("게임 종료"); // 에디터용 확인 메시지
    }

    // 메인 메뉴로 이동 버튼
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // 메인 메뉴로 이동
    }

    // 특정 블록에 닿으면 다음 스테이지로 이동
    private void OnTriggerEnter2D(Collider2D other)
    {
        // "NextStage" 태그를 가진 블록과 충돌했을 때
        if (other.CompareTag("NextStage"))
        {
            Debug.Log(nextStageName + "로 이동!");
            SceneManager.LoadScene(nextStageName); // Inspector 창에서 입력한 다음 씬 이름으로 이동
        }
    }
}