using System.Collections;
using System.Collections.Generic;

namespace DataInfo
{
    [System.Serializable]
    public class GameData
    {
        public int killCount = 0;                                                                   //사망한 Enemy의 수
        public float exp = 0.0f;                                                                    //Player 경험치
        public float hp = 120.0f;                                                                   //Player 초기 생명
        public float damage = 25.0f;                                                                //총알의 데미지
        public float speed = 6.0f;                                                                  //이동 속도
        public int Strength = 10;
        public int Dexterity = 5;
        public int Constitution = 5;
        public List<Item> equipItem = new List<Item>();                                             //취득한 아이템
    }

    [System.Serializable]
    public class Item
    {
        public enum ITEMTYPE { ITEMTYPE_HP, ITEMTYPE_SPEED, ITEMTYPE_GRENADE, ITEMTYPE_DAMAGE }     //아이템 종류 선언
        public enum ITEMCALC { ITEMCALC_INC_VALUE, ITEMCALC_PERCENT }                               //계산 방식 선언
        public ITEMTYPE itemType;                                                                   //아이템 종류
        public ITEMCALC itemCalc;                                                                   //아이템 적용시 계산 방식
        public string name;                                                                         //아이템 이름
        public string desc;                                                                         //아이템 소개
        public float value;                                                                         //계산 값
    }
}
