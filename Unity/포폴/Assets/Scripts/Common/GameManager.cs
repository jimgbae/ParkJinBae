using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DataInfo;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public Transform PlayerSpawn;
    public GameObject Player;
    public bool isResetPlayer = false;

    //게임 종료 판단 변수
    public bool isGameOver = false;
    //게임 리셋 판단 변수
    public bool isReset = false;
    //Singleton에 접근하기 위해 static으로 변수 선언
    public static GameManager instance = null;

    [Header("Object Pool")]
    //총알 Prefab
    public GameObject bulletPrefab;
    //오브젝트 풀에 생성할 개수
    public int maxPool = 20;
    public List<GameObject> BulletPool = new List<GameObject>();

    //Enemy 프리팹 저장 변수
    public GameObject enemyPrefab;
    //Enemy 생성 주기 변수
    public float createTime = 2.0f;
    //최대 Enemy 개수
    public int maxEnemy;
    public List<GameObject> EnemyPool = new List<GameObject>();

    //Enemy 스폰지역 위치 변수
    public Transform[] points;
    //게임 멈춤 판단 변수ㅔ
    public bool isPaused;
    //Inventory의 CanvasGroup 컴포넌트 저장 변수
    public CanvasGroup invenCG;

    //스텟창 컴포넌트 저장 변수
    public CanvasGroup StateCG;
    //스텟 업그레이드 관리창 저장 변수
    public CanvasGroup StatPointCG;
    //스텟 포인트
    private Text StatText;
    //힘
    private Text STRText;
    //민첩
    private Text DEXText;
    //채력
    private Text CONText;

    //Player가 Enemy를 죽인 횟수
    public int EnemyDieCount = 0;
    [Header("GameData")]
    //Enemy를 죽인 횟수 표시 Text UI
    public Text KillCountText;
    //DataManager 저장 변수
    private DataManager dataManager;
    public GameData gameData;

    //Inventory아이템 변경시 발생시킬 이벤트 정의
    public delegate void ItemChangeDelegate();
    public static event ItemChangeDelegate OnItemChange;

    //SlotList 게임오브젝트 저장 변수와 ItemList 하위에 있는 네 개의 아이템 저장 배열
    private GameObject slotList;
    public GameObject[] itemObjects;

    //StageManager 컴포넌트 저장 변수
    private StageManager stageM;

    //Stat변화 시 발생시킬 이벤트 정의
    public delegate void StatChangeDelegate();
    public static event StatChangeDelegate OnStatChange;

    public CanvasGroup PauseCG;


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

        DontDestroyOnLoad(this.gameObject);

        PlayerPrefab = (Resources.Load("Prefabs/Player")) as GameObject;
        Player = Instantiate<GameObject>(PlayerPrefab, this.transform);
        Player.SetActive(false);

        dataManager = GetComponent<DataManager>();
        dataManager.Initialize();

        
        slotList = invenCG.transform.Find("SlotList").gameObject;


        //게임 초기 데이터 로드
        LoadGameData();

        //총알 오브젝트 풀에 생성
        CreatePooling();
    }

    void LoadGameData()
    {
        //DataManager를 통해 파일에 저장된 데이터 불러오기
        GameData data = dataManager.Load();

        gameData.hp = data.hp;
        gameData.exp = data.exp;
        gameData.damage = data.damage;
        gameData.speed = data.speed;
        gameData.killCount = data.killCount;
        gameData.Strength = data.Strength;
        gameData.Dexterity = data.Dexterity;
        gameData.Constitution = data.Constitution;
        gameData.Status = data.Status;
        gameData.equipItem = data.equipItem;

        if (gameData.equipItem.Count > 0)
        {
            InventorySetup();
        }

        //KILL_COUNT 키로 저장된값 로드
        KillCountText.text = "KILL " + gameData.killCount.ToString("0000");
    }

    void SaveGameData()
    {
        dataManager.Save(gameData);
    }

    //로드한 데이터 기준으로 인벤토리 아이템 추가 함수
    void InventorySetup()
    {
        //SlotList하위 모든 Slot 추출
        var slots = slotList.GetComponentsInChildren<Transform>();

        //보유한 아이템 개수만큼 반복
        for (int i = 0; i < gameData.equipItem.Count; i++)
        {
            for (int j = 1; j < slots.Length; j++)
            {
                //Slot하위에 다른 아이템 있을 시 다음 인덱스로 넘김
                if (slots[j].childCount > 0) continue;

                //보유한 아이템 종류에 따라 인덱스 추출
                int itemIndex = (int)gameData.equipItem[i].itemType;

                //아이템의 부모를 Slot으로 변경
                itemObjects[itemIndex].GetComponent<Transform>().SetParent(slots[j]);
                //아이템의 ItemInfo클래스의 itemData에 로드한 데이터값 저장
                itemObjects[itemIndex].GetComponent<ItemInfo>().itemData = gameData.equipItem[i];

                //아이템 slot에 추가하면 for문 종료
                break;
            }
        }
    }

    public void AddItem(Item item)
    {
        //보유 아이템에 같은 아이템이 있으면 추가하지않음
        if (gameData.equipItem.Contains(item)) return;

        //아이템을 GameData.equipItem 배열에 추가
        gameData.equipItem.Add(item);

        switch (item.itemType)
        {
            case Item.ITEMTYPE.ITEMTYPE_HP:
                if (item.itemCalc == Item.ITEMCALC.ITEMCALC_INC_VALUE)
                    gameData.hp += item.value;
                else
                    gameData.hp += gameData.hp * item.value;
                break;
            case Item.ITEMTYPE.ITEMTYPE_DAMAGE:
                if (item.itemCalc == Item.ITEMCALC.ITEMCALC_INC_VALUE)
                    gameData.damage += item.value;
                else
                    gameData.damage += gameData.damage * item.value;
                break;
            case Item.ITEMTYPE.ITEMTYPE_SPEED:
                if (item.itemCalc == Item.ITEMCALC.ITEMCALC_INC_VALUE)
                    gameData.speed += item.value;
                else
                    gameData.speed += gameData.speed * item.value;
                break;
            case Item.ITEMTYPE.ITEMTYPE_GRENADE:
                break;
        }

        //아이템 변경 실시간 반영하기 위해 이벤트 발생
        OnItemChange();

    }

    public void RemoveItem(Item item)
    {
        gameData.equipItem.Remove(item);

        switch (item.itemType)
        {
            case Item.ITEMTYPE.ITEMTYPE_HP:
                if (item.itemCalc == Item.ITEMCALC.ITEMCALC_INC_VALUE)
                    gameData.hp -= item.value;
                else
                    gameData.hp = gameData.hp / (1.0f + item.value);
                break;
            case Item.ITEMTYPE.ITEMTYPE_DAMAGE:
                if (item.itemCalc == Item.ITEMCALC.ITEMCALC_INC_VALUE)
                    gameData.damage -= item.value;
                else
                    gameData.damage = gameData.damage / (1.0f + item.value);
                break;
            case Item.ITEMTYPE.ITEMTYPE_SPEED:
                if (item.itemCalc == Item.ITEMCALC.ITEMCALC_INC_VALUE)
                    gameData.speed -= item.value;
                else
                    gameData.speed = gameData.speed / (1.0f + item.value);
                break;
            case Item.ITEMTYPE.ITEMTYPE_GRENADE:
                break;
        }

        //아이템 변경 실시간 반영하기 위해 이벤트 발생
        OnItemChange();
    }


    void Start()
    {
        StatText = CanvasManager.instance.Stat;
        STRText = CanvasManager.instance.STR;
        DEXText = CanvasManager.instance.DEX;
        CONText = CanvasManager.instance.CON;
        stageM = GetComponent<StageManager>();
        //처음 인벤토리 비활성화
        OnInventoryOpen(false);
        //처음 스탯창 비활성화
        OnStatOpen(false);

        OnESCBtn(false);

        Setting();

        SetText();

        GameStart();
    }

    public void GameStart()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        OnESCBtn(false);
        if (points.Length > 0)
        {
            StartCoroutine(this.CreateEnemy());
        }
    }

    void SetText()
    {
        KillCountText = CanvasManager.instance.KillCount;
    }

    //오브젝트 풀에서 사용 가능한 총알을 가져오는 함수
    public GameObject GetBullet()
    {
        for (int i = 0; i < BulletPool.Count; i++)
        {
            //총알 비활성화 여부 판단
            if (BulletPool[i].activeSelf == false)
            {
                return BulletPool[i];
            }
        }
        return null;
    }

    //EnemyList에서 사용 가능한 Enemy를 가져오는 함수
    public GameObject GetEnemy()
    {
        for (int i = 0; i < EnemyPool.Count; i++)
        {
            if (EnemyPool[i].activeSelf == false)
            {
                return EnemyPool[i];
            }
        }
        return null;
    }

    //오브젝트 풀에 Enemy 생성 함수
    public void CreateEnemyPooling()
    {
        for (int i = 0; i < maxEnemy; i++)
        {
            var obj = Instantiate<GameObject>(enemyPrefab,this.transform);
            obj.name = "Enemy" + i.ToString("00");
            obj.SetActive(false);
            EnemyPool.Add(obj);
        }
    }

    //오브젝트 풀에 총알 생성 함수
    public void CreatePooling()
    {
        //풀링 개수만큼 미리 총알 생성
        for (int i = 0; i < maxPool; i++)
        {
            var obj = Instantiate<GameObject>(bulletPrefab,this.transform);
            obj.name = "Bullet" + i.ToString("00");
            obj.SetActive(false);
            //리스트에 총알 추가
            BulletPool.Add(obj);
        }
    }


    public void OnPauseClick()
    {
        //일시 정지값 토글
        isPaused = !isPaused;
        //Time.timeScale이 0이되면 정지, 1이면 정상 속도
        Time.timeScale = (isPaused) ? 0.0f : 1.0f;

        //Player 객체 추출
        var playerObj = GameObject.FindGameObjectWithTag("PLAYER");
        //Player에 추가된 모든 스크립트 추출
        var scripts = playerObj.GetComponents<MonoBehaviour>();
        //Player의 모든 스크립트 활성화/비활성화
        foreach (var script in scripts)
        {
            script.enabled = !isPaused;
        }

        var canvasGroup = GameObject.Find("Weapon").GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = !isPaused;
    }

    public void OnInventoryOpen(bool isOpened)
    {
        invenCG.alpha = (isOpened) ? 1.0f : 0.0f;
        invenCG.interactable = isOpened;
        invenCG.blocksRaycasts = isOpened;
    }

    public void OnStatOpen(bool isOpened)
    {
        StateCG.alpha = (isOpened) ? 1.0f : 0.0f;
        StateCG.interactable = isOpened;
        StateCG.blocksRaycasts = isOpened;
    }

    public void IncKillCount()
    {
        ++gameData.killCount;
        KillCountText.text = string.Format("KILL {0}", gameData.killCount);
    }

    public void IncExp(float Exp)
    {
        gameData.exp += Exp;
    }

    void OnApplicationQuit()
    {
        SaveGameData();
    }

    //적 생성
    IEnumerator CreateEnemy()
    {
        while (!isGameOver)
        {
            int enemyCount = 0;

            if (enemyCount < maxEnemy)
            {
                yield return new WaitForSeconds(createTime);

                enemyCount++;
                //SpawnPoint 랜덤 위치 설정
                int idx = Random.Range(1, points.Length);
                var _Enemy = GetEnemy();
                if (_Enemy != null)
                {
                    _Enemy.transform.position = points[idx].position;
                    _Enemy.SetActive(true);
                }
            }
            if(EnemyDieCount == maxEnemy)
            {
                PlayerWin();
                isGameOver = true;
            }
        }
    }

    public void PlayerWin()
    {
        SaveGameData();
        Reset();
        SceneManager.LoadScene("GameClear");
    }

    public void PlayerDie()
    {
        gameData.killCount = 0;
        gameData.exp = 0;
        SaveGameData();
        Reset();
        SceneManager.LoadScene("GameOver");
    }

    public void Setting()
    {
        GameObject.FindGameObjectWithTag("STAGEMANAGER").GetComponent<StageManager>().GameSetting();
        PlayerSetting();
        CreateEnemyPooling();
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void PlayerSetting()
    {
        isResetPlayer = false;
        if (Player != null)
        {
            Player.transform.position = PlayerSpawn.position;
            Player.SetActive(true);
        }
    }

    public void Reset()
    {
        if (isPaused == true)
            OnPauseClick();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isGameOver = false;
        isResetPlayer = true;
        CanvasManager.instance.OffCanvas();
        EnemyDieCount = 0;
        foreach(var obj in BulletPool)
        {
            obj.SetActive(false);
        }
        foreach (var obj in EnemyPool)
        {
            obj.SetActive(false);
            Destroy(obj);
        }
        Player.SetActive(false);
        EnemyPool.Clear();
    }

    void Update()
    {
        UpdateText();

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (invenCG.alpha == 1.0f)
            {
                OnInventoryOpen(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                OnInventoryOpen(true);
 }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if (StateCG.alpha == 1.0f)
            {
                OnStatOpen(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                OnStatOpen(true);
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                OnESCBtn(true);
                OnPauseClick();
            }
            else
            {
                OnPauseClick();
                OnESCBtn(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        if (StateCG.alpha == 1.0f && gameData.Status > 0)
        {
            OnPlusStat(true);
        }
        else
            OnPlusStat(false);
    }

    public void GetStatPoint()
    {
        gameData.Status += 2;
    }

    public void PlusStatPoint(int Point)
    {
        switch (Point)
        {
            case 1:
                gameData.Strength += 1;
                gameData.Status--;
                gameData.damage += 0.2f;
                OnStatChange();
                break;
            case 2:
                gameData.Dexterity += 1;
                gameData.Status--;
                gameData.speed += 0.05f;
                OnStatChange();
                break;
            case 3:
                gameData.Constitution += 1;
                gameData.Status--;
                gameData.hp += 5.0f;
                OnStatChange();
                break;
        }
        UpdateText();
    }

    public void OnPlusStat(bool isOpened)
    {
        StatPointCG.alpha = (isOpened) ? 1.0f : 0.0f;
        StatPointCG.interactable = isOpened;
        StatPointCG.blocksRaycasts = isOpened;
    }

    void UpdateText()
    {
        UpdateStatPointText();
        UpdateSTRText();
        UpdateDexText();
        UpdateCONText();
    }

    void UpdateStatPointText()
    {
        StatText.text = string.Format("{0}", gameData.Status);
    }

    void UpdateSTRText()
    {
        STRText.text = string.Format("{0}", gameData.Strength);
    }

    void UpdateDexText()
    {
        DEXText.text = string.Format("{0}", gameData.Dexterity);
    }

    void UpdateCONText()
    {
        CONText.text = string.Format("{0}", gameData.Constitution);
    }

    public void OnESCBtn(bool isOpened)
    {
        if (isPaused == true)
        {
            OnPauseClick();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        PauseCG.alpha = (isOpened) ? 1.0f : 0.0f;
        PauseCG.interactable = isOpened;
        PauseCG.blocksRaycasts = isOpened;
    }

    public void OnLobbyBtn()
    {
        OnPauseClick();
        Reset();
        SceneManager.LoadScene("Main");
    }
}
