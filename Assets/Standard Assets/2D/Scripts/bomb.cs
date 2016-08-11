using UnityEngine;
using System.Collections;

public class bomb : MonoBehaviour {

	void OnAnimationFinish()
    {
        Destroy(gameObject);
    }
}
