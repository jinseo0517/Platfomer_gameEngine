using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject 설명배경;

    public void ShowExplanation()
    {
        if (설명배경 != null)
        {
            설명배경.SetActive(true);
            Debug.Log("설명 배경 표시");
        }
        else
        {
            Debug.LogWarning("설명배경이 설정되지 않았습니다!");
        }
    }

    public void HideExplanation()
    {
        if (설명배경 != null)
        {
            설명배경.SetActive(false);
            Debug.Log("설명 배경 숨김");
        }
        else
        {
            Debug.LogWarning("설명배경이 설정되지 않았습니다!");
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void QuitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}