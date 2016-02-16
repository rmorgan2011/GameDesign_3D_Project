using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour {

    public GameObject cameraRotate;
    public bool isOn;
    public bool canBeOn;

    void Start()
    {
        gameObject.GetComponent<AudioSource>().enabled = true;
        canBeOn = true;
    }
	void Update () {

        gameObject.transform.rotation = cameraRotate.transform.rotation;



        if (canBeOn == true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                isOn = !isOn;
                gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            isOn = false;
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
