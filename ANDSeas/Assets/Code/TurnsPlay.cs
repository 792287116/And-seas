/*轮播*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TurnsPlay : MonoBehaviour {
    public Button buttonBefore;
    public Button buttonNext;
    public Image[] picture;
    public Sprite[] sprites;
    Tweener turnTween;//位移动画
    Sequence sequence;
    float w;
    int index = 1;//图片序号
    bool isBox0;
	// Use this for initialization
	void Start () {
        w = picture[0].GetComponent<RectTransform>().sizeDelta.x;
        picture[0].sprite = sprites[0];
        isBox0 = true;
        picture[1].gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayTween(bool left) {
        //超出图集数量归零
        if (index >= sprites.Length)
        {
            index = 0;
        }
        if (isBox0)
        {
            picture[1].sprite = sprites[index++];
        }
        else
        {
            picture[0].sprite = sprites[index++];
        }
        if (left){
            //向左
            if (isBox0)
            {
                PlayT(1,true);
            }
            else {
                PlayT(1, false);
            }
        }
        else {
            //向右
            if (isBox0)
            {
                PlayT(-1,true);
            }
            else
            {
                PlayT(-1,false);
                
            }
        }
        
        isBox0 = !isBox0;
    }

    private void PlayT(int left,bool box0) {
        int i, j;
        if (box0)
        {
            i = 1;
            j = 0;
        }
        else {
            i = 0;
            j = 1;
        }
        picture[i].transform.localPosition = new Vector3(left*w/2, 0, 0);
        picture[i].gameObject.SetActive(true);
        picture[i].transform.DOScale(Vector3.one, 0.3f);
        picture[i].transform.DOLocalMoveX(0, 0.3f)
            .OnComplete(() => {
                picture[i].gameObject.SetActive(true);
                buttonBefore.enabled = true;
                buttonNext.enabled = true;
            });
        picture[j].transform.DOScale(Vector3.zero, 0.3f);
        picture[j].transform.DOLocalMoveX(-left*w/2, 0.3f)
            .OnComplete(() => {
                picture[j].gameObject.SetActive(false);
            });
    }

    public void PlayBefore() {
        PlayTween(true);
        buttonBefore.transform.DOScale(new Vector3(0.8f,0.8f,0.8f),0.3f)
            .OnComplete(()=> {
                buttonBefore.transform.DOScale(Vector3.one, 0.3f);
            });
        buttonBefore.transform.GetChild(0).transform.DOLocalMoveX(0, 0.3f).OnComplete(() => {
            buttonBefore.transform.GetChild(0).transform.DOLocalMoveX(14.62f, 0.3f);
        });
        buttonBefore.enabled = false;
    }
    public void PlayNext() {
        PlayTween(false);
        buttonNext.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.3f)
            .OnComplete(() => {
                buttonNext.transform.DOScale(Vector3.one, 0.3f);
            });
        buttonNext.transform.GetChild(0).transform.DOLocalMoveX(0, 0.3f).OnComplete(() => {
            buttonNext.transform.GetChild(0).transform.DOLocalMoveX(14.62f, 0.3f);
        });
        buttonNext.enabled = false;
    }
}
