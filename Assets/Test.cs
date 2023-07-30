using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Test : MonoBehaviour
{
    public string bundleName = "model131_wp";
    public string assetName = "model131_wp";
    AssetBundle assetBundle;

    // Start is called before the first frame update
    void Start()
    {
        LoadBundle_Editor();
        // LoadDependence();
        // InstantiateBundle();
    }

    void LoadBundle_Editor(){
        var path = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(bundleName, assetName);
        var go = AssetDatabase.LoadAssetAtPath<GameObject>(path[0]);
        Instantiate(go);
    }

    private void LoadDependence()
    {
        // 先读取manifest文件
        var assetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "StandaloneWindows"));

        // 读取依赖信息
        var manifest = assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        var dependencies = manifest.GetAllDependencies(bundleName);
        foreach (var item in dependencies)
        {
            AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, item));
        }
    }

    private void InstantiateBundle()
    {
        assetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, bundleName));
        Instantiate(assetBundle.LoadAsset(assetName));
    }


    // Update is called once per frame
    void Update()
    {

    }
}
