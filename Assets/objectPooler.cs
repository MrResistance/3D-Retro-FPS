using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPooler : MonoBehaviour
{
    #region Singleton
    public static objectPooler instance;

    public void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion
    public List<GameObject> objects = new List<GameObject>();
    private bool duplicate = false;
    public void AddObjectToPool(GameObject obj)
    {
        if (!CheckObjIsNotAlreadyInPool(obj))
        {
            obj.SetActive(true);
            objects.Add(obj);
        }
    }
    public void SetObjectInactive(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void RemoveObjectFromPool(GameObject obj)
    {
        objects.Remove(obj);
    }
    public void ClearPool()
    {
        objects.Clear();
    }

    public bool CheckObjIsNotAlreadyInPool(GameObject obj)
    {
        
        foreach (GameObject gameObject in objects)
        {
            if (gameObject.name == obj.name)
            {
                duplicate = true;
                break;
            }
            else
            {
                duplicate = false;
            }
        }
        return duplicate;
    }
}
