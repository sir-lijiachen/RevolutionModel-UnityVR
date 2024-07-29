using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text001 : MonoBehaviour
{
    public GameObject textScreenObject;//文字屏幕
    public GameObject videoScreenObject;//文字屏幕
    float angle = 30f; // 旋转角度
    private void Start()
    {
        //屏幕位置计算
        Vector3 imageSize = new Vector3(1920 * 0.003f, 1080 * 0.003f, 1);

        float radians = angle * Mathf.Deg2Rad; // 转换为弧度
        Debug.Log(radians);
        // 假设图片的中心在原点，需要调整到其边缘位置
        float xOffset = (imageSize.x / 2) * Mathf.Cos(radians) + (imageSize.x / 2);
        float zOffset = (imageSize.x / 2) * Mathf.Sin(radians);

        Debug.Log("xOffset: " + xOffset); // 检查 xOffset 计算
        Debug.Log("zOffset: " + zOffset); // 检查 zOffset 计算
        // 设置图片2的位置和大小
        textScreenObject.transform.position = new Vector3(-xOffset, 0, -zOffset);
        videoScreenObject.transform.position = new Vector3(xOffset, 0, -zOffset);
    }
}
