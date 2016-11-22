using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FieldInfoScript : MonoBehaviour {

    public bool visible = true;
    public Text FuelText;
    public Text TempText;
    public Text IsBurningText;
    public Field currentObject;

	// Use this for initialization
	void Start () {
        visible = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if(currentObject != null)
        {
            FuelText.text = currentObject.fuel.ToString();
            IsBurningText.text = (currentObject.isBurning? "yes" : "no");
            // temp
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
