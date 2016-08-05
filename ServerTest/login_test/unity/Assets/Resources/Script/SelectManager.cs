using UnityEngine;
using System.Collections;

public class SelectManager : MonoBehaviour {
    Camera _mainCamera = null;
    private bool _mouseState;
    private GameObject target;
    private Vector3 MousePos;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            target = GetClickedObject();
            if(target.Equals(gameObject))
            {
                _mouseState = true;
            }
            else if(Input.GetMouseButtonUp(0))
            {
                _mouseState = false;
            }
            if(_mouseState)
            {
                transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            }
            else
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
        
    }

    private GameObject GetClickedObject()
    {
        RaycastHit hit;
        GameObject target = null;
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray.origin,ray.direction*10,out hit))
        {
            target = hit.collider.gameObject;
        }
        return target;
    }
}
