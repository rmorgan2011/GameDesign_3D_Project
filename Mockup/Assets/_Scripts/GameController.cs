using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.Cameras;

public class GameController : MonoBehaviour {

    public GameObject[] enemies;
    public GameObject player;

    public AudioSource endAudio;

    public GameObject flashlight;
    public GameObject flashlightText;
    public GameObject sprintText;
	public GameObject flashlightBattery;
	public GameObject sprintMeter;

    public float maxSprint;
    public float maxBattery;

    public float sprintReduce;
    public float batteryReduce;
	public float sprintRegen;

    private float currSprint;
    private float currBattery;

    private float batteryPercent;
    private float sprintPercent;

    private float enemyDistance;
    public float flashlightThreshold;
    public float deathThreashold;
    

    public bool isSprinting;
    public bool isOn;

    private bool sprintWait = false;
    private bool flashlightWait = false;
    

	// Use this for initialization
    void Start()
    {
        endAudio.enabled = true;

        currSprint = maxSprint;
        currBattery = maxBattery;

        batteryPercent = (currBattery / maxBattery) * 100;

        isOn = flashlight.GetComponent<Flashlight>().isOn;
    }
	
	// Update is called once per frame
	void Update () {


        //handles reactions for enemy distance
        foreach (GameObject var in enemies)
        {
            enemyDistance = Vector3.Distance(var.transform.position, player.transform.position);
          //  Debug.Log(enemyDistance);
            if(enemyDistance < flashlightThreshold)
            {
                //prevents user from keeping flashlight on if within a certain distance
                //adds a lot of tension to the game
                flashlight.GetComponent<Flashlight>().isOn = false;
                isOn = false;
                if (enemyDistance < deathThreashold && player.GetComponent<CharacterController>().isGrounded)
                {
                    //enable enemy render, stop pursuit, and play scare sound
                    if (var.GetComponent<RenderControl>().isVisible == false)
                    {
                        //disables input
                        player.GetComponent<FirstPersonController>().enabled = false;
                        var.transform.LookAt(player.transform.position - new Vector3(0, .75f, 0));
                        var.GetComponent<AICharacterControl>().pursuing = false;
                        var.GetComponent<RenderControl>().isVisible = true;
                        endAudio.Play();
                    }
                   
                    //handles looking at enemy
                    Vector3 relativePos = (var.transform.position + new Vector3(0,.75f,0)) - player.transform.position;
                    Quaternion rotation = (Quaternion.LookRotation(relativePos));
                    player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rotation, 5 * Time.deltaTime);
                    
                   
                }
                
            }
        }


        

        //handles battery meter
        while (isOn && currBattery > 0f && flashlightWait == false)
        {
           // Debug.Log(currBattery);
            StartCoroutine(reduceFlashlight());
        }

		//fire3 = left shift, check if he's sprinting
		if (Input.GetButton ("Fire3") && currSprint > 0) {
			isSprinting = true;
			StartCoroutine (reduceSprint());
		}

		if(Input.GetKeyUp(KeyCode.LeftShift) || currSprint < 1){
			isSprinting = false;
		}

		if (!isSprinting) {
			currSprint += sprintRegen;
			if (currSprint > 100) {
				currSprint = 100;
			}
			float sprintPercent = currSprint / maxSprint;
			sprintMeter.transform.localScale = new Vector3(sprintPercent, transform.localScale.y, sprintMeter.transform.localScale.z);
		}


        if (currBattery <= 0f)
        {
            flashlight.GetComponent<Flashlight>().canBeOn = false;
        }
        else
        {
            flashlight.GetComponent<Flashlight>().canBeOn = true;
        }
        isOn = flashlight.GetComponent<Flashlight>().isOn;
       
        batteryPercent = (currBattery / maxBattery);
        //flashlightText.GetComponent<GUIText>().text = "Battery: " + batteryPercent + "%";
		flashlightBattery.transform.localScale = new Vector3(batteryPercent, transform.localScale.y, flashlightBattery.transform.localScale.z);
        
       
    }
    //reduces flashlight value
    IEnumerator reduceFlashlight()
    {
        flashlightWait = true;
       
        currBattery -= batteryReduce;
        yield return new WaitForSeconds(1f);
        flashlightWait = false;
    }
    //reduces sprint value
    IEnumerator reduceSprint()
    {
        sprintWait = true;
        currSprint -= sprintReduce;
		if (currSprint < 0) {
			currSprint = 0;
			isSprinting = false;
		}
		float sprintPercent = currSprint / maxSprint;
		sprintMeter.transform.localScale = new Vector3(sprintPercent, transform.localScale.y, sprintMeter.transform.localScale.z);
        yield return new WaitForSeconds(2f);
        sprintWait = false;
    }

	public bool amSprinting(){
		return isSprinting;
		}

}
