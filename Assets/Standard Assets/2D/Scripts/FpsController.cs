using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class FpsController : MonoBehaviour {

    public Text fpsLabel;

    int frameCount = 0;
    int oldFrame = 0;
    float time = 0;

	void Start () {
        Application.targetFrameRate = 60;	
	}
	

	void Update () {
        ++frameCount;
        time += Time.deltaTime;

        if( time >= 1.0f )
        {
            if( oldFrame != frameCount )
            {
                fpsLabel.text = "Fps:" + frameCount;
                oldFrame = frameCount;
            }
            frameCount = 0;
            time = 0;
        }
	}
}
