using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecapTime : MonoBehaviour
{
    public Text recap;
    public Timer timeScript;

    void Update()
    {
        recap.text = timeScript.timer.text;
    }
}
