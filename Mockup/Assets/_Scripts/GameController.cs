using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

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
    

    public bool isSprinting;
    public bool isOn;

    private bool sprintWait = false;
    private bool flashlightWait = false;

	// Use this for initialization
    void Start()
    {
        currSprint = maxSprint;
        currBattery = maxBattery;

        batteryPercent = (currBattery / maxBattery) * 100;

        isOn = flashlight.GetComponent<Flashlight>().isOn;
        flashlightText.GetComponent<GUIText>().text = "Battery: " + batteryPercent + "%";
    }
	
	// Update is called once per frame
	void Update () {
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

    IEnumerator reduceFlashlight()
    {
        flashlightWait = true;
       
        currBattery -= batteryReduce;
        yield return new WaitForSeconds(2f);
        flashlightWait = false;
    }

    IEnumerator reduceSprint()
    {
        sprintWait = true;
        currSprint -= sprintReduce;
        yield return new WaitForSeconds(2f);
        sprintWait = false;
    }


}
