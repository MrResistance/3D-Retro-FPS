using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public Transform parent;
    }
    #region Singleton
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, pool.parent);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + "doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        switch (tag)
        {
            case "Enemy Projectile":
                objectToSpawn.GetComponent<Projectile>();
                //objectToSpawn.transform.rotation = new Quaternion(0,180,0,0);
                break;
            //case "Teleporter":
            //    objectToSpawn.GetComponent<Teleporter>().TeleporterStart();
            //    break;
            //case "Score Pickup Text":
            //    objectToSpawn.GetComponent<Fade>().DeactivateDelayed();
            //    break;
            //case "Pickup":
            //    objectToSpawn.GetComponent<Pickup>().fadeAway();
            //    objectToSpawn.GetComponent<Pickup>().FindPlayer();
            //    break;
            //case "Super Pickup Text":
            //    objectToSpawn.GetComponent<Fade>().DeactivateDelayed();
            //    break;
            //case "Implosion":
            //    objectToSpawn.GetComponent<Fade>().DeactivateDelayed();
            //    break;
            //case "Super Pickup":
            //    objectToSpawn.GetComponent<Pickup>().fadeAway();
            //    objectToSpawn.GetComponent<Pickup>().FindPlayer();
            //    break;
        }
        
        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}
