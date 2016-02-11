using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour {

    public GameObject cameraRotate;

	void Update () {

        gameObject.transform.rotation = cameraRotate.transform.rotation;

	}
}
