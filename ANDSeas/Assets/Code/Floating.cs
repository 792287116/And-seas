﻿using UnityEngine;
using System.Collections;
// 主界面的开始按钮使用该脚本，控制上下来回浮动  
public class Floating : MonoBehaviour
{
    float radian = 0; // 弧度  
    float perRadian = 0.03f; // 每次变化的弧度  
    float radius = 0.8f; // 半径  
    Vector3 oldPos; // 开始时候的坐标  
    Vector3 parentVec;
    // Use this for initialization  
    void Start()
    {
        radian = Random.Range(0,10);
        parentVec = this.transform.parent.position;
        oldPos = transform.position; // 将最初的位置保存到oldPos  
    }

    // Update is called once per frame  
    void Update()
    {
        if (this.transform.parent.position == parentVec)
        {
            radian += perRadian; // 弧度每次加0.03  
            float dy = Mathf.Cos(radian) * radius; // dy定义的是针对y轴的变量，也可以使用sin，找到一个适合的值就可以  
            transform.position = oldPos + new Vector3(0, dy, 0);
        }
    }
}