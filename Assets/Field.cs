using UnityEngine;
using System.Collections;

public class Field : MonoBehaviour {

    ForestGenerator forestGenerator;

    GameObject treeRef;
    GameObject fireRef;

	// Use this for initialization
	void Start () {
        treeRef = transform.FindChild("FTree").gameObject;
        fireRef = treeRef.transform.FindChild("FFire").gameObject;
        forestGenerator = GameObject.Find("Forest").gameObject.GetComponent<ForestGenerator>();
        treeRef.GetComponent<Renderer>().material.color = Color.cyan;
    }

    // Update is called once per frame
    void Update () {
        // update fire parameters (size, strength, etc)
        UpdateFire();
        // update tree parameters (size, color, etc)
        UpdateTree();
        // update field parameters (color)
        UpdateField();
    }

    void UpdateTree()
    {

    }

    void UpdateFire()
    {

    }

    void UpdateField()
    {

    }
}
