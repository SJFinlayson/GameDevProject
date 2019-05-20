using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    public float timeLeft = 3.0f;
    public Text StartText;
    public Text points;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        timeLeft -= Time.deltaTime;
        StartText.text = (timeLeft).ToString("0");
        if (timeLeft < 0 ) {
            StartText.text = ("Stop scooping!");
            points.gameObject.name = "Finished";
        }
    }
}
