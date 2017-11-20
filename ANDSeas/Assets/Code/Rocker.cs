/*摇杆控制*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Rocker : MonoBehaviour {
    public float speed;
    private Vector3 parentV;//摇杆背景坐标
    private Vector3 clientV;
    private float r;//摇杆移动半径
    public GameObject player;
    private bool isWalk;
    Vector3 dir;
    //private Animator animator;
    float angle;
    private float playerX;
    private float playerY;
    private Sequence JumpT;
    private bool isJump = false;

    public GameObject bullet;
    Quaternion bulletQuaternion;
    public Vector3 vec3;
    // Use this for initialization
    void Start () {
        parentV = this.GetComponentInParent<RectTransform>().position;
        clientV = this.transform.position;
        //r = Vector3.Distance(this.transform.position,parentV);
        r = this.GetComponentInParent<RectTransform>().sizeDelta.x 
            - this.GetComponent<RectTransform>().sizeDelta.x/2;
        //animator = player.GetComponent<Animator>();

        this.gameObject.AddComponent<EventTrigger>();
        EventTrigger joystick = this.gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry joystickDrag = new EventTrigger.Entry();
        joystickDrag.eventID = EventTriggerType.Drag;
        joystickDrag.callback.AddListener((data)=> { OnpointerDrag((PointerEventData)data); });
        joystick.triggers.Add(joystickDrag);
        EventTrigger.Entry joystickDragEnd = new EventTrigger.Entry();
        joystickDragEnd.eventID = EventTriggerType.EndDrag;
        joystickDragEnd.callback.AddListener((data)=> { OnpointerDragEnd((PointerEventData)data); });
        joystick.triggers.Add(joystickDragEnd);

        //JumpT = player.transform.DOLocalJump(new Vector3(playerX, 0, 0), 1, 1, 0.5f, false)
        //    .SetAutoKill(false)
        //    .Pause()
        //    .SetEase(Ease.InOutQuad)
        //    .OnComplete(()=> {
        //        isJump = false;
        //    });

        bulletQuaternion = Quaternion.Euler(vec3);
    }
	
	// Update is called once per frame
	void Update () {
        if (isWalk)
        {
            playerX = speed * (this.transform.position.x - parentV.x) * Time.deltaTime;
            player.transform.position += 
                new Vector3(playerX, player.transform.position.y, 0);
            angle = Vector3.Angle(dir, new Vector3(1, 0, 0));
            //if (this.transform.localPosition.y > 0 && angle > 45 && angle < 135)
            //{
            //    animator.SetInteger("Walk", 0);//上
            //}
            //else if (angle >= 0 && angle <= 45)
            //{
            //    animator.SetInteger("Walk", 1);//右
            //}
            //else if (this.transform.localPosition.y < 0 && angle > 45 && angle < 135)
            //{
            //    animator.SetInteger("Walk", 2);//下
            //}
            //else if (angle >= 135 && angle <= 180)
            //{
            //    animator.SetInteger("Walk", 3);//左
            //}
        }
        else {
            //animator.SetInteger("Walk",4);
        }
	}

    private void OnpointerDrag(PointerEventData data) {
        dir = new Vector3(data.position.x, data.position.y, 0) - parentV;
        if (Vector3.Distance(data.position, parentV) < r)
        {
            this.transform.position = data.position;
        }
        else {
            transform.position = parentV + dir.normalized * r;
        }
        isWalk = true;
    }

    private void OnpointerDragEnd(PointerEventData data) {
        this.transform.position = parentV;
        isWalk = false;
    }

    public void Jump() {
        if (isJump == false)
        {
            isJump = true;
            player.transform.DOLocalJump(new Vector3(player.transform.position.x,0,0), 1, 1, 0.5f, false)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => {
                isJump = false;
            });
        }
        
    }
    //发射子弹
    public void OpenFire() {
        Vector3 vec = player
        Instantiate(bullet, vec, bulletQuaternion);
    }

}
