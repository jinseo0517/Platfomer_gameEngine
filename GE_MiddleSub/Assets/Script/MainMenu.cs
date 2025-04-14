using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위한 네임스페이스

public class MainMenu : MonoBehaviour
{
    // 설명 배경과 텍스트를 담고 있는 패널 (Inspector 창에서 연결)
    public GameObject 설명배경;

    // 설명 배경과 텍스트를 화면에 표시
    public void ShowExplanation()
    {
        설명배경.SetActive(true); // 설명 배경 활성화
        Debug.Log("설명 배경 표시"); // 디버그 메시지 출력
    }

    // 설명 배경과 텍스트를 화면에서 숨김
    public void HideExplanation()
    {
        설명배경.SetActive(false); // 설명 배경 비활성화
        Debug.Log("설명 배경 숨김"); // 디버그 메시지 출력
    }

    // 게임 시작 버튼 클릭 시 호출
    public void StartGame()
    {
        Debug.Log("Stage1 씬으로 이동 시도"); // 디버깅 메시지
        SceneManager.LoadScene("Stage1"); // 첫 번째 스테이지로 이동
    }

    // 게임 종료 버튼 클릭 시 호출
    public void QuitGame()
    {
        Debug.Log("게임 종료"); // 디버깅 메시지 (에디터용)
        Application.Quit(); // 게임 종료 (빌드된 실행 파일에서만 동작)
    }

    // 메인 메뉴로 이동 버튼 클릭 시 호출
    public void GoToMainMenu()
    {
        Debug.Log("메인 메뉴로 이동 시도"); // 디버깅 메시지
        SceneManager.LoadScene("MainMenu"); // 메인 메뉴 씬으로 이동
    }

    // 특정 태그("NextStage")와 충돌 시 호출
}