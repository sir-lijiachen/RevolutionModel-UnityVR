using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
/// <summary>
/// 移动开关门
/// </summary>
public class MoveOpenDoor : MonoBehaviour
{
    public Button doorButton;
    public GameObject leftDoor;
    public GameObject rightDoor;
    private bool doorSwitch = false;

    private void Awake()
    {
        doorButton = transform.GetChild(0).GetChild(0).GetComponent<Button>();
        leftDoor = transform.GetChild(1).gameObject;
        rightDoor = transform.GetChild(2).gameObject;
    }
    private void Start()
    {
        doorButton.onClick.AddListener(DoorSwitch);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DoorSwitch();
        }
    }
    private void DoorSwitch()
    {
        if (doorSwitch)
        {
            leftDoor.transform.DOMoveX(-1, 1f);
            rightDoor.transform.DOMoveX(1, 1f);
            doorButton.transform.DOMoveX(0, 1f);
            doorSwitch = false;
        }
        else
        {
            leftDoor.transform.DOMoveX(-3, 1f);
            rightDoor.transform.DOMoveX(3, 1f);
            doorButton.transform.DOMoveX(2, 1f);
            doorSwitch = true;
        }
    }
}
