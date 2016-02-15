using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class LightHit : MonoBehaviour {

    public GameObject[] enemies;
    public GameObject flashlight;
    private bool isVisible = false;

    void Start()
    {
        gameObject.GetComponent<AudioSource>().enabled = true;
    }
    void OnTriggerEnter(Collider other)
    {
        foreach (GameObject var in enemies)
        {
            if (other.tag == "Enemy" && flashlight.GetComponent<Flashlight>().isOn)
            {
                if (var.GetComponent<Collider>() == other && var.GetComponent<RenderControl>().isVisible == false) {
                    StartCoroutine(FlashEnemy(var));
                }
           //     var.GetComponent<ai>
            }
        }
    }

    IEnumerator FlashEnemy(GameObject other)
    {//
        gameObject.GetComponent<AudioSource>().Play();
        other.GetComponent<RenderControl>().isVisible = true;
        other.GetComponent<AICharacterControl>().pursuing = false;
        //flashlight.GetComponent<Flashlight>().isOn = false;

        yield return new WaitForSeconds(2f);
        other.GetComponent<RenderControl>().isVisible = false;
        other.GetComponent<AICharacterControl>().pursuing = true;
        isVisible = false;
    }
}
