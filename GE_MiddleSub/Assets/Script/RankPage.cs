using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class RankPage : MonoBehaviour
{
    [SerializeField] Transform contentRoot;
    [SerializeField] GameObject rowPrefab;

    StageResultList allData;

    public void buttonaction(int stage)
    {
        RefreshRankList(stage);
    }

    private void Awake()
    {
        allData = StageResulSaver.LoadRank();
        RefreshRankList(1);
    }

    void RefreshRankList(int _stage)
    {
        //������ ��� �ڽ� ������Ʈ ����
        foreach (Transform child in contentRoot)
        {
            Destroy(child.gameObject);
        }
        //��ũ ������ ����
        var sortedData = allData.results.Where(r => r.stage == _stage).OrderByDescending(x => x.score).ToList();

        //��ũ ������ ����
        for (int i = 0; i < sortedData.Count; i ++)
        {
            GameObject row = Instantiate(rowPrefab, contentRoot);
            TMP_Text rankText = row.GetComponentInChildren<TMP_Text>();
            rankText.text = $"{i + 1}. {sortedData[i].playerName} - {sortedData[i].score}";
        }
    }
}
