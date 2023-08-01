using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

public class LoadABEditorTest : MonoBehaviour
{
    public string bundleName = "model131_wp";
    public string assetName = "model131_wp";

    void Start()
    {
        LoadBundle_Editor();
    }

    void LoadBundle_Editor()
    {
        var path = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(bundleName, assetName);
        var go = AssetDatabase.LoadAssetAtPath<GameObject>(path[0]);
        Instantiate(go);
    }
}
#endif