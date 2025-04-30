using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoretext;
    void Start()
    {
        switch(gameObject.name)
        {
            case "score1":
                scoretext.text = $"stage{gameObject.name[5]} : " + PlayerPrefs.GetInt("HighScore_1");
                break;
            case "score2":
                scoretext.text = $"stage{gameObject.name[5]} : " + PlayerPrefs.GetInt("HighScore_2");
                break;
            case "score3":
                scoretext.text = $"stage{gameObject.name[5]} : " + PlayerPrefs.GetInt("HighScore_3");
                break;
            case "score4":
                scoretext.text = $"stage{gameObject.name[5]} : " + PlayerPrefs.GetInt("HighScore_4");
                break;
            case "score5":
                scoretext.text = $"stage{gameObject.name[5]} : " + PlayerPrefs.GetInt("HighScore_5");
                break;
            default:
                break;
        }
    }
}
