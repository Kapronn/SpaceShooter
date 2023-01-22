using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScrollingEffect : MonoBehaviour
{
    [SerializeField] float scrollingSpeed = 0.5f;
    Material material;
    Vector2 offSet;
    void Start()
    {
        material = GetComponent<Renderer>().material;
        offSet = new Vector2(0, scrollingSpeed);

    }

   
    void Update()
    {
        
        
        material.mainTextureOffset += offSet * Time.deltaTime;
    }
}
