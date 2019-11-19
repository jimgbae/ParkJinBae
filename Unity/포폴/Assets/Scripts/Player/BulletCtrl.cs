using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCtrl : MonoBehaviour
{
    //총알 데미지 및 속도
    public float damage = 20.0f;
    public float speed = 1500.0f;
    public float lifeTime = 3.0f;
    public float _elapsedTime = 0.0f;

    //컴포넌트 저장 변수
    private Transform tr;
    private Rigidbody rigid;
    private TrailRenderer trail;

    private Text DamageText;


    void Awake()
    {
        tr = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();

        damage = GameManager.instance.gameData.damage;
    }

    void Start()
    {
        DamageText = CanvasManager.instance.Damage;
        UpdateDMGText();
    }

    void OnEnable()
    {
        rigid.AddForce(transform.forward * speed);
        GameManager.OnItemChange += UpdateSetup;
        GameManager.OnStatChange += UpdateSetup;
    }

    void UpdateSetup()
    {
        damage = GameManager.instance.gameData.damage;
    }

    void OnDisable()
    {
        trail.Clear();
        tr.position = Vector3.zero;
        tr.rotation = Quaternion.identity;
        rigid.Sleep();
    }

    void Update()
    {
        UpdateDMGText();
    }

    void UpdateDMGText()
    {
        DamageText.text = string.Format("{0}", damage);
    }
}
