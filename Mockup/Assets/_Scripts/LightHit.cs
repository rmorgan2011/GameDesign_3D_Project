using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class LightHit : MonoBehaviour {

    public GameObject[] enemies;
    public GameObject flashlight;
    private bool isVisible = false;
    public Transform playerLocation;

    void Start()
    {
        gameObject.GetComponent<AudioSource>().enabled = true;
    }
    void OnTriggerStay(Collider other)
    {
		foreach (GameObject var in enemies) {
			//   if (!Physics.Linecast(transform.position, other.transform.position))
			// {
			if (var != null) {
				if (other.tag == "Enemy" && flashlight.GetComponent<Flashlight> ().isOn) {
					if (var.GetComponent<Collider> () == other && var.GetComponent<RenderControl> ().isVisible == false) {
						StartCoroutine (FlashEnemy (var));
					}
					//     var.GetComponent<ai>
				} else if (other.tag == "Killable" && flashlight.GetComponent<Flashlight> ().isOn) {
					StartCoroutine (KillEnemy (var));
				}
				// }
			}
		}
    }

    IEnumerator FlashEnemy(GameObject other)
    {
        gameObject.GetComponent<AudioSource>().Play();
        other.transform.LookAt(playerLocation.position - new Vector3 (0,.75f,0));
        other.GetComponent<RenderControl>().isVisible = true;
        other.GetComponent<AICharacterControl>().pursuing = false;
        //flashlight.GetComponent<Flashlight>().isOn = false;

        yield return new WaitForSeconds(2f);
        other.GetComponent<RenderControl>().isVisible = false;
        int randomIndex = Random.Range(0, other.GetComponent<AICharacterControl>().spawnPoints.Length);
        other.transform.position = other.GetComponent<AICharacterControl>().spawnPoints[randomIndex].position;
        other.GetComponent<AICharacterControl>().pursuing = true;
        isVisible = false;
    }

	IEnumerator KillEnemy(GameObject other)
	{
		gameObject.GetComponent<AudioSource>().Play();
		other.transform.LookAt(playerLocation.position - new Vector3 (0,.75f,0));
		other.GetComponent<RenderControl>().isVisible = true;
		other.GetComponent<AICharacterControl>().pursuing = false;
		//flashlight.GetComponent<Flashlight>().isOn = false;

		yield return new WaitForSeconds(2f);
		Destroy (other);
	}
}
