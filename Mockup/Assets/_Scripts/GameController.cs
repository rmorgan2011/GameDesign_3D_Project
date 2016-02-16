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

    public float maxSprint;
    public float maxBattery;

    public float sprintReduce;
    public float batteryReduce;

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

    private int turnSpeed = 5;

	// Use this for initialization
    void Start()
    {
        endAudio.enabled = true;

        currSprint = maxSprint;
        currBattery = maxBattery;

        batteryPercent = (currBattery / maxBattery) * 100;

        isOn = flashlight.GetComponent<Flashlight>().isOn;
        flashlightText.GetComponent<GUIText>().text = "Battery: " + batteryPercent + "%";
    }
	
	// Update is called once per frame
	void Update () {


        //handles reactions for enemy distance
        foreach (GameObject var in enemies)
        {
            enemyDistance = Vector3.Distance(var.transform.position, player.transform.position);
            Debug.Log(enemyDistance);
            if(enemyDistance < flashlightThreshold)
            {
                flashlight.GetComponent<Flashlight>().isOn = false;
                isOn = false;
                if (enemyDistance < deathThreashold)
                {
                    if (var.GetComponent<RenderControl>().isVisible == false)
                    {
                        player.GetComponent<FirstPersonController>().enabled = false;
                        var.GetComponent<AICharacterControl>().pursuing = false;
                        player.transform.LookAt(var.transform);
                   //     player.GetComponent<LookatTarget>().SetTarget(var.transform);
                    //    player.GetComponent<LookatTarget>().enabled = true;
                   //     var targetRotation = Quaternion.LookRotation(var.transform.position - player.transform.position);
                   //     player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
                        var.GetComponent<RenderControl>().isVisible = true;
                        endAudio.Play();
                        Time.timeScale = 0;
                    }
                }
                
            }
        }
        

        //handles battery meter
        while (isOn && currBattery > 0f && flashlightWait == false)
        {
            Debug.Log(currBattery);
            StartCoroutine(reduceFlashlight());
        }

        if (currBattery == 0f)
        {
            flashlight.GetComponent<Flashlight>().canBeOn = false;
        }
        else
        {
            flashlight.GetComponent<Flashlight>().canBeOn = true;
        }
        isOn = flashlight.GetComponent<Flashlight>().isOn;
       
        batteryPercent = (currBattery / maxBattery) * 100;
        flashlightText.GetComponent<GUIText>().text = "Battery: " + batteryPercent + "%";
        
       
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
        yield return new WaitForSeconds(2f);
        sprintWait = false;
    }


}
