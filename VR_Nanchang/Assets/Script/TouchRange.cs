using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ײ��
/// </summary>
public class TouchRange : MonoBehaviour
{
    public MenuBar menuBar;

    private void Start()
    {

    }
    void OnTriggerEnter(Collider trigger)
    {
        Debug.Log("��������ײ");
        ScreenSwitch(true);
    }
    void OnTriggerExit(Collider trigger)
    {
        Debug.Log("�뿪");
        ScreenSwitch(false);
    }

    private void ScreenSwitch(bool isActive)
    {
        menuBar.MenuScreenSwitch(isActive);
        menuBar.TextScreenSwitch(isActive);
        menuBar.VideoScreenSwitch(isActive);
    }

}
