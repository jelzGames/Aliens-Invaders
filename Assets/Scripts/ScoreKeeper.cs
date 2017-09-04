using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public static int score = 0;
    Text myText;

    public void Score(int points)
    {
        score += points;
        myText.text = score.ToString();
    }

    public static void Reset()
    {
        score += 0;
        //myText.text = score.ToString();
    }
    // Use this for initialization
    void Start () {
        myText = GetComponent<Text>();
        Reset();


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
