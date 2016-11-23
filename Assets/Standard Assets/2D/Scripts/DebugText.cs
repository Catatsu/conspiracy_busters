using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class DebugText : MonoBehaviour {

    public float text = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Text>().text = "Debug : " + text.ToString();
	}
}
