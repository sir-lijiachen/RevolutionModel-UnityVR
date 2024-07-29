using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
/// <summary>
/// VR���ƶ��������������������
/// </summary>
public class CharacterMovementControl : MonoBehaviour
{
    // �ƶ������󶨵�SteamVR����
    [SerializeField] private SteamVR_Action_Vector2 moveAction;
    // �ƶ��ٶ�
    [SerializeField] private float speed = 1;
    // ����
    [SerializeField] private float gravity = 9.81f;
    // ��ɫ��С�߶�
    [SerializeField] private float minHeight = 0;
    // ��ɫ���߶�
    [SerializeField] private float maxHeight = float.PositiveInfinity;
    // ��ɫ������
    [SerializeField] private CharacterController characterController;

    // �ڽű���ʼʱ��ʼ��
    void Start()
    {
        // ���δ�ֶ�ָ����ɫ��������������Զ���ȡ
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }
    }

    // ÿ֡����
    void Update()
    {
        // �����ɫ�߶�
        HandleHeight();
        // �ƶ���ɫ
        Move();
    }

    // �����ɫ�ƶ�
    private void Move()
    {
        
        // ����ƶ��������Ƿ�������������
        if (moveAction.axis.magnitude > 0.1f)
        {
            // ��ͷ�Եķ���ת��Ϊ�ƶ�����
            Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(moveAction.axis.x, 0, moveAction.axis.y));
            // �ƶ���ɫ��ͬʱӦ������
            characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, gravity, 0) * Time.deltaTime);
        }
    }

    // �����ɫ�߶�
    private void HandleHeight()
    {
        // ����ͷ�Ե�λ�õ�����ɫ�ĸ߶�
        float headHeight = Mathf.Clamp(Player.instance.hmdTransform.position.y, minHeight, maxHeight);
        characterController.height = headHeight;

        // ������ɫ������������λ�ã�����ͷ�Ե�λ�ö���
        Vector3 newCenter = Player.instance.transform.InverseTransformPoint(Player.instance.hmdTransform.position);
        newCenter.y = characterController.height / 2 + characterController.skinWidth;
        characterController.center = newCenter;
    }
}
