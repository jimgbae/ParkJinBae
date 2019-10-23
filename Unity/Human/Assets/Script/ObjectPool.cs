using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<ObjectPooling> objectPool = new List<ObjectPooling>();

    void Awake()
    {
            for (int ix = 0; ix < objectPool.Count; ++ix)
                objectPool[ix].Initialize(transform);
    }

    public bool PushToPool(string itemName, GameObject item, Transform parent = null)
    {
        ObjectPooling pool = GetPoolItem(itemName);
        if (pool == null)
            return false;

        pool.PushToPool(item, parent == null ? transform : parent);
        return true;
    }

    public GameObject PopFromPool(string itemName, Transform parent = null)
    {
        ObjectPooling pool = GetPoolItem(itemName);
        if (pool == null)
            return null;

        return pool.PopFromPool(parent);
    }

    ObjectPooling GetPoolItem(string itemName)
    {
        for (int ix = 0; ix < objectPool.Count; ++ix)
        {
            if (objectPool[ix].poolItemName.Equals(itemName))
                return objectPool[ix];
        }

        Debug.LogWarning("There's no matched pool list.");
        return null;
    }
}
