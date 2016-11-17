using UnityEngine;
using System.Collections;

public class ForestGenerator : MonoBehaviour
{
    
    GameObject[,] forest;
    public GameObject firePrefab;
    public GameObject fieldPrefab;
    public WindZone wind;

    /// 
    public float size = 20.0f; //TODO: change to int
    public float density;
    public int terrainType;
    public int forestType;
    public int windDirection;
    public Vector3 windVector;
    public float windStrength;
    ///

    // Use this for initialization
    void Start()
    {
        density = 0.75f;
        terrainType = 0;
        forestType = 0;
        size = 20;
        changeWindDirection(0);

        //Generate();
    }

    // Update is called once per frame
    void Update()
    {

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

        forest = new GameObject[s, s];

        for (int i = 0; i < s; i++)
        {
            for (int j = 0; j < s; j++)
            {
                float yPos = ((float)Random.Range(0, 100) / 100) * terrainType;
                GameObject obj = (GameObject)Instantiate(fieldPrefab, new Vector3(i, yPos, j), Quaternion.identity);
                obj.GetComponent<Field>().setFuel(Random.Range(0f, 1.0f));
                obj.GetComponent<Field>().setIsBurning(true);
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

}
