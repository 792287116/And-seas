/*摇杆控制*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class Rocker : MonoBehaviour {
    private float speed = 10;
    private Vector3 parentV;//摇杆背景坐标
    private Vector3 clientV;
    private float r;//摇杆移动半径
    public GameObject player;
    private bool isWalk;
    Vector3 dir;
    //private Animator animator;
    float angle;
    private float playerX;
    private float playerY = 0;
    private Sequence JumpT;
    private bool isJump = false;
    private bool isCanJump = false;

    public GameObject bullet;
    Quaternion bulletQuaternion;
    public Vector3 vec3;

    KeyCode currentKey;

    private float jumpSpeed = 2f;

    public float gravity = 20;

    private float margin = 0.1f;

    private Vector2 moveDirection = Vector2.zero;

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
        
        if (isWalk)
        {
            playerX = speed * (this.transform.position.x - parentV.x) * Time.deltaTime;
            angle = Vector3.Angle(dir, new Vector3(1, 0, 0));
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(playerX,0);
        }
        if (isJump)
        {
            playerY = jumpSpeed;
            jumpSpeed -= gravity * Time.deltaTime;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, playerY);
        }
        if (Physics2D.Raycast(player.transform.position - new Vector3(0, 247 / 2, 0), -Vector2.up, 0.1f))
        {
            isCanJump = true;
        }
        else
        {
            isCanJump = false;
        }
        var inputX = Input.GetAxis("Horizontal");
        var inputY = Input.GetAxis("Vertical");
        if (inputX != 0 || inputY != 0)
        {
            this.transform.localPosition = new Vector3(inputX * parentV.x / 2, inputY * parentV.y / 2, 0);
            isWalk = true;
        }
        if (inputX == 0 && inputY == 0 && this.transform.localPosition != Vector3.zero)
        {
            this.transform.localPosition = Vector3.zero;
            isWalk = false;
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
            Debug.Log("手柄A");
            OpenFire();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            Debug.Log("手柄B");
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            OpenFire();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Jump();
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
        if (Physics.Raycast(player.transform.position - new Vector3(0, 247 / 2, 0), -Vector3.up, 0.1f))
        {
            isJump = true;
            isWalk = true;
            jumpSpeed = 10;
        }
    }

    public void OpenFire() {
        Vector3 vec = player.transform.position;
        if (this.transform.localPosition != Vector3.zero)
        {
            GameObject bullet1 = Instantiate(bullet, vec, bulletQuaternion);
            bullet1.GetComponent<Bullet>().Fir(1000, (this.transform.position - parentV).normalized);
        }
        
    }
}
