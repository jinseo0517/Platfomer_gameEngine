using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject ������;

    public void ShowExplanation()
    {
        if (������ != null)
        {
            ������.SetActive(true);
            Debug.Log("���� ��� ǥ��");
        }
        else
        {
            Debug.LogWarning("�������� �������� �ʾҽ��ϴ�!");
        }
    }

    public void HideExplanation()
    {
        if (������ != null)
        {
            ������.SetActive(false);
            Debug.Log("���� ��� ����");
        }
        else
        {
            Debug.LogWarning("�������� �������� �ʾҽ��ϴ�!");
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void QuitGame()
    {
        Debug.Log("���� ����");
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        Debug.Log("���� �޴��� �̵� �õ�");
        if (SceneManager.GetSceneByName("MainMenu").IsValid())
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            Debug.LogError("MainMenu ���� Build Settings�� �߰��Ǿ� ���� �ʽ��ϴ�!");
        }
    }
}