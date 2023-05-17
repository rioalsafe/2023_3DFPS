using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class BarrelCtrl : MonoBehaviour
{
    //폭팔 파티클을 참조할 변수
    public GameObject expEffect;
    //무작위로 적용할 텍스쳐 배열.
    public Texture[] textures;

    public float radius = 10.0f;
    //하위에 있는 Mesh Renderer
    private new MeshRenderer renderer;

    //컴포넌트를 참조할 변수
    private Transform tr;
    private Rigidbody rb;

    //총알 맞은 횟수를 누적시키는 변수
    private int hitCount = 0;
    
    //오디오 변수
        public AudioClip fireSfx;
        public new AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        audio = GetComponent<AudioSource>();  

        //하위에 있는 Mesh Renderer 컴포넌트 참조
        renderer = GetComponentInChildren<MeshRenderer>();

        //난수 발생
        int idx = Random.Range(0, textures.Length);
        //텍스쳐 저장.
        renderer.material.mainTexture = textures[idx];

    }

    //충돌시 호출되는 콜백 함수
    void OnCollisionEnter(Collision coll) 
    {
        if(coll.collider.CompareTag("BULLET"))
        {
            //총알 맞은 횟수를 증가 시키고 3회이상이면 폭팔
            if(++hitCount == 3)
            {
                ExpBarrel();
            }
        }
    }

    void ExpBarrel()
    {
        //폭팔 효과 파티클 생성
        GameObject exp = Instantiate(expEffect, tr.position, Quaternion.identity);
        //폭팔 효과 파이클 5초 후에 제거
        Destroy(exp, 2.0f);

        //Rigidbody 컴포넌트의 무게 mass를 1.0으로 수정해 무게를 가볍게 함
        rb.mass = 1.0f;

        //위로 솟구치는 힘을 가함.
        rb.AddForce(Vector3.up * 1500.0f);

        IndirectDamage(tr.position);

        //3초 후 드럼통제거
        Destroy(gameObject, 3.0f);
        

    }

    void IndirectDamage(Vector3 pos)
    {
        Collider[] colls = Physics.OverlapSphere(pos, radius, 1<<3);

        foreach (Collider coll in colls)
        {
            if(coll)
            {
                rb = coll.GetComponent<Rigidbody>();
                rb.mass = 1.0f;
                rb.constraints = RigidbodyConstraints.None;

                rb.AddExplosionForce(1500.0f, pos, radius, 1200.0f);
                
                audio.PlayOneShot(fireSfx);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
