using UnityEngine;
using System.Collections;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class FacebookManager : MonoBehaviour {


    public static FacebookManager _inst;

    public static FacebookManager instance
    {
        get
            {
            if (null == _inst)
            {
                GameObject fbm = new GameObject("FacebookManager");
                fbm.AddComponent<FacebookManager>();
            }
            return _inst;
        }
    }

    public bool IsLoggedIn { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _inst = this;

        IsLoggedIn = true;
    }
}
