using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public Transform shakeCamera;
    public bool shakeRotate = false;

    private Vector3 originPos;
    private Quaternion originRot;

    void Start()
    {
        originPos = shakeCamera.localPosition;
        originRot = shakeCamera.localRotation;
    }

    public IEnumerator ShakeCamera(float duration = 0.05f, float magniudePos = 0.03f, float magnitudeRot = 0.1f)
    {
        //지난 시간 누적 변수
        float passTime = 0.0f;

        //진동 시간 동안 루프 순회
        while(passTime < duration)
        {
            //불규칙 위치 산출
            Vector3 shakePos = Random.insideUnitSphere;
            //카메라 위치 변경
            shakeCamera.localPosition = shakePos * magniudePos;

            //불규칙한 회전 사용할 경우
            if(shakeRotate)
            {
                //불규칙한 회전값을 펄린 노이즈 함수를 이용해 추출
                Vector3 shakeRot = new Vector3(0, 0, Mathf.PerlinNoise(Time.time * magnitudeRot, 0.0f));
                //카메라 회전값 변경
                shakeCamera.localRotation = Quaternion.Euler(shakeRot);
            }
            passTime += Time.deltaTime;

            yield return null;
        }

        shakeCamera.localPosition = originPos;
        shakeCamera.localRotation = originRot;
    }
}
