using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecapPoint : MonoBehaviour
{
    public Text recap;
    public Score scoreScript;

    void Update()
    {
        recap.text = scoreScript.score.text;
    }
}
