using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myscript : MonoBehaviour
{
    public List<IngameObject> CreatedObjects;
    public List<GameObject> CreatedObjectsAsGameObjects;
    public GameObject PathObject;
    public GameObject Rail;
    public GameObject Mover;
    public float GridSize;
    private GameObject _Rail;

    public void CreateObj(Vector3 pos)
    {
        _Rail = GameObject.FindGameObjectWithTag("Rail");
        if(CreatedObjects == null)
        {
            CreatedObjects = new List<IngameObject>();
        }

        if(CreatedObjectsAsGameObjects==null)
        {
            CreatedObjectsAsGameObjects = new List<GameObject>();
        
        }
        if(_Rail == null)
        {
            Instantiate(Rail, pos, Quaternion.identity);
        }
        GameObject go = (GameObject)Instantiate(PathObject, pos, Quaternion.Euler(0, 0, 0), _Rail.transform);

        CreatedObjects.Add(new IngameObject(go));
        CreatedObjectsAsGameObjects.Add(go);
    }

    public void DestroyObject(IngameObject toDestroy)
    {
        CreatedObjectsAsGameObjects.Remove(toDestroy.get());
        CreatedObjects.Remove(toDestroy);
        DestroyImmediate(toDestroy.get());
    }
    public void CreateMover()
    {
        GameObject go = (GameObject)Instantiate(Mover, Vector3.zero, Quaternion.Euler(0, 0, 0));

        CreatedObjects.Add(new IngameObject(go));
        CreatedObjectsAsGameObjects.Add(go);
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



