using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    public GameObject Get(string name, Vector3? newPosition = null, Quaternion? newRotation = null)
    {
        if (pools.ContainsKey(name) == false)
        {
            return null;
        }
        else
        {
            Vector3 position = newPosition != null ? newPosition.Value : Vector3.zero;
            Quaternion rotation = newRotation != null ? newRotation.Value : Quaternion.identity;
            if (pools[name].Count <= 0)
            {
                GameObject newObject = Create(name, position, rotation);
                if (newObject != null)
                {
                    return newObject;
                }
            }
            else
            {
                GameObject pool = pools[name].Pop();
                pool.transform.SetParent(null);
                pool.transform.position = position;
                pool.transform.rotation = rotation;
                pool.SetActive(true);
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

    private GameObject Create(string name, Vector3 newPosition, Quaternion newRotation)
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            if (prefabs[i].name == name)
            {

                GameObject newObject = Instantiate(prefabs[i], newPosition, newRotation);
                newObject.name = prefabs[i].name;

                return newObject;
            }
        }
        return null;
    }
}
