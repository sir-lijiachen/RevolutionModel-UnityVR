using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
/// <summary>
/// 射线控制
/// </summary>
public class VivePointersControl : MonoBehaviour
{
    // VivePointer相关字段
    [SerializeField] private GameObject vivePointer_left; // VivePointer组件
    [SerializeField] private GameObject vivePointer_right; // VivePointer组件
    [SerializeField] private SteamVR_Action_Boolean togglePointerAction; // 切换射线的按钮动作
    private bool isPointerActive = true; // 射线是否激活
    private void Update()
    {
        TogglePointer();// 切换射线发射状态
    }

    // 切换射线发射状态
    private void TogglePointer()
    {
        if (togglePointerAction.GetStateDown(SteamVR_Input_Sources.Any))
        {
            isPointerActive = !isPointerActive;
            if (vivePointer_left != null)
            {
                vivePointer_left.SetActive(isPointerActive);
            }
            if (vivePointer_right != null)
            {
                vivePointer_right.SetActive(isPointerActive);
            }
        }
    }
}
