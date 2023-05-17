using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{

    private Transform tr;
    public float moveSpeed = 10.0f;
    public float turnSpeed = 80.0f;

    private Animation anim;

    IEnumerator Start()
    {
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();
        anim.Play("Idle");

        turnSpeed = 0.0f;
        yield return new WaitForSeconds(0.3f);
        turnSpeed = 80.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        //transform.position += new Vector3(0,0,0.1f);
        //transform.position += Vector3.forward * 0.1f;
        //.position += Vector3.forward * 0.1f;

        tr.Translate(moveDir.normalized * Time.deltaTime * moveSpeed);

        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r);

        PlayAnim(h, v);
    }

    void PlayAnim(float h, float v)
    {
        if( v >= 0.1f)
        {
            anim.CrossFade("RunF", 0.25f);
        }
        else if( v<= -0.1f)
        {
            anim.CrossFade("RunB", 0.25f);
        }
        else if(h>= 0.1f)
        {
            anim.CrossFade("RunR", 0.25f);
        }
        else if(h<= -0.1f)
        {
            anim.CrossFade("RunL", 0.25f);
        }
        else
        {
            anim.CrossFade("Idle", 0.25f);
        }
    }
}
