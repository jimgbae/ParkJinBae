using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    //스파크 프리팹 변수
    public GameObject SparkEffect;

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "BULLET") 
        {
            ShowEffect(coll);
            coll.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "BULLET")
        {
            //총알 삭제
            coll.gameObject.SetActive(false);
        }
    }

    void ShowEffect(Collision coll)
    {
        ContactPoint contact = coll.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, contact.normal);

        GameObject spark = Instantiate(SparkEffect, contact.point + (-contact.normal* 0.05f), rot);
        spark.transform.SetParent(this.transform);
    }
}
