/*摇杆控制*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rocker : MonoBehaviour {
    private Vector3 parentV;
    private float r;
    public GameObject player;
    private bool isWalk;
    Vector3 dir;
    // Use this for initialization
    void Start () {
        parentV = this.GetComponentInParent<RectTransform>().position;
        //r = Vector3.Distance(this.transform.position,parentV);
        r = this.GetComponentInParent<RectTransform>().sizeDelta.x;

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
            player.transform.position += 2*(this.transform.position - parentV) * Time.deltaTime;
        }
	}

    private void OnpointerDrag(PointerEventData data) {
        if (Vector3.Distance(data.position, parentV) < r)
        {
            this.transform.position = data.position;
        }
        else {
            dir = Input.mousePosition - parentV;
            transform.position = parentV + dir.normalized * r;
        }
        isWalk = true;
    }

    private void OnpointerDragEnd(PointerEventData data) {
        this.transform.position = parentV;
        isWalk = false;
    }
}
