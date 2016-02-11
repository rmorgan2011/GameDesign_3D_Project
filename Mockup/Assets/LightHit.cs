using UnityEngine;
using System.Collections;

public class LightHit : MonoBehaviour {

    public GameObject flashlight;
    private bool isVisible = false;

    void Start()
    {
        gameObject.GetComponent<AudioSource>().enabled = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && isVisible == false && flashlight.GetComponent<Flashlight>().isOn)
       {
            StartCoroutine(FlashEnemy(other));           
       }
    }

    IEnumerator FlashEnemy(Collider other)
    {
        isVisible = true;
        gameObject.GetComponent<AudioSource>().Play();
        other.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        flashlight.GetComponent<Flashlight>().isOn = false;

        yield return new WaitForSeconds(3);
        other.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        isVisible = false;
    }
}
