using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour {
    /// <summary>
    /// C# version of a unity script by Daniel Brauer taken from here:
    /// https://forum.unity3d.com/threads/fade-in-and-fade-out-of-a-gameobject.4723/
    /// Author: Lilo Elia
    /// </summary>

    public float fadeTime = 1f;
    public string buttonName = "Toggle Material";
    //color records and state booleans
    Color solidColor;
    Color fadedColor;
    bool fading;
    bool faded;
    Renderer myRenderer;
    //record color
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        solidColor = myRenderer.material.color;
        fadedColor = new Color(solidColor.r, solidColor.g, solidColor.b, 0f);
    }
    //check for input only if we aren't in the middle of fading
    void Update()
    {
        if (!fading && Input.GetButtonDown(buttonName))
        {
            StartCoroutine(Fade());
        }
    }
    //set fading and lerp from faded to solid over fadeTime
    IEnumerator Fade()
    {
        fading = true;
        Color fromColor = faded ? fadedColor : solidColor;
        Color toColor = faded ? solidColor : fadedColor;
        for (var t = 0f; t < fadeTime; t += Time.deltaTime)
        {
            myRenderer.material.color = Color.Lerp(fromColor, toColor, t / fadeTime);
            yield return null;
        }
        fading = false;
        faded = !faded;
    }
}
