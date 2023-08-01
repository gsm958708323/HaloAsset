using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnLoadABTest : MonoBehaviour
{
    public string bundleName = "model131";
    public string assetName = "model131";

    AssetBundle assetBundle;
    GameObject goAsset;
    GameObject go;

    private void OnGUI()
    {
        float h = 50;
        float w = 100;
        if (GUILayout.Button("LoadFile", GUILayout.MinHeight(h), GUILayout.MinWidth(w)))
            LoadFile();

        if (GUILayout.Button("LoadAsset", GUILayout.MinHeight(h), GUILayout.MinWidth(w)))
            LoadAsset();

        if (GUILayout.Button("InstantiateAsset", GUILayout.MinHeight(h), GUILayout.MinWidth(w)))
            InstantiateAsset();

        if (GUILayout.Button("Asset Unload_True", GUILayout.MinHeight(h), GUILayout.MinWidth(w)))
            Unload_True();

        if (GUILayout.Button("Asset Unload_false", GUILayout.MinHeight(h), GUILayout.MinWidth(w)))
            Unload_false();


        if (GUILayout.Button("ReLoadScene", GUILayout.MinHeight(h), GUILayout.MinWidth(w)))
            SceneManager.LoadScene("LoadTest");
    }

    private void Unload_false()
    {
        assetBundle.Unload(false);
    }

    private void Unload_True()
    {
        assetBundle.Unload(true);
    }

    private void InstantiateAsset()
    {
        go = Instantiate(goAsset);
    }

    private void LoadAsset()
    {
        goAsset = assetBundle.LoadAsset<GameObject>(assetName);
    }

    private void LoadFile()
    {
        if (assetBundle == null)
            assetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, bundleName));
    }
}
