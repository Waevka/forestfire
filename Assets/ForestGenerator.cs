﻿using UnityEngine;
using System.Collections;

public class ForestGenerator : MonoBehaviour
{
    
    GameObject[,] forest;
    public GameObject firePrefab;
    public GameObject fieldPrefab;
    public WindZone wind;
    float[,] heightMap;
    float[,] densityMap;

    /// 
    public float size = 20.0f; //TODO: change to int
    public float dx; // m
    public float h; //s
    public float density;
    public int terrainType;
    public int forestType;
    public int windDirection;
    public Vector3 windVector;
    public float windStrength;
    ///

    ///
    public float burnRate;
    public float simulationSpeed;
    public float combustionTemp;
    public float airTemp;
    public float exchangeRate;
    public float windSpeed;
    ///

    // Use this for initialization
    void Start()
    {
        density = 0.75f;
        terrainType = 0;
        forestType = 0;
        size = 20;
        dx = 10.0f;
        h = 1.0f;

        changeWindDirection(0);
        burnRate = 0.001f;
        simulationSpeed = 1.0f;
        combustionTemp = 250.0f;
        airTemp = 20.0f;
        exchangeRate = 0.1f;
        windSpeed = 3.0f; //0.03m/s

        //Generate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            RaycastHit hit = new RaycastHit();
            bool clicked = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);

            if (clicked)
            {
                if(hit.transform.gameObject.name == "FTree")
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        hit.transform.gameObject.transform.parent.GetComponent<Field>().setIsBurning(true);
                        hit.transform.gameObject.transform.parent.GetComponent<Field>().temp = combustionTemp;
                    } else if (Input.GetMouseButtonDown(1))
                    {
                        GameObject.Find("FieldInfoCanvas").gameObject.GetComponent<FieldInfoScript>().setCurrentObject(
                            hit.transform.gameObject.transform.parent.GetComponent<Field>());
                    }
                } else if (hit.transform.gameObject.name == "NewField(Clone)")
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        hit.transform.gameObject.GetComponent<Field>().setIsBurning(true);
                        hit.transform.gameObject.GetComponent<Field>().temp = combustionTemp;
                    } else if (Input.GetMouseButtonDown(1))
                    {
                        GameObject.Find("FieldInfoCanvas").gameObject.GetComponent<FieldInfoScript>().setCurrentObject(
                            hit.transform.gameObject.GetComponent<Field>());
                    }
                }
            }
        }
    }

    public void Generate()
    {   
        int s = (int)size;
        
        if (this.forest != null) {
            foreach (GameObject o in forest)
            {
              Destroy(o);
            }
        };

        fillHeightMap(s, s);
        fillDensityMap(s, s);

        forest = new GameObject[s, s];

        for (int i = 0; i < s; i++)
        {
            for (int j = 0; j < s; j++)
            {
                float yPos = heightMap[i, j] * (terrainType*3+0.3f);
                GameObject obj = (GameObject)Instantiate(fieldPrefab, new Vector3(i, yPos, j), Quaternion.identity);
                obj.GetComponent<Field>().setXY(i, j);
                obj.GetComponent<Field>().setFuel(densityMap[i,j]);
                obj.GetComponent<Field>().setIsBurning(false);
                obj.GetComponent<Field>().temp = airTemp;
                forest[i, j] = obj;
            }
        }

        Camera.main.transform.position = new Vector3(s * 0.5f, 8 + terrainType + s*0.1f, -5);

    }

    public void changeSize(float newSize)
    {
        size = newSize;
        Generate();
    }

    public void changeDensity(float newDensity)
    {
        density = newDensity;
    }

    public void changeTerrain(int newTerrain)
    {
        terrainType = newTerrain;
    }

    public void changeForestType(int newType)
    {
        forestType = newType;
    }

    public void changeWindDirection(int newDir)
    {
        windDirection = newDir;
        if (newDir == 0)
        {
            wind.transform.localRotation = Quaternion.identity;
            wind.transform.Rotate(Vector3.left * 90);
        }
        if (newDir == 1)
        {
            wind.transform.localRotation = Quaternion.identity;
        }
        if (newDir == 2)
        {
            wind.transform.localRotation = Quaternion.identity;
            wind.transform.Rotate(Vector3.down*180);
        }
        if (newDir == 3)
        {
            wind.transform.localRotation = Quaternion.identity;
            wind.transform.Rotate(Vector3.down * 90);
        }
        if (newDir == 4)
        {
            wind.transform.localRotation = Quaternion.identity;
            wind.transform.Rotate(Vector3.up * 90);
        }
    }

    public void changeWindStrength(float newStrength)
    {
        windStrength = newStrength/4;
        windSpeed = 3.0f * (1-newStrength) / 8; // need to adjust interface values to wind
        wind.windMain = windStrength;

    }
    
    public void changeSimulationSpeed(int newSpeed)
    {
        if (newSpeed == 0) simulationSpeed = 0.25f;
        if (newSpeed == 1) simulationSpeed = 0.5f;
        if (newSpeed == 2) simulationSpeed = 1.0f;
        if (newSpeed == 3) simulationSpeed = 1.5f;
        if (newSpeed == 4) simulationSpeed = 2.0f;
    }

    void fillHeightMap(int x, int y)
    {   
        float randX = Random.Range(0f, 1.0f) + terrainType*0.1f;
        float randY = Random.Range(0f, 1.0f) + terrainType*0.1f;
        heightMap = new float[x, y];
        for(int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                float height = Mathf.PerlinNoise(randX + i*0.1f, randY + j*0.1f);
                heightMap[i, j] = height;
                //Debug.Log(height);
            }
        }
    }

    void fillDensityMap(int x, int y)
    {
        float randX = Random.Range(0f, 1.0f);
        float randY = Random.Range(0f, 1.0f);
        densityMap = new float[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                float densityM = Mathf.PerlinNoise(randX + i, randY + j);
                if (densityM < 0.01) densityM = 0.0f;
                if (densityM > 0.99) densityM = 1.0f;
                densityMap[i, j] = densityM * 2*density;
                //Debug.Log(density);
            }
        }
    }

    public float getBurnRate()
    {
        return burnRate * simulationSpeed;
    }

    public float getExchangeRate()
    {
        return exchangeRate * simulationSpeed;
    }

    public float getTempAtXY(int _x, int _y)
    {
        if(_x < 0 || _y < 0 || _x >= (int)size || _y >= (int)size)
        {
            return airTemp;
        } else
        {
            return forest[_x, _y].GetComponent<Field>().temp;
        }
    }

    public bool getIsBurning(int _x, int _y)
    {
        if (_x < 0 || _y < 0 || _x >= (int)size || _y >= (int)size)
        {
            return false;
        }
        else
        {
            return forest[_x, _y].GetComponent<Field>().isBurning;
        }
    }

    // 0-1
    public float getAreaTouching(int _x1, int _y1, int _x2, int _y2) //1 - to, 2 - from 
    {
        if (_x1 < 0 || _y1 < 0 || _x1 >= (int)size || _y1 >= (int)size)
        {
            return 1.0f;
        }
        else
        {   
            float heightTo = forest[_x1, _y1].transform.position.y;
            float heightFrom = forest[_x2, _y2].transform.position.y;
            if (heightFrom >= heightTo) { return 1.1f; }
            else { return terrainType * 3 + 0.3f - heightTo + 0.1f; }
        }
    }
}
