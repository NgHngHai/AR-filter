using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARFilterManager : MonoBehaviour
{
    public Filter[] filters;
    public ARFaceManager faceManager;

    // Start is called before the first frame update
    void Start()
    {
        faceManager = FindObjectOfType<ARFaceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class Filter
{
    public Sprite filterSprite;
    public GameObject filter;
}
