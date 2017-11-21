using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private bool isCanJump;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // 碰撞开始    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Floor")
        {
            isCanJump = true;
        }
        else {
            isCanJump = false;
        }
    }

    public void SetJump(bool b) {
        isCanJump = b;
    }
    public bool GetJump() {
        return isCanJump;
    }
}
