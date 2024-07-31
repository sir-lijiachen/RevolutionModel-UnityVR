using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

/// <summary>
/// 机械臂操控+圆盘旋转+Plane移动显示模型
/// </summary>
public class MechanicalArm : MonoBehaviour
{
    public GameObject[] objects; // 圆周上的物体数组
    public float radius = 5f;    // 圆的半径
    public float duration = 3f;  // 动画持续时间
    private float currentRotation = 0f; // 当前旋转角度

    public Transform planeMoveTran;//Plane控制

    private void Awake()
    {
        int childCount = transform.GetChild(1).childCount;
        objects = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            // 存放物体
            objects[i] = transform.GetChild(1).GetChild(i).gameObject;
        }
    }
    void Start()
    {
        StartCoroutine(HandleStartSequence());
    }

    // 协程：处理启动顺序
    private IEnumerator HandleStartSequence()
    {
        if (planeMoveTran.position.z == 10)
        {
            DisappearModel();
            yield return new WaitForSeconds(1f); // 等待1秒，确保动画完成
        }
        ArrangeObjectsInCircle(); // 在动画结束后将物体排列成圆形
    }

    // 将物体按圆形排列
    public void ArrangeObjectsInCircle()
    {
        int count = objects.Length; // 获取物体数量
        float angleStep = 360f / count; // 计算每个物体之间的角度步长

        for (int i = 0; i < count; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad; // 计算物体的弧度角度
            Vector3 position = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius); // 根据角度计算物体的位置
            objects[i].transform.position = position; // 设置物体的位置
            objects[i].transform.LookAt(Vector3.zero); // 使物体朝向圆心
        }
    }

    // 设置新的半径并重新排列物体
    public void SetRadius(float newRadius)
    {
        radius = newRadius; // 更新半径
        ArrangeObjectsInCircle(); // 重新排列物体
    }

    // 移动指定索引的物体到前方
    public void MoveToFront(int index)
    {
        StartCoroutine(MoveObjectToFront(index)); // 启动协程移动物体
    }

    // 协程：移动物体到前方
    private IEnumerator MoveObjectToFront(int index)
    {
        float elapsed = 0f; // 已经过的时间
        float currentAngle = GetCurrentAngle(index); // 获取当前物体的角度
        float targetAngle = 270f; // 目标角度为270度

        float startRotation = currentRotation; // 起始旋转角度
        float endRotation = startRotation - (currentAngle - targetAngle); // 计算结束旋转角度

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime; // 增加已经过的时间
            float t = elapsed / duration; // 计算插值因子

            currentRotation = Mathf.Lerp(startRotation, endRotation, t); // 线性插值计算当前旋转角度
            UpdateObjectPositions(); // 更新物体位置

            yield return null; // 等待下一帧
        }

        // 确保所有物体都在正确的位置
        currentRotation = endRotation; // 设置最终的旋转角度
        UpdateObjectPositions(); // 更新物体位置

        // 动画结束后运行AppearModel方法
        AppearModel();
    }

    // 获取指定物体当前的角度
    private float GetCurrentAngle(int index)
    {
        Vector3 direction = objects[index].transform.position - Vector3.zero; // 计算物体到圆心的方向
        return Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg; // 计算角度并转换为度数
    }

    // 更新所有物体的位置
    private void UpdateObjectPositions()
    {
        int count = objects.Length; // 获取物体数量
        float angleStep = 360f / count; // 计算每个物体之间的角度步长

        for (int i = 0; i < count; i++)
        {
            float newAngle = currentRotation + i * angleStep; // 计算新角度
            float radian = newAngle * Mathf.Deg2Rad; // 转换为弧度
            Vector3 position = new Vector3(Mathf.Cos(radian) * radius, 0, Mathf.Sin(radian) * radius); // 根据新角度计算新位置
            objects[i].transform.position = position; // 设置物体的新位置
            objects[i].transform.LookAt(Vector3.zero); // 使物体朝向圆心
        }
    }


    private void AppearModel()
    {
        /*添加机械臂出现*/
        planeMoveTran.DOMoveZ(-10, 1f);
    }
    private void DisappearModel()
    {
        /*添加机械臂消失*/
        planeMoveTran.DOMoveZ(10, 1f);
    }
}
