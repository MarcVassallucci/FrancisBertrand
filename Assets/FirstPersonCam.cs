using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    public float yawLimit = 40f;
    public float pitchLimitUp = -40f;
    public float pitchLimitDown = 80f;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private float initialYaw;
    private float initialPitch;

    void Start()
    {
        yaw = transform.rotation.eulerAngles.y;
        pitch = transform.rotation.eulerAngles.x;

        initialYaw = yaw;
        initialPitch = pitch;
    }

    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X") * Time.deltaTime;
        pitch -= speedV * Input.GetAxis("Mouse Y") * Time.deltaTime;

        yaw = Mathf.Clamp(yaw, initialYaw - yawLimit, initialYaw + yawLimit);
        pitch = Mathf.Clamp(pitch, initialPitch + pitchLimitDown, initialPitch + pitchLimitUp);
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}
