using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int scoreValue = 0;
    public Text score;

    public int multiplier = 1; // 2, 4, 6, 8

    void Start()
    {
        score = GetComponent<Text>();
    }

    void Update()
    {
        score.text = "Score: " + scoreValue;
    }

    public IEnumerator Combo()
    {
        if (multiplier <= 10)
        {
            multiplier *= 2;
        }

        yield return new WaitForSeconds(3f);

        multiplier = 1;
    }

}
