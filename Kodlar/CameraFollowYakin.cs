using DitzeGames.MobileJoystick;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowYakin : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform PlayerTransform;
    public TouchField Alan;
    public TouchField Alan2;

    private Vector3 cameraOffset;

    [Range(0.01f, 1.0f)]
    public float smooth = 0.5f;
    public float CameraAngel;
    public float CameraPosy;
    

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerTransform = player.transform;
        cameraOffset = transform.position - PlayerTransform.position;
        



    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Vector3 newPos = PlayerTransform.position + cameraOffset;
        CameraAngel += Alan.TouchDist.x * 0.1f;
        CameraPosy = Mathf.Clamp(CameraPosy - Alan.TouchDist.y * 0.01f, 0, 5f);
        
        //transform.position = Vector3.Slerp(transform.position , newPos , smooth);
        //if (CameraPosy == 0)
        //{
        //    transform.position = PlayerTransform.position + Quaternion.AngleAxis(CameraAngel, Vector3.up) * new Vector3(0, 3, -2.1f);
        //}
        //else
        //{

        //}
        transform.position = PlayerTransform.position + Quaternion.AngleAxis(CameraAngel, Vector3.up) * new Vector3(0, CameraPosy, -2.1f);
        

        transform.rotation = Quaternion.LookRotation(PlayerTransform.position + Vector3.up * 2f - transform.position, Vector3.up);

    }
}

