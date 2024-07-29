using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text001 : MonoBehaviour
{
    public GameObject textScreenObject;//������Ļ
    public GameObject videoScreenObject;//������Ļ
    float angle = 30f; // ��ת�Ƕ�
    private void Start()
    {
        //��Ļλ�ü���
        Vector3 imageSize = new Vector3(1920 * 0.003f, 1080 * 0.003f, 1);

        float radians = angle * Mathf.Deg2Rad; // ת��Ϊ����
        Debug.Log(radians);
        // ����ͼƬ��������ԭ�㣬��Ҫ���������Եλ��
        float xOffset = (imageSize.x / 2) * Mathf.Cos(radians) + (imageSize.x / 2);
        float zOffset = (imageSize.x / 2) * Mathf.Sin(radians);

        Debug.Log("xOffset: " + xOffset); // ��� xOffset ����
        Debug.Log("zOffset: " + zOffset); // ��� zOffset ����
        // ����ͼƬ2��λ�úʹ�С
        textScreenObject.transform.position = new Vector3(-xOffset, 0, -zOffset);
        videoScreenObject.transform.position = new Vector3(xOffset, 0, -zOffset);
    }
}
