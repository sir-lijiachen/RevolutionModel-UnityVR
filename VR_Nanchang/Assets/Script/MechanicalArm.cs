using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

/// <summary>
/// ��е�۲ٿ�+Բ����ת+Plane�ƶ���ʾģ��
/// </summary>
public class MechanicalArm : MonoBehaviour
{
    public GameObject[] objects; // Բ���ϵ���������
    public float radius = 5f;    // Բ�İ뾶
    public float duration = 3f;  // ��������ʱ��
    private float currentRotation = 0f; // ��ǰ��ת�Ƕ�

    public Transform planeMoveTran;//Plane����

    private void Awake()
    {
        int childCount = transform.GetChild(1).childCount;
        objects = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            // �������
            objects[i] = transform.GetChild(1).GetChild(i).gameObject;
        }
    }
    void Start()
    {
        StartCoroutine(HandleStartSequence());
    }

    // Э�̣���������˳��
    private IEnumerator HandleStartSequence()
    {
        if (planeMoveTran.position.z == 10)
        {
            DisappearModel();
            yield return new WaitForSeconds(1f); // �ȴ�1�룬ȷ���������
        }
        ArrangeObjectsInCircle(); // �ڶ����������������г�Բ��
    }

    // �����尴Բ������
    public void ArrangeObjectsInCircle()
    {
        int count = objects.Length; // ��ȡ��������
        float angleStep = 360f / count; // ����ÿ������֮��ĽǶȲ���

        for (int i = 0; i < count; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad; // ��������Ļ��ȽǶ�
            Vector3 position = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius); // ���ݽǶȼ��������λ��
            objects[i].transform.position = position; // ���������λ��
            objects[i].transform.LookAt(Vector3.zero); // ʹ���峯��Բ��
        }
    }

    // �����µİ뾶��������������
    public void SetRadius(float newRadius)
    {
        radius = newRadius; // ���°뾶
        ArrangeObjectsInCircle(); // ������������
    }

    // �ƶ�ָ�����������嵽ǰ��
    public void MoveToFront(int index)
    {
        StartCoroutine(MoveObjectToFront(index)); // ����Э���ƶ�����
    }

    // Э�̣��ƶ����嵽ǰ��
    private IEnumerator MoveObjectToFront(int index)
    {
        float elapsed = 0f; // �Ѿ�����ʱ��
        float currentAngle = GetCurrentAngle(index); // ��ȡ��ǰ����ĽǶ�
        float targetAngle = 270f; // Ŀ��Ƕ�Ϊ270��

        float startRotation = currentRotation; // ��ʼ��ת�Ƕ�
        float endRotation = startRotation - (currentAngle - targetAngle); // ���������ת�Ƕ�

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime; // �����Ѿ�����ʱ��
            float t = elapsed / duration; // �����ֵ����

            currentRotation = Mathf.Lerp(startRotation, endRotation, t); // ���Բ�ֵ���㵱ǰ��ת�Ƕ�
            UpdateObjectPositions(); // ��������λ��

            yield return null; // �ȴ���һ֡
        }

        // ȷ���������嶼����ȷ��λ��
        currentRotation = endRotation; // �������յ���ת�Ƕ�
        UpdateObjectPositions(); // ��������λ��

        // ��������������AppearModel����
        AppearModel();
    }

    // ��ȡָ�����嵱ǰ�ĽǶ�
    private float GetCurrentAngle(int index)
    {
        Vector3 direction = objects[index].transform.position - Vector3.zero; // �������嵽Բ�ĵķ���
        return Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg; // ����ǶȲ�ת��Ϊ����
    }

    // �������������λ��
    private void UpdateObjectPositions()
    {
        int count = objects.Length; // ��ȡ��������
        float angleStep = 360f / count; // ����ÿ������֮��ĽǶȲ���

        for (int i = 0; i < count; i++)
        {
            float newAngle = currentRotation + i * angleStep; // �����½Ƕ�
            float radian = newAngle * Mathf.Deg2Rad; // ת��Ϊ����
            Vector3 position = new Vector3(Mathf.Cos(radian) * radius, 0, Mathf.Sin(radian) * radius); // �����½Ƕȼ�����λ��
            objects[i].transform.position = position; // �����������λ��
            objects[i].transform.LookAt(Vector3.zero); // ʹ���峯��Բ��
        }
    }


    private void AppearModel()
    {
        /*��ӻ�е�۳���*/
        planeMoveTran.DOMoveZ(-10, 1f);
    }
    private void DisappearModel()
    {
        /*��ӻ�е����ʧ*/
        planeMoveTran.DOMoveZ(10, 1f);
    }
}
