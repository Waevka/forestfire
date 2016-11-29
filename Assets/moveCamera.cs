using UnityEngine;
using System.Collections;

public class moveCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("right"))
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime);
        if (Input.GetKey("left"))
            transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime);
        if (Input.GetKey("up"))
            transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime);
        if (Input.GetKey("down"))
            transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime);

    }
}
