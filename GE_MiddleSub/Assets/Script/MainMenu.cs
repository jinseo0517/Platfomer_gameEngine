using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // ���� ���� �ؽ�Ʈ�� ��� �ִ� �г�
    public GameObject ������;

    // Inspector â���� ������ ���� �� �̸�
    [SerializeField] private string nextStageName; // �� �̸��� ���� �巡�� �� ��� ������� ����

    // ���� �ؽ�Ʈ�� ȭ�鿡 ǥ��
    public void ShowExplanation()
    {
        ������.SetActive(true); // ���� �ؽ�Ʈ Ȱ��ȭ
    }

    // ���� �ؽ�Ʈ�� ȭ�鿡�� ����
    public void HideExplanation()
    {
        ������.SetActive(false); // ���� �ؽ�Ʈ ��Ȱ��ȭ
    }

    // ���� ���� ��ư
    public void StartGame()
    {
        SceneManager.LoadScene("Stage1"); // ù ��° �������� �ε�
    }

    // ���� ���� ��ư
    public void QuitGame()
    {
        Application.Quit(); // ���� ����
        Debug.Log("���� ����"); // �����Ϳ� Ȯ�� �޽���
    }

    // ���� �޴��� �̵� ��ư
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // ���� �޴��� �̵�
    }

    // Ư�� ��Ͽ� ������ ���� ���������� �̵�
    private void OnTriggerEnter2D(Collider2D other)
    {
        // "NextStage" �±׸� ���� ��ϰ� �浹���� ��
        if (other.CompareTag("NextStage"))
        {
            Debug.Log(nextStageName + "�� �̵�!");
            SceneManager.LoadScene(nextStageName); // Inspector â���� �Է��� ���� �� �̸����� �̵�
        }
    }
}