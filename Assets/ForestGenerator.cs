using UnityEngine;
using System.Collections;

public class ForestGenerator : MonoBehaviour {

    GameObject[,] forest;
    public int size = 20;

	// Use this for initialization
	void Start () {

        forest = new GameObject[size,size];

        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = new Vector3(i, 0, j);
                obj.transform.parent = this.transform;
                Field f = obj.AddComponent<Field>();
                forest[i, j] = obj;
            }
        }

        forest[(int)size/2, (int)size/2].GetComponent<Renderer>().material.color = Color.magenta;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
