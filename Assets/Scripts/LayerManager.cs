using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour 
{
    public int layersToCreateAtStart = 0;

    public GameObject layerPrefab;
    public GameObject layersPanel;

    private List<ScanLayer> layersList;

	// Use this for initialization
	void Start () 
    {
        layersList = new List<ScanLayer>();

        if(layersToCreateAtStart > 0)
        {
            for (int i=0; i < layersToCreateAtStart; i++)
            {
                GameObject temp = GameObject.Instantiate(layerPrefab) as GameObject;
                ScanLayer _scanlayer = temp.GetComponent<ScanLayer>();
                if(_scanlayer != null)
                {
                    layersList.Add(_scanlayer);
                    temp.transform.parent = layersPanel.transform;
                    temp.transform.SetAsLastSibling();
                }
                else
                {
                    Destroy(temp);
                }
            }
        }
            
		
	}

    public void AddLayer()
    {
        GameObject temp = GameObject.Instantiate(layerPrefab) as GameObject;
        ScanLayer _scanlayer = temp.GetComponent<ScanLayer>();
        if (_scanlayer != null)
        {
            layersList.Add(_scanlayer);
            temp.transform.parent = layersPanel.transform;
            temp.transform.SetAsLastSibling();
            _scanlayer.Create(layersList.Count);
        }
        else
        {
            Destroy(temp);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
