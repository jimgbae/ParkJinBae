using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform itemTr;
    private Transform inventoryTr;
    private Transform itemListTr;
    private CanvasGroup canvasGroup;

    public static GameObject draggingItem = null;


    void Start()
    {
        itemTr = GetComponent<Transform>();
        inventoryTr = GameObject.Find("Inventory").GetComponent<Transform>();
        itemListTr = GameObject.Find("ItemList").GetComponent<Transform>();

        //CanvasGroup 컴포넌트 추출
        canvasGroup = GetComponent<CanvasGroup>();
    }

    //드래그 이벤트
    public void OnDrag(PointerEventData eventData)
    {
        //드래그 이벤트 발생시 아이템 위치를 마우스 커서 위치로 변경
        itemTr.position = Input.mousePosition;
    }

    //드래그 시작 시 한 번 호출되는 이벤트
    public void OnBeginDrag(PointerEventData eventData)
    {
        //부모 변경
        this.transform.SetParent(inventoryTr);
        //드래그가 시작되면 드래그되는 아이템 정보 저장
        draggingItem = this.gameObject;
        //드래그가 시작되면 다른 UI 이벤트를 받지 않도록 설정
        canvasGroup.blocksRaycasts = false;
    }

    //드래그 종료할 때 한 번 호출되는 이벤트
    public void OnEndDrag(PointerEventData eventData)
    {
        //드래그 종료시 드래그 아이템 null로 변경
        draggingItem = null;
        //드래그가 시작되면 다른 UI 이벤트를 받지 않도록 설정
        canvasGroup.blocksRaycasts = false;

        if(itemTr.parent == inventoryTr)
        {
            itemTr.SetParent(itemListTr.transform);
            GameManager.instance.RemoveItem(GetComponent<ItemInfo>().itemData);
        }
    }
    
    void Update()
    {
        
    }
}
