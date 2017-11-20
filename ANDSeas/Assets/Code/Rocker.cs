/*摇杆控制*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System;

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

    KeyCode currentKey;

    public float jumpSpeed = 10;

    public float gravity = 20;

    public float margin = 0.1f;

    private Vector3 moveDirection = Vector3.zero;

    private Collider2D controller;

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
    void Update() {
        playerX = speed * (this.transform.position.x - parentV.x) * Time.deltaTime;
        if (isWalk)
        {
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

        //检测按键名字
        //if (Input.anyKeyDown)
        //{
        //    foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
        //    {
        //        if (Input.GetKeyDown(keyCode))
        //        {
        //            Debug.Log(keyCode.ToString());
        //        }
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            Debug.Log("A");
            OpenFire();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            Debug.Log("B");
            Jump();
        }

        var inputX = Input.GetAxis("Horizontal");
        var inputY = Input.GetAxis("Vertical");
        if (inputX != 0 || inputY != 0)
        {
            this.transform.localPosition = new Vector3(inputX * parentV.x / 2, inputY * parentV.y / 2, 0);
            player.transform.position += new Vector3(playerX, player.transform.position.y, 0);
        }
        if (inputX == 0 && inputY == 0)
        {
            this.transform.localPosition = Vector3.zero;
        }

        if (Physics.Raycast(transform.position, -Vector3.up, margin))
        {
            // 空格键控制跳跃  
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.(moveDirection * Time.deltaTime);
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
        
    }
    //发射子弹
    public void OpenFire() {
        Vector3 vec = player.transform.GetChild(0).transform.position;
         Instantiate(bullet, vec, bulletQuaternion);
    }
}
