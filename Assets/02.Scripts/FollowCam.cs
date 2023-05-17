using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{

    public Transform targetTr;
    private Transform camTr;

    [Range(2.0f, 20.0f)]
    public float distance = 10.0f;

    [Range(0.0f, 10.0f)]
    public float height = 2.0f;
    // Start is called before the first frame update

    public float damping = 10.0f;

    //Camera LookAt의 Offset값
    public float targetOffset = 2.0f;
    
    void Start()
    {
        camTr = GetComponent<Transform>();
           
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = targetTr.position
                     + (-targetTr.forward *distance)
                     + (Vector3.up * height);

        //선형 보간 함수사용 부드럽게 위치변경
        camTr.position = Vector3.Slerp(camTr.position, pos, Time.deltaTime * damping);


        camTr.LookAt(targetTr.position + (targetTr.up * targetOffset));
    }
}