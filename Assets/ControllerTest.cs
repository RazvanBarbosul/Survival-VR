using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour {

    public GameObject cubeTest;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("MouseScrollWheel"))
        {
            cubeTest.transform.Translate(0, 1, 0);
        }
        if (Input.GetButtonDown("Horizontal"))
        {
            cubeTest.transform.Translate(1, 0, 0);
        }
        if (Input.GetButtonDown("Vertical"))
        {
            cubeTest.transform.Translate(0, 0, 1);
        }
	}
}
