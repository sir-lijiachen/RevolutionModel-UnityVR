using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 碰撞器
/// </summary>
public class TouchRange : MonoBehaviour
{
    public MenuBar menuBar;

    private void Start()
    {

    }
    void OnTriggerEnter(Collider trigger)
    {
        Debug.Log("发生了碰撞");
        ScreenSwitch(true);
    }
    void OnTriggerExit(Collider trigger)
    {
        Debug.Log("离开");
        ScreenSwitch(false);
    }

    private void ScreenSwitch(bool isActive)
    {
        menuBar.MenuScreenSwitch(isActive);
        menuBar.TextScreenSwitch(isActive);
        menuBar.VideoScreenSwitch(isActive);
    }

}
