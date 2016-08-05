using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ButtonCreator : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 50, 50), "test"))
            Debug.Log("Clicked the button with an image");
    }
    // UnityEngine.UI.Button UiTexturedButton(Sprite sprite, Vector2 size, GameObject canvas)
    //{
    //    GameObject go = new GameObject("Textured button (" + sprite.name + ")");

    //    Image image = go.AddComponent<Image>();
    //    image.sprite = sprite;

    //    UnityEngine.UI.Button button = go.AddComponent<UnityEngine.UI.Button>();
    //    go.transform.SetParent(canvas.transform, false);

    //    image.rectTransform.sizeDelta = size;

    //    return button;
    //}

}
