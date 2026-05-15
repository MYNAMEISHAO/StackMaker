using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SimplePool : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static SimplePool Instance;

    private Dictionary<string, Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        OnInit();
    }

    public void OnInit()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    public GameObject Spawn(GameObject pref, Vector3 position,Quaternion rotation, Transform parent)
    {
        string key = pref.name;
        if (!pool.ContainsKey(key))
        {
            pool.Add(key, new Queue<GameObject>());
        }

        GameObject spawnedObject;
        if(pool[key].Count > 0)
        {
            spawnedObject = pool[key].Dequeue();
            spawnedObject.SetActive(true);
            spawnedObject.transform.localPosition = position;
            spawnedObject.transform.rotation = rotation;
        }
        else
        {
            spawnedObject = Instantiate(pref,position,rotation,parent);
            spawnedObject.SetActive(true);
            spawnedObject.name = key;
            spawnedObject.transform.localPosition = position;
            spawnedObject.transform.rotation = rotation;
        }
        return spawnedObject;
    }


    public void Despawn(GameObject pref)
    {
        string key = pref.name;
        if (pool.ContainsKey(key))
        {
            pool[key].Enqueue(pref);
            pref.SetActive(false);
        }
        else
        {
            if(!Application.isPlaying)
            {
                DestroyImmediate(pref);
            }

            else Destroy(pref);
        }
    }
}
