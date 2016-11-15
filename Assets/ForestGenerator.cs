using UnityEngine;
using System.Collections;

public class ForestGenerator : MonoBehaviour
{

    GameObject[,] forest;
    public float size = 20.0f;
    public Parameters gameParams;

    // Use this for initialization
    void Start()
    {
        //Generate();
        if (Parameters.instance == null)
        {
            gameParams = new Parameters();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Generate()
    {   
        int s = (int)size;
        forest = new GameObject[s, s];

        for (int i = 0; i < s; i++)
        {
            for (int j = 0; j < s; j++)
            {
                float yPos = ((float)Random.Range(0, 100) / 100) * Parameters.terrainType;
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = new Vector3(i, yPos, j);
                obj.transform.parent = this.transform;
                obj.AddComponent<Field>();
                forest[i, j] = obj;
            }
        }

        forest[(int)s / 2, (int)s / 2].GetComponent<Renderer>().material.color = Color.magenta;
    }

    public void changeSize(float newSize)
    {
        size = newSize;
    }

}
