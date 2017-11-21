using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour {
    public GameObject player;
    private RectTransform thisR;
    Player playerP;
	// Use this for initialization
	void Start () {
        thisR = this.GetComponent<RectTransform>();
        playerP = player.GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update () {
        thisR.sizeDelta = new Vector2(playerP.GetHP(),30);
	}
}
