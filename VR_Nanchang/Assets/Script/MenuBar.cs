using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// 菜单栏
/// </summary>
public class MenuBar : MonoBehaviour
{
    public MechanicalArm mechanicalArm;

    private Transform buttonListTran;

    public GameObject menuScreenObject;//菜单屏幕
    public GameObject textScreenObject;//文字屏幕
    public GameObject videoScreenObject;//视频屏幕

    public Button videoButton;
    public VideoPlayer videoPlay;
    public Button videoExit;

    float angle = 30f; // 旋转角度
    private void Awake()
    {
        buttonListTran = transform.GetChild(0).GetChild(0).GetChild(0);
        for (int i = 0; i < buttonListTran.childCount; i++)
        {
            Button btn = buttonListTran.GetChild(i).GetComponent<Button>();
            int num = i;
            btn.onClick.AddListener(() => ClickButton(num));
        }
    }
    private void Start()
    {
        //屏幕位置计算
        Vector3 imageSize = new Vector3(1920 * 0.003f, 1080 * 0.003f, 1);
        //计算偏移位置
        float radians = angle * Mathf.Deg2Rad; // 转换为弧度
        float xOffset = (imageSize.x / 2) * Mathf.Cos(radians) + (imageSize.x / 2);
        float zOffset = (imageSize.x / 2) * Mathf.Sin(radians);
        //定位置
        textScreenObject.transform.rotation = Quaternion.Euler(0, -30, 0);
        videoScreenObject.transform.rotation = Quaternion.Euler(0, 30, 0);
        textScreenObject.transform.position = new Vector3(-xOffset, 0, -zOffset);
        videoScreenObject.transform.position = new Vector3(xOffset, 0, -zOffset);
    }

    //点击按钮进行动画
    private void ClickButton(int num)
    {
        mechanicalArm.MoveToFront(num);//转动
        MenuScreenSwitch(false);//关闭菜单屏幕
        TextScreen(num);


        /*知识屏出现，并有按钮*/
    }
    //文字屏幕
    private void TextScreen(int num)
    {
        //该物体的介绍
        videoButton.onClick.AddListener(PlayVideo);
    }

    //播放视频
    private void PlayVideo()
    {
        VideoScreenSwitch(true);
        VideoClip video = Resources.Load<VideoClip>($"Video/0");
        videoPlay.clip = video;
        videoPlay.Play();
        videoExit.onClick.AddListener(CloseVideo);
    }
    //退出视频
    private void CloseVideo()
    {
        videoPlay.Pause();
        VideoScreenSwitch(false);
        videoPlay.clip = null;
    }


    /// <summary>
    /// 菜单、文字、视频 屏幕开关
    /// </summary>
    public void MenuScreenSwitch(bool isActive)
    {
        menuScreenObject.SetActive(isActive);
    }
    public void TextScreenSwitch(bool isActive)
    {
        /*清理文字、按钮*/
        textScreenObject.SetActive(isActive);
    }
    public void VideoScreenSwitch(bool isActive)
    {
        /*添加暂停视频/删除clip */
        videoScreenObject.SetActive(isActive);
    }
}
