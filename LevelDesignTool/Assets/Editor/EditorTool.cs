using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(myscript))]
public class EditorTool : Editor
{
    private Vector3 _MousePosition;
    private myscript _MyScript;
    private bool _CreateObj = false;
    private GameObject _GridSceneObject;
    public float Depth=0;

    public Vector3 MouseinScene()
    {
        return _MousePosition;
    }
    private void OnEnable()
    {
        _GridSceneObject = GameObject.Find("grid");
    }

    private void OnSceneGUI()
    {
        if (_MyScript == null)
        {
            _MyScript = (myscript)target;
        }

        _MousePosition = new Vector3(Event.current.mousePosition.x, Event.current.mousePosition.y);
        Ray ray = HandleUtility.GUIPointToWorldRay(_MousePosition);
        RaycastHit hit;
        

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("LD")) == true)
        {
            _GridSceneObject = hit.collider.gameObject;
            Vector3 shiftOffset = _GridSceneObject.transform.position;
            shiftOffset.x = shiftOffset.x - (int)shiftOffset.x;
            shiftOffset.y = shiftOffset.y - (int)shiftOffset.y;
            shiftOffset.z = shiftOffset.z - (int)shiftOffset.z;
            _MousePosition.x = Mathf.Round(((hit.point.x + shiftOffset.x) - hit.normal.x * 0.001f) / _MyScript.gridsize) * _MyScript.gridsize - shiftOffset.x;
            _MousePosition.z = Mathf.Round(((hit.point.z + shiftOffset.z) - hit.normal.z * 0.001f) / _MyScript.gridsize) * _MyScript.gridsize - shiftOffset.z;
            _MousePosition.y =  1 + _GridSceneObject.transform.position.y;
          

            if (_CreateObj == true)

            {
                Event e = Event.current;
                if (e.isMouse)
                {
                    if (e.type == EventType.MouseDown )
                    {
                        if (e.button == 0 && e.alt==true)
                        {
                            _MyScript.CreateObj(MouseinScene());
                            EditorUtility.SetDirty(_MyScript);
                            EditorUtility.SetDirty(_MyScript.gameObject);
                        }
                    }
                }
            }
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if(GUILayout.Button("Spawn Object On Mouse Click =" + _CreateObj))
        {
            _CreateObj = !_CreateObj;
        }
       
        try
        {
            foreach(IngameObject g in _MyScript.createdObjects)
            {
                if(GUILayout.Button("Destroy object @ "+ g.get().transform.position))
                {
                    _MyScript.destroyObject(g);
                }
            }
        }
        catch
        {

        }
    }
}
