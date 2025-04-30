using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int score = HighScore.Load(1);
        Debug.Log(score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
