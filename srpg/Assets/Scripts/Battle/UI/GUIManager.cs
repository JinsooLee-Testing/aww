﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GUIManager : MonoBehaviour {
    private static GUIManager inst = null;
    private PlayerManager pm = null;
    public bool battle = true;
    public bool small = false;
    void Awake()
    {
        inst = this;
        pm = PlayerManager.GetInst();

    }
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
    }
    public static GUIManager GetInst()
    {
        return inst;
    }
    public void DrawGUI()
    {

        //OnGUI();
    }

    public void DrawCommand(PlayerBase pb)
    {


    }

}
