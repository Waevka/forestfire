using UnityEngine;
using System.Collections;

public class Field : MonoBehaviour {

    ForestGenerator forestGenerator;

    GameObject treeRef;
    GameObject fireRef;

    public float fuel;
    Color color;

    public bool isBurning;

    void Awake() {
        fuel = 0.5f;
        color = new Color32(60, 255, 70, 1);
        isBurning = false; // false
    }

	// Use this for initialization
	void Start () {
        
        treeRef = transform.FindChild("FTree").gameObject;
        fireRef = treeRef.transform.FindChild("FFire").gameObject;
        forestGenerator = GameObject.Find("Forest").gameObject.GetComponent<ForestGenerator>();
        treeRef.GetComponent<Renderer>().material.color = Color.cyan;
        setColor();
        treeRef.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f) * fuel;
    }

    // Update is called once per frame
    void Update () {

        if (fuel < 0.01f)
        {
            setIsBurning(false);
        }
        // calculate
        UpdateValues();
        // update fire parameters (size, strength, etc)
        UpdateFire();
        // update tree parameters (size, color, etc)
        UpdateTree();
        // update field parameters (color)
        UpdateField();

    }

    void UpdateValues()
    {
        if (isBurning)
        {
            fuel -= forestGenerator.getBurnRate();
        }
    }

    void UpdateTree()
    {
        if (isBurning)
        {
            treeRef.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f) * fuel;
        }
    }

    void UpdateFire()
    {
        if (isBurning)
        {
            fireRef.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f) * fuel;
        } else  {
            fireRef.transform.localScale = Vector3.zero;
        }
    }

    void UpdateField()
    {
        if (isBurning)
        {
            setColor();
        }
    }

    public void setIsBurning(bool newIsBurning)
    {
        isBurning = newIsBurning;
    }

    public void setFuel(float newFuel)
    {
        fuel = newFuel;
    }

    void setColor()
    {
        float g = fuel;
        if (fuel < 0.1) g = 0.1f;
        color = new Color32(60, 255, 70, 1);
        color.g = g;
        GetComponent<Renderer>().material.color = color;
    }

}
