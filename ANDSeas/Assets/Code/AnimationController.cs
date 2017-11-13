using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour {
    public Sprite[] frameImage;
    private float t;
    private int startF, endF;
    private bool _Loop;
    private bool isPlay;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isPlay)
        {
            PlayFrame(startF,endF,_Loop);
        }
	}

    public void PlayFrame(int i,int j,bool loop) {
        isPlay = true;
    }

    
}
