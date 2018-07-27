using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileLoader  : Singleton<FileLoader> 
{
    private string pathToLoadFilesFrom;

    protected FileLoader() { } // guarantee this will be always a singleton only - can't use the constructor!

    public void LoadMap(out RenderTexture _diffuse, out RenderTexture _normal, out RenderTexture _displacement)
    {
        pathToLoadFilesFrom = "";
        BrowseWrapper.Instance.Browse(callbackFunction: GetFilePath,
            browseType: BrowseWrapper.Type.FileOpen, browserTitle: "Choose image to load",
            root: "", extensionFilterPreset: BrowseWrapper.ExtensionFilterPreset.Images, multiSelect: true);

        if(!string.IsNullOrEmpty(pathToLoadFilesFrom))
        {
            Debug.Log("FileLoader::LoadMap: pathToLoadFilesFrom: " + pathToLoadFilesFrom);
            //Split the path and get the paths to individual maps
            string[] splitItems = pathToLoadFilesFrom.Split('/');
            if (splitItems.Length > 0)
            {
                string directoryPath = "";
                for (int i = 0; i < splitItems.Length-1; i++)
                {
                    directoryPath += splitItems[i]+"/";
                }
                string fileName = splitItems[splitItems.Length - 1];
                Debug.Log("FileLoader::LoadMap: directoryPath: " + directoryPath);
                Debug.Log("FileLoader::LoadMap: fileName: " + fileName);
                splitItems = fileName.Split('_');
                string assetID = "";
                string assetIDWithRes = "";
                string fileEXT = "";
                string diffusePath = "";
                string normalPath = "";
                string displacementPath = "";
                if (splitItems.Length > 1)
                {
                    assetID = splitItems[0];
                    assetIDWithRes = assetID + "_" + splitItems[1];
                    splitItems = splitItems[splitItems.Length - 1].Split('.');
                    fileEXT = splitItems[splitItems.Length - 1];
                    Debug.Log("FileLoader::LoadMap: assetID: " + assetID);
                    Debug.Log("FileLoader::LoadMap: assetIDWithRes: " + assetIDWithRes);
                    Debug.Log("FileLoader::LoadMap: fileEXT: " + fileEXT);

                    diffusePath = directoryPath + assetIDWithRes + "_Albedo." + fileEXT;
                    normalPath = directoryPath + assetIDWithRes + "_Normal." + fileEXT;
                    displacementPath = directoryPath  + assetIDWithRes + "_Displacement." + fileEXT;
                    Debug.Log("FileLoader::LoadMap: diffusePath: " + diffusePath);
                    Debug.Log("FileLoader::LoadMap: normalPath: " + normalPath);
                    Debug.Log("FileLoader::LoadMap: displacementPath: " + displacementPath);
                    _diffuse = FreeImageManagerOrig.Instance.LoadImage(diffusePath, isLinear: false);
                    _normal = FreeImageManagerOrig.Instance.LoadImage(normalPath, isLinear: false);
                    _displacement = FreeImageManagerOrig.Instance.LoadImage(displacementPath, isLinear: false);
                    return;
                }
            }
        }
        _diffuse = null;
        _normal = null;
        _displacement = null;
        
    }

    private void GetFilePath(string filePath)
    {
        pathToLoadFilesFrom = filePath;
    }
}
