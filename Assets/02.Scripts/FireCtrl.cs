using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//반드시 필요한 컴포넌트를 명시해 컴포넌트가 삭제되는것을 방지하는 Attribute
[RequireComponent(typeof(AudioSource))]

public class FireCtrl : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;

    public AudioClip fireSfx;
    public new AudioSource audio;

    private MeshRenderer muzzleFlash;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();  

        muzzleFlash = firePos.GetComponentInChildren<MeshRenderer>();

        muzzleFlash.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        Instantiate(bullet, firePos.position, firePos.rotation);
        audio.PlayOneShot(fireSfx);
        
        //총구 화염 효과 코루틴 함수 호출
        StartCoroutine(ShowMuzzleFlash());

    }

    IEnumerator ShowMuzzleFlash() 
    {
        //오프셋 좌표값을 랜덤함수로 생성.
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;
        muzzleFlash.material.mainTextureOffset = offset;

        //MuzzleFlash 회전 변경.
        float angle = Random.Range(0, 360);
        muzzleFlash.transform.localRotation = Quaternion.Euler(0,0,angle);

        //MuzzleFlash 크기 조절.
        float scale = Random.Range(1.0f, 2.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale; // 1,1,1 스케일 * scale.


         muzzleFlash.enabled=true;

        yield return new WaitForSeconds(0.2f);

        muzzleFlash.enabled = false;

    }
}
