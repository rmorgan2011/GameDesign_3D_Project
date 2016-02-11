using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour {

    public GameObject cameraRotate;
    public bool isOn;

    void Start()
    {
        gameObject.GetComponent<AudioSource>().enabled = true;
    }
	void Update () {

        gameObject.transform.rotation = cameraRotate.transform.rotation;
       

        if (Input.GetMouseButtonUp(0))
        {
            isOn = !isOn;
            gameObject.GetComponent<AudioSource>().Play();
        }

        if (isOn)
        {
            gameObject.GetComponent<Light>().enabled = true;
            
        }
        else
        {
            gameObject.GetComponent<Light>().enabled = false;
        }

    }
}
