using UnityEngine;
using UnityEngine.SceneManagement; // �� ������ ���� ���ӽ����̽�

public class MainMenu : MonoBehaviour
{
    // ���� ���� �ؽ�Ʈ�� ��� �ִ� �г� (Inspector â���� ����)
    public GameObject ������;

    // Inspector â���� ���� ������ ���� �������� �̸� (�⺻��: "Stage1")
    [SerializeField] private string nextStageName = "Stage1";

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
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("�浹�� ������Ʈ �±�: " + other.tag); // �浹�� ������Ʈ�� �±� ���

        // "NextStage" �±׸� ���� ������Ʈ�� �浹�� ���
        if (other.CompareTag("NextStage"))
        {
            Debug.Log(nextStageName + " ������ �̵�"); // ����� �޽���
            SceneManager.LoadScene(nextStageName); // ���� ���������� �̵�
        }
        else
        {
            Debug.Log("�浹�� �±װ� 'NextStage'�� �ƴ�."); // �±װ� �ٸ��� �� �޽��� ���
        }
    }
}