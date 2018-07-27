using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blitter : MonoBehaviour
{
    public Texture Map1;
    public Texture Map2;
    public Shader BlendShader;
    private Material blendMat;
    public Renderer PreviewResult;

    private int map2ID;

	// Use this for initialization
	void Start ()
    {
        map2ID = Shader.PropertyToID("_Map2");
        blendMat = new Material(BlendShader);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.B)) Blend();
	}

    void Blend()
    {
        RenderTexture rt = new RenderTexture(Map1.width, Map1.height,0, RenderTextureFormat.ARGB32);
        blendMat.SetTexture(map2ID, Map2);
        Graphics.Blit(Map1, rt, blendMat);
        PreviewResult.material.mainTexture = rt;
    }
}
