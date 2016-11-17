using UnityEngine;
using System.Collections;

public class ForestGenerator : MonoBehaviour
{
    
    GameObject[,] forest;
    public GameObject firePrefab;
    public GameObject fieldPrefab;

    /// 
    public float size = 20.0f; //TODO: change to int
    public float density;
    public int terrainType;
    public int forestType;
    ///

    // Use this for initialization
    void Start()
    {
        density = 0.75f;
        terrainType = 0;
        forestType = 0;
        size = 20;
        
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

}
