using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIMove : MonoBehaviour {
    /*首页：Shouye,
      图鉴：Tujian
      设置: Setting
      游戏中：GameIng
    */
    public GameObject[] scenes;
    public enum Direction { up,left,right,down}
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToShouye(GameObject go)
    {
        SetScene(go, scenes[0], Direction.right);
    }

    public void ToTujian(GameObject go) {
        SetScene(go,scenes[1],Direction.up);
    }

    public void ToSetting(GameObject go)
    {
        SetScene(go, scenes[2], Direction.down);
    }

    public void ToGameIng(GameObject go)
    {
        SetScene(go, scenes[3], Direction.left);
    }

    public void SetScene(GameObject start,GameObject end, Direction direction) {
        switch (direction) {
            case Direction.up:
                ToScene(start, end, new Vector3(0, 750, 0));
                break;
            case Direction.left:
                ToScene(start, end, new Vector3(-1334, 0, 0));
                break;
            case Direction.right:
                ToScene(start, end, new Vector3(1334, 0, 0));
                break;
            case Direction.down:
                ToScene(start, end, new Vector3(0, -750, 0));
                break;
        }
    }

    private void ToScene(GameObject start, GameObject end,Vector3 v) {
        end.transform.localPosition = -v;
        end.SetActive(true);
        end.transform.DOLocalMove(new Vector3(0,0,0), 0.3f);
        start.transform.DOLocalMove(v, 0.3f)
            .OnComplete(() => {
                start.transform.localPosition = new Vector3(0, 0, 0);
                start.SetActive(false);
            });
    }
}
