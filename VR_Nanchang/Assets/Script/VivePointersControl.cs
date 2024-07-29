using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
/// <summary>
/// ���߿���
/// </summary>
public class VivePointersControl : MonoBehaviour
{
    // VivePointer����ֶ�
    [SerializeField] private GameObject vivePointer_left; // VivePointer���
    [SerializeField] private GameObject vivePointer_right; // VivePointer���
    [SerializeField] private SteamVR_Action_Boolean togglePointerAction; // �л����ߵİ�ť����
    private bool isPointerActive = true; // �����Ƿ񼤻�
    private void Update()
    {
        TogglePointer();// �л����߷���״̬
    }

    // �л����߷���״̬
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
