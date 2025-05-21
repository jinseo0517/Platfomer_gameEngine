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
        //기존의 모든 자식 오브젝트 삭제
        foreach (Transform child in contentRoot)
        {
            Destroy(child.gameObject);
        }
        //랭크 데이터 정렬
        var sortedData = allData.results.Where(r => r.stage == _stage).OrderByDescending(x => x.score).ToList();

        //랭크 데이터 생성
        for (int i = 0; i < sortedData.Count; i ++)
        {
            GameObject row = Instantiate(rowPrefab, contentRoot);
            TMP_Text rankText = row.GetComponentInChildren<TMP_Text>();
            rankText.text = $"{i + 1}. {sortedData[i].playerName} - {sortedData[i].score}";
        }
    }
}
