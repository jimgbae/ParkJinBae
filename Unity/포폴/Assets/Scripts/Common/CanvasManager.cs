using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    public GameObject Playcanvas;
    public static CanvasManager instance = null;


    //탄창 Image UI와 남은 총알 수 Text UI
    public Image magazineImg;
    public Text magazineText;
    //총기 Image
    public Image weaponImage;

    //BloodScreen 텍스처 저장 변수
    public Image bloodScreen;

    public Image hpBar;

    //EXP Text
    public Text ExpText;

    //Stat
    public Text HP;
    public Text Speed;
    public Text Damage;
    public Text Stat;
    public Text STR;
    public Text DEX;
    public Text CON;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(Playcanvas);
    }

    public void OnCanvas()
    {
        Playcanvas.SetActive(true);
    }

    public void OffCanvas()
    {
        Playcanvas.SetActive(false);
    }
}
