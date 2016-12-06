using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FieldInfoScript : MonoBehaviour {

    public bool visible = true;
    public Text FuelText;
    public Text TempText;
    public Text IsBurningText;
    public Text fuelPercent;
    public Text T2;
    public Text T4;
    public Text T5;
    public Text T7;
    public Text eR;
    public Text newTemp;
    public Text outTemp;
    public Text windSpeed;
    public Field currentObject;

	// Use this for initialization
	void Start () {
        visible = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if(currentObject != null)
        {
            FuelText.text = (currentObject.fuel * 100).ToString() + " kg";
            IsBurningText.text = (currentObject.isBurning? "yes" : "no");
            TempText.text = currentObject.temp.ToString() + "oC";
            fuelPercent.text = currentObject.getTotalFuelPercent() + "%";
            T2.text = currentObject.T2.ToString();
            T4.text = currentObject.T4.ToString();
            T5.text = currentObject.T5.ToString();
            T7.text = currentObject.T7.ToString();
            eR.text = currentObject.eR.ToString();
            newTemp.text = currentObject.newTemp.ToString();
            outTemp.text = currentObject.tempOut.ToString();
            windSpeed.text = currentObject.windSpeed.ToString();
        }
	}

    public void changeVisibility(bool v)
    {
        visible = v;
        setVisible();
    }

    void setVisible()
    {
        if (visible == true)
        {
            this.gameObject.GetComponent<CanvasGroup>().alpha = 1.0f;
        } else
        {
            this.gameObject.GetComponent<CanvasGroup>().alpha = 0.0f;
        }
    }

    public void setCurrentObject(Field o)
    {
        currentObject = o;
        //Debug.Log(currentObject);
    }
}
