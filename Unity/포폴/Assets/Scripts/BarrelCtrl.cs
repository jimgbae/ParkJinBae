using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    //폭발효과 프리팹 변수
    public GameObject expEffect;
    //드럼통 메쉬 저장 배열
    public Mesh[] meshes;
    // 드럼통 텍스처 저장 배열
    public Texture[] textures;

    //폭발 반경
    public float expRadius = 10.0f;


    //총알 맞은 횟수
    private int hitCount = 0;
    //Rigidbody
    private Rigidbody rigid;
    //MeshFilter 컴포넌트 저장 변수
    private MeshFilter meshFilter;
    //MeshRenderer 컴포넌트 저장 변수
    private MeshRenderer _renderer;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        meshFilter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material.mainTexture = textures[Random.Range(0, textures.Length)];
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            if (++hitCount == 3)
                ExpBarrel();
        }
    }

    void ExpBarrel()
    {
        //폭발 프리팹 생성
        GameObject effect = Instantiate(expEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2.0f);
        //무게 1.0f로 설정
        rigid.mass = 1.0f;

        //폭발력 생성
        IndirectDamage(transform.position);

        int idx = Random.Range(0, meshes.Length);
        meshFilter.sharedMesh = meshes[idx];
    }

    void IndirectDamage(Vector3 pos)
    {
        Collider[] colls = Physics.OverlapSphere(pos, expRadius, 1 << 8);
        foreach(var coll in colls)
        {
            var _rb = coll.GetComponent<Rigidbody>();
            _rb.mass = 1.0f;
            _rb.AddExplosionForce(1200.0f, pos, expRadius, 1000.0f);
        }
    }
}
