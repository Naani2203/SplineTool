using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myscript : MonoBehaviour
{
    public List<IngameObject> createdObjects;
    public List<GameObject> createdObjectsAsGameObjects;
    public GameObject block;
    public float gridsize;

    public void CreateObj(Vector3 pos)
    {
        if(createdObjects == null)
        {
            createdObjects = new List<IngameObject>();
        }

        if(createdObjectsAsGameObjects==null)
        {
            createdObjectsAsGameObjects = new List<GameObject>();
        
        }
        GameObject go = (GameObject)Instantiate(block, pos, Quaternion.Euler(0, 0, 0));

        createdObjects.Add(new IngameObject(go));

        createdObjectsAsGameObjects.Add(go);
    }

    public void destroyObject(IngameObject toDestroy)
    {
        createdObjectsAsGameObjects.Remove(toDestroy.get());
        createdObjects.Remove(toDestroy);
        DestroyImmediate(toDestroy.get());
    }
}

[System.Serializable]
public class IngameObject
{
    public GameObject MyObject;
    public IngameObject(GameObject myObj)
    {
        MyObject = myObj;
    }

    public GameObject get()
    {
        return MyObject;
    }
}



