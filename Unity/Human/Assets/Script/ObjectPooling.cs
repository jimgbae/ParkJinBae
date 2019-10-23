using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    //https://ronniej.sfuh.tk/%EC%9C%A0%EB%8B%88%ED%8B%B0%EC%97%90%EC%84%9C-%EC%98%A4%EB%B8%8C%EC%A0%9D%ED%8A%B8-%ED%92%80-%EB%A7%8C%EB%93%A4%EA%B8%B0-object-pool/
    public string poolItemName = string.Empty;
    public GameObject prefab = null;
    public int poolCount = 0;

    [SerializeField]
    private List<GameObject> poolList = new List<GameObject>();

    public void Initialize(Transform parent = null)
    {
        for(int ix = 0; ix < poolCount; ix++)
        {
            poolList.Add(CreateItem(parent));
        }
    }
    public void PushToPool(GameObject item, Transform parent = null)
    {
        item.transform.SetParent(parent);
        item.SetActive(false);
        poolList.Add(item);
    }
    public GameObject PopFromPool(Transform parent = null)
    {
        if (poolCount == 0)
            poolList.Add(CreateItem(parent));
        GameObject item = poolList[0];
        poolList.RemoveAt(0);
        return item;
    }
    private GameObject CreateItem(Transform parent = null)
    {
        GameObject item = Object.Instantiate(prefab) as GameObject;
        item.name = poolItemName;
        item.transform.SetParent(parent);
        item.SetActive(false);
        return item;
    }
}
