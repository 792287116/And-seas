using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private bool isCanJump;
    private float HP = 300;
    public 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("击中了");
        if (collider.tag == "EnemyBullet")
        {
            HP -= 10;
            Destroy(collider.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("击中了");
        if (collision.collider.tag == "EnemyBullet")
        {
            HP -= 10;
            Destroy(collision.gameObject);
        }
    }

    public void SetJump(bool b) {
        isCanJump = b;
    }
    public bool GetJump() {
        return isCanJump;
    }

    public float GetHP() {
        return HP;
    }
}
