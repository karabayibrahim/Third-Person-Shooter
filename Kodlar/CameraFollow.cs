using DitzeGames.MobileJoystick;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform PlayerTransform;
    public TouchField Alan;
    public TouchField Alan2;
    private Vector3 cameraOffset;

    [Range(0.01f,1.0f)]
    public float smooth = 0.5f;
   public float CameraAngel;
    public float CameraPosy;
    

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerTransform = player.transform;
        transform.position = PlayerTransform.position + Quaternion.AngleAxis(CameraAngel, Vector3.up) * new Vector3(0, 3, -5.48f);
    }
    //void Start()
    //{
    //    GameObject player = GameObject.FindGameObjectWithTag("Player");
    //    PlayerTransform = player.transform;
    //    cameraOffset = transform.position - PlayerTransform.position;
        
        

    //}

    // Update is called once per frame
    void LateUpdate()
    {
        //Vector3 newPos = PlayerTransform.position + cameraOffset;
        CameraAngel += Alan.TouchDist.x * 0.1f;
        CameraPosy = Mathf.Clamp(CameraPosy-Alan.TouchDist.y * 0.01f, 0, 5f);

        //transform.position = Vector3.Slerp(transform.position , newPos , smooth);
        if (FindObjectOfType<JoyPlayerMovement>().ShootAktif)
        {
            float SapmaX = Random.Range(-0.01f, 0.05f);//SpraySistemi Oluşturduk
            float SapmaY = -0.001f;
            
            transform.position = PlayerTransform.position + Quaternion.AngleAxis(CameraAngel, Vector3.up) * new Vector3(0+SapmaX, CameraPosy+=SapmaY, -3.0f);
        }
        else
        {
            transform.position = PlayerTransform.position + Quaternion.AngleAxis(CameraAngel, Vector3.up) * new Vector3(0, CameraPosy, -3.0f);
        }
        



        transform.rotation = Quaternion.LookRotation(PlayerTransform.position + Vector3.up * 2f - transform.position, Vector3.up);
        //Debug.Log("pOS"+CameraPosy);

    }
}
