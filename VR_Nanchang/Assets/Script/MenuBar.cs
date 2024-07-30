using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// �˵���
/// </summary>
public class MenuBar : MonoBehaviour
{
    public MechanicalArm mechanicalArm;

    private Transform buttonListTran;

    public GameObject menuScreenObject;//�˵���Ļ
    public GameObject textScreenObject;//������Ļ
    public GameObject videoScreenObject;//��Ƶ��Ļ

    public Button videoButton;
    public VideoPlayer videoPlay;
    public Button videoExit;

    float angle = 30f; // ��ת�Ƕ�
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
        //��Ļλ�ü���
        Vector3 imageSize = new Vector3(1920 * 0.003f, 1080 * 0.003f, 1);
        //����ƫ��λ��
        float radians = angle * Mathf.Deg2Rad; // ת��Ϊ����
        float xOffset = (imageSize.x / 2) * Mathf.Cos(radians) + (imageSize.x / 2);
        float zOffset = (imageSize.x / 2) * Mathf.Sin(radians);
        //��λ��
        textScreenObject.transform.rotation = Quaternion.Euler(0, -30, 0);
        videoScreenObject.transform.rotation = Quaternion.Euler(0, 30, 0);
        textScreenObject.transform.position = new Vector3(-xOffset, 0, -zOffset);
        videoScreenObject.transform.position = new Vector3(xOffset, 0, -zOffset);
    }

    //�����ť���ж���
    private void ClickButton(int num)
    {
        mechanicalArm.MoveToFront(num);//ת��
        MenuScreenSwitch(false);//�رղ˵���Ļ
        TextScreen(num);


        /*֪ʶ�����֣����а�ť*/
    }
    //������Ļ
    private void TextScreen(int num)
    {
        //������Ľ���
        videoButton.onClick.AddListener(PlayVideo);
    }

    //������Ƶ
    private void PlayVideo()
    {
        VideoScreenSwitch(true);
        VideoClip video = Resources.Load<VideoClip>($"Video/0");
        videoPlay.clip = video;
        videoPlay.Play();
        videoExit.onClick.AddListener(CloseVideo);
    }
    //�˳���Ƶ
    private void CloseVideo()
    {
        videoPlay.Pause();
        VideoScreenSwitch(false);
        videoPlay.clip = null;
    }


    /// <summary>
    /// �˵������֡���Ƶ ��Ļ����
    /// </summary>
    public void MenuScreenSwitch(bool isActive)
    {
        menuScreenObject.SetActive(isActive);
    }
    public void TextScreenSwitch(bool isActive)
    {
        /*�������֡���ť*/
        textScreenObject.SetActive(isActive);
    }
    public void VideoScreenSwitch(bool isActive)
    {
        /*�����ͣ��Ƶ/ɾ��clip */
        videoScreenObject.SetActive(isActive);
    }
}
