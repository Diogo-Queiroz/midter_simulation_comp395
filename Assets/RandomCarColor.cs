using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCarColor : MonoBehaviour
{
    
    private MaterialPropertyBlock propBlock;
    private int colorID;

    [SerializeField] private List<Renderer> renderers;
    // Start is called before the first frame update
    void Start()
    {
        propBlock = new MaterialPropertyBlock();
        colorID = Shader.PropertyToID("_Color");

        var color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
        
        foreach (var renderer in renderers)
        {
            renderer.GetPropertyBlock(propBlock);
            propBlock.SetColor(colorID,color);
            renderer.SetPropertyBlock(propBlock);
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
