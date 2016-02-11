using UnityEngine;
using System.Collections;

public class RenderControl : MonoBehaviour {

    public bool isVisible = false;
	
	// Update is called once per frame
	void Update () {
        if (!isVisible)
        {
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        }
        else
        {
            gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        }

    }
}
