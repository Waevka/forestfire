using UnityEngine;
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
    ///

    // Use this for initialization
    void Start()
    {
        density = 0.75f;
        terrainType = 0;
        forestType = 0;
        size = 20;
        changeWindDirection(0);
        burnRate = 0.001f;
        simulationSpeed = 1.0f;

        //Generate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = new RaycastHit();
            bool clicked = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);

            if (clicked)
            {
                if(hit.transform.gameObject.name == "FTree")
                {
                    hit.transform.gameObject.transform.parent.GetComponent<Field>().setIsBurning(true);
                } else if (hit.transform.gameObject.name == "NewField(Clone)")
                {
                    hit.transform.gameObject.GetComponent<Field>().setIsBurning(true);
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
                obj.GetComponent<Field>().setFuel(densityMap[i,j]);
                obj.GetComponent<Field>().setIsBurning(false);
                forest[i, j] = obj;
            }
        }

    }

    public void changeSize(float newSize)
    {
        size = newSize;
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
            wind.transform.Rotate(Vector3.up * 90);
        }
        if (newDir == 4)
        {
            wind.transform.localRotation = Quaternion.identity;
            wind.transform.Rotate(Vector3.down * 90);
        }
    }

    public void changeWindStrength(float newStrength)
    {
        windStrength = newStrength/4;
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
}
