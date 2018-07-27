using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlitLoop : MonoBehaviour {

    public RenderTexture DiffuseRT;
    public RenderTexture DisplacementRT;
    public RenderTexture NormalRT;

    public List<ScanLayer> AllLayers = new List<ScanLayer>();

    public Renderer GroundPlane;
    public Shader HeightMask;
    public Shader LoopDiffuse;
    public Shader LoopNormals;
    public Shader LoopDisplacement;
    private Material displaceMat;
    private Material normalsMat;
    private Material diffuseMat;
    private Material maskMat;


    int height1ID;
    int height2ID;
    int maskID;
    int layerHeightID;

    void Awake()
    {
        DiffuseRT = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGB32);
        DisplacementRT = new RenderTexture(1024, 1024, 0, RenderTextureFormat.RFloat);
        NormalRT = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGB32);

        GroundPlane.material.SetTexture("_Diffuse", DiffuseRT);
        GroundPlane.material.SetTexture("_Normals", NormalRT);
        GroundPlane.material.SetTexture("_Displacement", DisplacementRT);
        maskMat = new Material(HeightMask);
        diffuseMat = new Material(LoopDiffuse);
        normalsMat = new Material(LoopNormals);
        displaceMat = new Material(LoopDisplacement);

        height1ID = Shader.PropertyToID("_Height1");
        height2ID = Shader.PropertyToID("_Height2");
        maskID = Shader.PropertyToID("_Mask");
        layerHeightID = Shader.PropertyToID("_LayerHeight");
    }

    public void MixLayers()
    {
        foreach (var layer in AllLayers)
        {
            BlitLayer(layer,firstLayer:layer==AllLayers[0]);
        }
    }

    void BlitLayer(ScanLayer layer, bool firstLayer)
    {
        if (firstLayer)
        {
            Debug.Log("Rendering first layer");
            Graphics.Blit(layer.Diffuse, DiffuseRT);
            Graphics.Blit(layer.Normal, NormalRT);
            Graphics.Blit(layer.Displacement, DisplacementRT);
            return;
        }

        RenderTexture tempMask = RenderTexture.GetTemporary(1024, 1024, 0, RenderTextureFormat.RHalf);

        Debug.Log("Rendering second layer");

        maskMat.SetTexture(height1ID, DisplacementRT);
        maskMat.SetTexture(height2ID, layer.Displacement);
        maskMat.SetFloat(layerHeightID, layer.Height);
        Graphics.Blit(null, tempMask, maskMat);

        diffuseMat.SetTexture(maskID,tempMask);
        Graphics.Blit(layer.Diffuse, DiffuseRT, diffuseMat);

        normalsMat.SetTexture(maskID, tempMask);
        Graphics.Blit(layer.Normal, NormalRT, normalsMat);

        displaceMat.SetTexture(maskID, tempMask);
        displaceMat.SetFloat(layerHeightID, layer.Height);
        Graphics.Blit(layer.Displacement, DisplacementRT, displaceMat);

        RenderTexture.ReleaseTemporary(tempMask);
    }
}
