using UnityEngine;
using System.Collections;

public class Field : MonoBehaviour {

    Color color = new Color32(60, 255, 70, 1);
    Color fireColor = new Color32(240, 70, 70, 1);
    Vector3 fireSize = new Vector3(0.5f, 1, 0);
    Vector3 treeSize = new Vector3(0.1f, 0.1f, 0.1f);
    GameObject trees;
    GameObject fire;
    float density;
    float fireIntensity;

    // Use this for initialization
    void Start () {
        density = (float)Random.Range(50, 255) / 255;
        fireIntensity = (float)Random.Range(0, 100) / 20;
        this.color.g = density;
        //Debug.Log(this.color.g);
        this.gameObject.GetComponent<Renderer>().material.color = this.color;
        
        trees = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        trees.transform.parent = this.transform;
        trees.transform.position = this.transform.position + new Vector3(0, 0.3f, 0);
        trees.GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>("BrokenVector\\FreeLowPolyPack\\Models\\Tree Type3 04");

        fire = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        fire.transform.parent = this.trees.transform;
        fire.transform.position = this.trees.transform.position + new Vector3(0,6,0);
        fireSize = fire.transform.localScale + new Vector3(0.5f, 1, 0);
        fire.transform.localScale = fireSize * fireIntensity;
        fire.GetComponent<Renderer>().material.color = this.fireColor;
        trees.transform.localScale = treeSize * (3.0f * density);

    }

    // Update is called once per frame
    void Update () {
        if(fireIntensity > 0)
        {
            fireIntensity -= 0.01f;
            fire.transform.localScale = fireSize * fireIntensity;
            trees.transform.localScale *= 0.995f;
        }
	}
}
