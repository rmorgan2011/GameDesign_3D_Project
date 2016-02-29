using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;

public class NotePickup : MonoBehaviour {
	public GameObject noteCanvas;
	public GameObject title;
	public GameObject content;
	public GameObject player;
    public GameController gameController;
    public AudioClip noteSound;


	//these two tell which note texts to display
	//needs to be set for each different note you place
	public string noteTitle;
	public string noteContent;

	private bool isVisible = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//allow player to press a button to dismiss the popup message
		if (isVisible) {
			if(Input.GetKeyDown(KeyCode.E)){
				noteCanvas.SetActive (false);
				player.GetComponent<FirstPersonController>().enabled = true;
				Destroy (transform.gameObject);
                gameController.IncrementNotes();
                Time.timeScale = 1;

            }
		}
	
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
            Time.timeScale = 0;
			noteCanvas.SetActive (true);
			isVisible = true;
			title.GetComponent<Text> ().text = noteTitle;
			content.GetComponent<Text> ().text = noteContent;
			player.GetComponent<FirstPersonController>().enabled = false;
            player.GetComponent<AudioSource>().PlayOneShot(noteSound);

		}
	}
}
