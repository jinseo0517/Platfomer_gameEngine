using UnityEngine;
using UnityEngine.SceneManagement; // �� ������ ���� ���ӽ����̽�

public class MainMenu : MonoBehaviour
{
    // ���� ���� �ؽ�Ʈ�� ��� �ִ� �г� (Inspector â���� ����)
    public GameObject ������;

    // ���� ���� �ؽ�Ʈ�� ȭ�鿡 ǥ��
    public void ShowExplanation()
    {
        ������.SetActive(true); // ���� ��� Ȱ��ȭ
        Debug.Log("���� ��� ǥ��"); // ����� �޽��� ���
    }

    // ���� ���� �ؽ�Ʈ�� ȭ�鿡�� ����
    public void HideExplanation()
    {
        ������.SetActive(false); // ���� ��� ��Ȱ��ȭ
        Debug.Log("���� ��� ����"); // ����� �޽��� ���
    }

    // ���� ���� ��ư Ŭ�� �� ȣ��
    public void StartGame()
    {
        Debug.Log("Stage1 ������ �̵� �õ�"); // ����� �޽���
        SceneManager.LoadScene("Stage1"); // ù ��° ���������� �̵�
    }

    // ���� ���� ��ư Ŭ�� �� ȣ��
    public void QuitGame()
    {
        Debug.Log("���� ����"); // ����� �޽��� (�����Ϳ�)
        Application.Quit(); // ���� ���� (����� ���� ���Ͽ����� ����)
    }

    // ���� �޴��� �̵� ��ư Ŭ�� �� ȣ��
    public void GoToMainMenu()
    {
        Debug.Log("���� �޴��� �̵� �õ�"); // ����� �޽���
        SceneManager.LoadScene("MainMenu"); // ���� �޴� ������ �̵�
    }

    // Ư�� �±�("NextStage")�� �浹 �� ȣ��
}