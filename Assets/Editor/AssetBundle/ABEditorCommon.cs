﻿using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

/// <summary>
///   AssetBundle打包策略
/// </summary>
public enum emAssetBundleNameRule
{
    None,               // 无
    SingleFile,         // 单个文件
    Folder,             // 文件夹
    Ignore,             // 忽略文件或者文件夹
}

/// <summary>
///   
/// </summary>
public static class ABEditorCommon
{
    /// <summary>
    ///   编辑器环境下默认打包路径
    /// </summary>
    public static readonly string BUILD_PATH = System.IO.Directory.GetCurrentDirectory() + "\\" + ABCommon.ROOT_FOLDER_NAME;

    /// <summary>
    ///   编辑器环镜下资源起始路径
    /// </summary>
    public static readonly string ASSET_START_PATH = Application.dataPath;

    /// <summary>
    ///   编辑器环镜下场景起始路径
    /// </summary>
    public static readonly string SCENE_START_PATH = Application.dataPath + "/Scenes";

    /// <summary>
    ///   编辑器环镜下主Manifest保存路径
    /// </summary>
    public static readonly string MAIN_MANIFEST_FILE_PATH = BUILD_PATH + "/" + ABCommon.MAIN_MANIFEST_FILE_NAME;

    /// <summary>
    ///   忽略的文件类型(后缀名)
    /// </summary>
    public static readonly string[] IGNORE_FILE_EXTENSION_ARRAY = 
        {
            ".rule",
            ".cs",
            ".js",
            ".meta",
            ".svn",
        };

    /// <summary>
    ///   忽略的文件夹
    /// </summary>
    public static readonly string[] IGNORE_FOLDER_ARRAY = 
        {
            ".svn",
        };

    /// <summary>
    ///   ProjectDirectory
    /// </summary>
    public static string ProjectDirectory
    {
        get
        {
            string directory = System.IO.Directory.GetCurrentDirectory() + "\\";
            directory = directory.Replace('\\', '/');
            return directory;
        }
    }

    /// <summary>
    ///   判断是否需要忽略
    /// </summary>
    public static bool IsIgnoreFile(string file_name)
    {
        string extension = System.IO.Path.GetExtension(file_name);
        foreach (string ignore in ABEditorCommon.IGNORE_FILE_EXTENSION_ARRAY)
        {
            if (extension == ignore)
                return true;
        }

        return false;
    }

    /// <summary>
    ///   判断是否需要忽略
    /// </summary>
    public static bool IsIgnoreFolder(string full_name)
    {
        string name = System.IO.Path.GetFileName(full_name);
        foreach (string ignore in IGNORE_FOLDER_ARRAY)
        {
            if (name == ignore)
                return true;
        }

        return false;
    }

    /// <summary>
    ///   加载AssetBundle
    /// </summary>
    public static AssetBundle LoadAssetBundleFromName(string assetbundlename)
    {
        string assetbundle_path = BUILD_PATH + "/" + assetbundlename;
        if (System.IO.File.Exists(assetbundle_path))
        {
            return AssetBundle.LoadFromFile(assetbundle_path);
        }

        return null;
    }

    /// <summary>
    ///   Unity/Assets相对路径转换为绝对路径
    /// </summary>
    public static string RelativeToAbsolutePath(string path)
    {
        return ProjectDirectory + path;
    }

    /// <summary>
    ///   绝对路径转换为Unity/Assets相对路径
    /// </summary>
    public static string AbsoluteToRelativePath(string path)
    {
        path = path.Replace('\\', '/');
        int last_idx = path.LastIndexOf(ProjectDirectory);
        if (last_idx < 0)
            return path;

        int start = last_idx + ProjectDirectory.Length;
        int length = path.Length - start;
        return path.Substring(start, length);
    }

    /// <summary>
    /// 得到合适的AssetBundleName
    /// </summary>
    public static string ConvertToAssetBundleName(string fullPath)
    {
        if (string.IsNullOrEmpty(fullPath))
            return fullPath;

        int position = fullPath.LastIndexOf(".");
        fullPath = position > -1 ? fullPath.Substring(0, position) : fullPath;
        return fullPath + ABCommon.EXTENSION;
    }
}
