using System.Collections;
using System.Collections.Generic;
//파일 입출력
using System.IO;
//바이너리 파일 포맷
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using DataInfo;

public class DataManager : MonoBehaviour
{
    //파일 저장 물리 경로 및 파일명 저장 변수
    private string dataPath;

    public void Initialize()
    {
        dataPath = Application.persistentDataPath + "/gameData.dat";
    }

    public void Save(GameData gameData)
    {
        //바이너리 파일 포맷을 위한 BinaryFormatter 생성
        BinaryFormatter bf = new BinaryFormatter();
        //데이터 저장 파일 생성
        FileStream file = File.Create(dataPath);

        //파일에 저장할 클래스에 데이터 할당
        GameData data = new GameData();
        data.killCount = gameData.killCount;
        data.level = gameData.level;
        data.hp = gameData.hp;
        data.exp = gameData.exp;
        data.speed = gameData.speed;
        data.damage = gameData.damage;
        data.equipItem = gameData.equipItem;

        //BinaryFormatter를 사용해 파일에 데이터 기록
        bf.Serialize(file, data);
        file.Close();
    }

    //파일에서 데이터 추출 함수
    public GameData Load()
    {
        if (File.Exists(dataPath))
        {
            //파일 존재시 데이터 불러오기
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(dataPath, FileMode.Open);
            //GameData 클래스에 파일로부터 읽은 데이터를 기록
            GameData data = (GameData)bf.Deserialize(file);
            file.Close();

            return data;
        }
        else
        {
            //파일 없을 경우 기본값 반환
            GameData data = new GameData();

            return data;
        }
    }
}
