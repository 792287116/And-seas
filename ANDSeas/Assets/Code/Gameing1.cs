using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Gameing1 : MonoBehaviour {
    public Button set_mesh;
    public Image start_Image;
    public Image strip_type;
    public Button HOME;
    public Button BOOK;
    private float strip_typeH = 0;
    private RectTransform strip_typeR;
    Tweener strip_typeT;
    public GameObject player;
    public GameObject Enemy;

	void Start () {
        strip_typeR = strip_type.GetComponent<RectTransform>();
        strip_typeT = DOTween.To(() => strip_typeH, x => strip_typeH = x, 190, 0.1f)
            .SetAutoKill(false)
            .Pause()
            .SetEase(Ease.Linear)
            .OnUpdate(() => {
                strip_typeR.sizeDelta = new Vector2(6, strip_typeH);
            });
        Init();
    }

    private void Init()
    {
        strip_typeR.sizeDelta = new Vector2(6, 0);
        HOME.transform.localScale = Vector3.zero;
        BOOK.transform.localScale = Vector3.zero;

        player.transform.position = new Vector3(-5f,-1,0);
        Enemy.transform.position = new Vector3(5,-1.9f,0);
    }

    void Update () {
        strip_typeH = strip_typeR.sizeDelta.y;
	}

    public void Set_Mesh() {
        set_mesh.transform.DOScale(new Vector3(0.8f,0.8f,0.8f), 0.2f)
            .OnComplete(() =>
            {
                set_mesh.transform.DOScale(Vector3.one,0.2f);
            });
        if (strip_typeH <= 0)
        {
            strip_typeT.PlayForward();
            start_Image.gameObject.SetActive(true);
            HOME.transform.DOScale(Vector3.one, 0.2f)
            .SetAutoKill(false)
            .OnComplete(() => {
                BOOK.transform.DOScale(Vector3.one, 0.2f)
                .SetAutoKill(false);
            });
        }
        else {
            start_Image.gameObject.SetActive(false);
        }
        if (strip_typeH >= 190) {
            strip_typeT.PlayBackwards();
            BOOK.transform.DOScale(Vector3.zero, 0.2f)
            .SetAutoKill(false)
            .OnComplete(() => {
                HOME.transform.DOScale(Vector3.zero, 0.2f)
                .SetAutoKill(false);
            });
        }
    }

    public void homeBut() {
        Init();
    }
    public void bookBut() {
        Init();
    }
}
