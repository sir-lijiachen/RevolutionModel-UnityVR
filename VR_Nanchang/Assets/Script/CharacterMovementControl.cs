using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
/// <summary>
/// VR的移动，绑定在摄像机或物体上
/// </summary>
public class CharacterMovementControl : MonoBehaviour
{
    // 移动操作绑定到SteamVR动作
    [SerializeField] private SteamVR_Action_Vector2 moveAction;
    // 移动速度
    [SerializeField] private float speed = 1;
    // 重力
    [SerializeField] private float gravity = 9.81f;
    // 角色最小高度
    [SerializeField] private float minHeight = 0;
    // 角色最大高度
    [SerializeField] private float maxHeight = float.PositiveInfinity;
    // 角色控制器
    [SerializeField] private CharacterController characterController;

    // 在脚本开始时初始化
    void Start()
    {
        // 如果未手动指定角色控制器组件，则自动获取
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }
    }

    // 每帧更新
    void Update()
    {
        // 处理角色高度
        HandleHeight();
        // 移动角色
        Move();
    }

    // 处理角色移动
    private void Move()
    {
        
        // 检查移动输入轴是否有显著的输入
        if (moveAction.axis.magnitude > 0.1f)
        {
            // 将头显的方向转化为移动方向
            Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(moveAction.axis.x, 0, moveAction.axis.y));
            // 移动角色，同时应用重力
            characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, gravity, 0) * Time.deltaTime);
        }
    }

    // 处理角色高度
    private void HandleHeight()
    {
        // 根据头显的位置调整角色的高度
        float headHeight = Mathf.Clamp(Player.instance.hmdTransform.position.y, minHeight, maxHeight);
        characterController.height = headHeight;

        // 调整角色控制器的中心位置，以与头显的位置对齐
        Vector3 newCenter = Player.instance.transform.InverseTransformPoint(Player.instance.hmdTransform.position);
        newCenter.y = characterController.height / 2 + characterController.skinWidth;
        characterController.center = newCenter;
    }
}
