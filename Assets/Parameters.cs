﻿using UnityEngine;
using System.Collections;

public class Parameters : MonoBehaviour {

    public static Parameters instance = null;
    public static float density = 0.75f;

	// Use this for initialization
	void Start () {
	    if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(instance);
        }
        density = 0.75f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void updateDensity(float newDensity)
    {
        density = newDensity;
    }
}