using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private float t;
    public GameObject bullet;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime;
        if (t >= 3)
        {
            t = 0;
            OpenFire();
        }
	}

    public void OpenFire()
    {
        Vector3 vec = this.transform.position;
        if (this.transform.localPosition != Vector3.zero)
        {
            GameObject bullet1 = Instantiate(bullet, vec,this.transform.rotation);
            bullet1.GetComponent<Bullet>().Fir(500, new Vector2(-1,0));
        }

    }
}
