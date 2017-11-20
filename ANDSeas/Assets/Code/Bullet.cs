using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float force;
    public Vector2 dir;
	// Use this for initialization
	void Start () {
        this.GetComponent<Rigidbody2D>().AddForce(dir * force);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
