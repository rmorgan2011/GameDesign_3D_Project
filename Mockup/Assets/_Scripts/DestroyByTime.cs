using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {

	public float lifetime;
    public GameObject explosion;

	// Use this for initialization
	void Start () {
        Invoke("destroyObj", lifetime);
	}

    void destroyObj()
    {   
        if(explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
