/*摇杆控制*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rocker : MonoBehaviour {
    public float speed;
    private Vector3 parentV;//摇杆背景坐标
    private float r;//摇杆移动半径
    public GameObject player;
    private bool isWalk;
    Vector3 dir;
    private Animator animator;
    float angle;
    // Use this for initialization
    void Start () {
        parentV = this.GetComponentInParent<RectTransform>().position;
        //r = Vector3.Distance(this.transform.position,parentV);
        r = this.GetComponentInParent<RectTransform>().sizeDelta.x 
            - this.GetComponent<RectTransform>().sizeDelta.x/2;
        animator = player.GetComponent<Animator>();

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
    }
	
	// Update is called once per frame
	void Update () {
        if (isWalk)
        {
            player.transform.position += speed*(this.transform.position - parentV) * Time.deltaTime;
            angle = Vector3.Angle(dir, new Vector3(1, 0, 0));
            if (this.transform.localPosition.y > 0 && angle > 45 && angle < 135)
            {
                animator.SetInteger("Walk", 0);//上
            }
            else if (angle >= 0 && angle <= 45)
            {
                animator.SetInteger("Walk", 1);//右
            }
            else if (this.transform.localPosition.y < 0 && angle > 45 && angle < 135)
            {
                animator.SetInteger("Walk", 2);//下
            }
            else if (angle >= 135 && angle <= 180)
            {
                animator.SetInteger("Walk", 3);//左
            }
        }
        else {
            animator.SetInteger("Walk",4);
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
}
