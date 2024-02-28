using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{

    [SerializeField] private GameObject[] prefabs;
    private Dictionary<string, Stack<GameObject>> pools;
    

    private void Awake()
    {
        if (Init() == false)
            return;

        pools = new Dictionary<string, Stack<GameObject>>();

        for (int i = 0; i < prefabs.Length; i++)
        {
            pools.Add(prefabs[i].name, new Stack<GameObject>());
        }


    }

    public GameObject Get(string name)
    {
        if (pools.ContainsKey(name) == false)
        {
            return null;
        }
        else
        {
            if (pools[name].Count <= 0)
            {
                GameObject newObject = Create(name);
                if (newObject != null)
                {
                    return newObject;
                }
            }
            else
            {
                GameObject pool = pools[name].Pop();
                pool.SetActive(true);
                pool.transform.SetParent(null);
                return pool;
            }
        }
        return null;
    }
    public void ReturnPool(GameObject poolObject)
    {
        if (pools.ContainsKey(poolObject.name) == false)
            return;

        poolObject.transform.SetParent(this.transform, false);
        poolObject.SetActive(false);

        pools[poolObject.name].Push(poolObject);

    }

    private GameObject Create(string name)
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            if (prefabs[i].name == name)
            {
                GameObject newObject = Instantiate(prefabs[i]);
                newObject.name = prefabs[i].name;


                return newObject;
            }
        }

        return null;
    }
}
