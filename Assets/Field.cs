using UnityEngine;
using System.Collections;
using System;

public class Field : MonoBehaviour {

    ForestGenerator forestGenerator;

    GameObject treeRef;
    GameObject fireRef;

    int x;
    int y;

    public float fuel;
    public float energy = 300000;
    public float temp;
    Color color;

    public bool isBurning;
    public bool burned;

    void Awake() {
        fuel = 0.5f;
        color = new Color32(60, 255, 70, 1);
        isBurning = false; // false
        burned = false;
    }

	// Use this for initialization
	void Start () {
        
        treeRef = transform.FindChild("FTree").gameObject;
        fireRef = treeRef.transform.FindChild("FFire").gameObject;
        forestGenerator = GameObject.Find("Forest").gameObject.GetComponent<ForestGenerator>();
        setMesh();
        treeRef.GetComponent<Renderer>().material.color = Color.cyan;
        setColor();
        treeRef.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f) * fuel;
    }

    public float getTotalFuelPercent()
    {
        return forestGenerator.getTotalFuelPercent();
    }

    void setMesh()
    {
        switch (forestGenerator.forestType)
        {
            case 0:
                treeRef.GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>("BrokenVector\\FreeLowPolyPack\\Models\\Tree Type1 04");
                break;
            case 1:
                treeRef.GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>("BrokenVector\\FreeLowPolyPack\\Models\\Tree Type3 04");
                break;
            case 2:
                treeRef.GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>("BrokenVector\\FreeLowPolyPack\\Models\\Tree Type1 04");
                break;
        }
    }

    // Update is called once per frame
    void Update () {

        if (fuel < 0.01f)
        {
            setIsBurning(false);
            burned = true;
        }
        switch (forestGenerator.forestType)
        {
            case 0:
                energy = 300000;
                break;
            case 1:
                energy = 250000;
                break;
            case 2:
                energy = 350000;
                break;
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
            temp = forestGenerator.combustionTemp + forestGenerator.getBurnRate() * fuel * energy;/*energia drewna*/;
        }
        else
        {
            if (temp >= forestGenerator.combustionTemp && !burned) isBurning = true;
        }
        if (!burned)
        {
            float eR = forestGenerator.getExchangeRate();
            // calculate cellular automata
            // [1][2][3]
            // [4][ ][5]
            // [6][7][8]
            float T2 = forestGenerator.getTempAtXY(x, y - 1);
            float T4 = forestGenerator.getTempAtXY(x - 1, y);
            float T5 = forestGenerator.getTempAtXY(x + 1, y);
            float T7 = forestGenerator.getTempAtXY(x, y + 1);

            // wind
            if (forestGenerator.windDirection != 0)
            {
                switch (forestGenerator.windDirection)
                {
                    case 1:
                        if (forestGenerator.getIsBurning(x, y + 1))
                        {
                            T7 *= forestGenerator.simulationSpeed * forestGenerator.windSpeed / forestGenerator.dx * 4;
                            //T7 *= forestGenerator.getAreaTouching(x, y + 1, x, y);
                        }
                        break;
                    case 2:
                        if (forestGenerator.getIsBurning(x, y - 1))
                        {
                            T2 *= forestGenerator.simulationSpeed * forestGenerator.windSpeed / forestGenerator.dx * 4;
                            //T2 *= forestGenerator.getAreaTouching(x, y - 1, x, y);
                        }
                        break;
                    case 3:
                        if (forestGenerator.getIsBurning(x - 1, y))
                        {
                            T4 *= forestGenerator.simulationSpeed * forestGenerator.windSpeed / forestGenerator.dx * 4;
                            //T4 *= forestGenerator.getAreaTouching(x - 1, y, x, y);
                        }
                        break;
                    case 4:
                        if (forestGenerator.getIsBurning(x + 1, y))
                        {
                            T5 *= forestGenerator.simulationSpeed * forestGenerator.windSpeed / forestGenerator.dx * 4;
                            //T5 *= forestGenerator.getAreaTouching(x + 1, y, x, y);
                        }
                        break;
                }
            }

            //Terrain height
            if (forestGenerator.getIsBurning(x, y + 1))
            {
                T7 *= forestGenerator.getAreaTouching(x, y + 1, x, y);
            }
            if (forestGenerator.getIsBurning(x, y - 1))
            {
                T2 *= forestGenerator.getAreaTouching(x, y - 1, x, y);
            }
            if (forestGenerator.getIsBurning(x - 1, y))
            {
                T4 *= forestGenerator.getAreaTouching(x - 1, y, x, y);
            }
            if (forestGenerator.getIsBurning(x + 1, y))
            {
                T5 *= forestGenerator.getAreaTouching(x + 1, y, x, y);
            }
            //T2 *= forestGenerator.getAreaTouching(x, y - 1, x, y);
            // T4 *= forestGenerator.getAreaTouching(x - 1, y, x, y);
            //T5 *= forestGenerator.getAreaTouching(x + 1, y, x, y);
            //T7 *= forestGenerator.getAreaTouching(x, y + 1, x, y);


            // Finally
            float newTemp = T2 * eR + T4 * eR + T5 * eR +
                T7 * eR - 4 * temp * eR;
            temp += newTemp;

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

    public void setXY(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

}
