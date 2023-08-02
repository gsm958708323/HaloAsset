using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Platform
{
    public static readonly string StandaloneWindows = "StandaloneWindows";
    public static readonly string Android = "Android";
}

public class ResMgr : MonoSingleton<ResMgr>
{
    private AssetBundleManifest manifest;

    public bool Encrypt = false;
    public Dictionary<string, BundleInfo> cacheBundle = new();

    public AssetBundle LoadAssetBundle(string bundleName, bool checkDependencies = true)
    {
        bundleName = bundleName.ToLower();

        // 加载依赖
        if (checkDependencies)
        {
            string[] dependencies = GetManifest().GetAllDependencies(bundleName);
            foreach (var dependency in dependencies)
            {
                if (cacheBundle.ContainsKey(dependency))
                {
                    continue;
                }

                AssetBundle ab = LoadBundle(dependency);
                cacheBundle.Add(dependency, new BundleInfo()
                {
                    Name = dependency,
                    Bundle = ab
                });
            }
        }

        // 加载自身
        AssetBundle assetBundle = LoadBundle(bundleName);
        return assetBundle;
    }

    public AssetBundleManifest GetManifest()
    {
        if (manifest != null)
            return manifest;

        // 获取当前平台
        string platform = "";
#if UNITY_ANDROID
        platform = Platform.Android;
#elif UNITY_STANDALONE_WIN
        platform = Platform.StandaloneWindows;
#endif

        AssetBundle assetBundle = LoadBundle(platform);
        manifest = assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        return manifest;
    }

    AssetBundle LoadBundle(string bundleName)
    {
        AssetBundle assetBundle;
        if (Encrypt)
        {
            assetBundle = LoadEncryptBundle(bundleName);
        }
        else
        {
            assetBundle = AssetBundle.LoadFromFile(GetABFilePath(bundleName));
        }
        return assetBundle;
    }

    private string GetABFilePath(string platform)
    {
        // todo 文件更新记录
        return Path.Combine(Application.streamingAssetsPath, platform);
    }

    private AssetBundle LoadEncryptBundle(string platform)
    {
        throw new NotImplementedException();
    }
}
