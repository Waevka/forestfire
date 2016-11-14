using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UINumberUpdater : MonoBehaviour {

    string text = "20";
    public Text sliderValue;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void changeText(float number)
    {
        this.text = number.ToString();
        this.sliderValue.text = text;
    }
}
