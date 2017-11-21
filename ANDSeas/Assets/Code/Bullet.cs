using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private float force;
    private Vector2 dir;
    private float t;
	// Use this for initialization
	void Start () {
        
	}

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (t >= 2)
        {
            Destroy(this.gameObject);
        }
    }

    public void Fir(float f,Vector2 v) {
        force = f;
        dir = v;
        this.GetComponent<Rigidbody2D>().AddForce(dir * force);
        this.transform.rotation = Quaternion.Euler(new Vector3(0,0,30));
    }
}
