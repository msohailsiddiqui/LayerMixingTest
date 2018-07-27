using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanLayer : MonoBehaviour
{
    
    public RenderTexture Diffuse;
    public RenderTexture Normal;
    public RenderTexture Displacement;

    public float Height = 0;
    public Text indexText;
    public GameObject loadMapsBtn;

    private bool mapsLoaded;
    private BlitLoop bl;

    void Awake()
    {
        bl = FindObjectOfType<BlitLoop>();
        mapsLoaded = false;
        if (loadMapsBtn != null)
        {
            if (mapsLoaded)
            {
                loadMapsBtn.SetActive(false);
            }
            else
            {
                loadMapsBtn.SetActive(true);
            }
        }
    }

    public void SetHeight(float value)
    {
        Debug.Log("Setting height to " + value);
        Height = value;
        bl.MixLayers();
    }

    public void Create(int index)
    {
        if(indexText != null)
        {
            indexText.text = index.ToString();
        }
    }

    public void LoadMaps()
    {
        mapsLoaded = true;
        if (loadMapsBtn != null)
        {
            FileLoader.Instance.LoadMap(out Diffuse, out Normal, out Displacement);
            loadMapsBtn.SetActive(false);
        }

    }
}
