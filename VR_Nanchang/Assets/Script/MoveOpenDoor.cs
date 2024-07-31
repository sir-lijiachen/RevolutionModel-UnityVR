using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Valve.VR;
/// <summary>
/// �ƶ�������
/// </summary>
public class MoveOpenDoor : MonoBehaviour
{
    [SerializeField] public ReticlePoser reticlePoser;
    [SerializeField] private SteamVR_Action_Boolean togglePointerAction; // ���

    [SerializeField] private Button doorButton;//�Ű�ť
    [SerializeField] private GameObject vfxObject;//���尴ť
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;
    private bool doorSwitch = true;//������

    private void Awake()
    {
        doorButton = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Button>();
        vfxObject = transform.GetChild(0).GetChild(1).gameObject;
        leftDoor = transform.GetChild(1).gameObject;
        rightDoor = transform.GetChild(2).gameObject;
    }
    private void Start()
    {
        doorButton.onClick.AddListener(DoorSwitch);//ui���Ű�ť
    }
    void Update()
    {
        //������
        if(reticlePoser.hitTarget.name == vfxObject.name)
        {
            if (togglePointerAction.GetStateDown(SteamVR_Input_Sources.Any))
            {
                DoorSwitch();
            }
        }
    }
    private void DoorSwitch()
    {
        if (doorSwitch)
        {
            //����
            leftDoor.transform.DOMoveZ(3, 1f);
            rightDoor.transform.DOMoveZ(-3, 1f);
            doorButton.transform.DOLocalMoveX(2, 1f);

            doorSwitch = false;
        }
        else
        {
            //����
            leftDoor.transform.DOMoveZ(1, 1f);
            rightDoor.transform.DOMoveZ(-1, 1f);
            doorButton.transform.DOLocalMoveX(0, 1f);
            doorSwitch = true;
        }
    }

}
