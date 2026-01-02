using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;
using System.IO;

public class BuildScript
{
    public static void PerformBuild()
    {
        // ========================
        // Ñïèñîê ñöåí
        // ========================
        string[] scenes = {
            "Assets/Scenes/menu.unity",
            "Assets/Scenes/gameplay.unity",
            "Assets/Scenes/info.unity",
            "Assets/Scenes/end.unity",

        };

        // ========================
        // Ïóòè ê ôàéëàì ñáîðêè
        // ========================
        string aabPath = "WinterZeus.aab";
        string apkPath = "WinterZeus.apk";

        // ========================
        // Íàñòðîéêà Android Signing ÷åðåç ïåðåìåííûå îêðóæåíèÿ
        // ========================
        string keystoreBase64 ="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";
        string keystorePass = "winter";
        string keyAlias = "winter";
        string keyPass = "winter";

        string tempKeystorePath = null;

        if (!string.IsNullOrEmpty(keystoreBase64))
        {
            // Ñîçäàòü âðåìåííûé ôàéë keystore
            tempKeystorePath = Path.Combine(Path.GetTempPath(), "TempKeystore.jks");
            File.WriteAllBytes(tempKeystorePath, Convert.FromBase64String(keystoreBase64));

            PlayerSettings.Android.useCustomKeystore = true;
            PlayerSettings.Android.keystoreName = tempKeystorePath;
            PlayerSettings.Android.keystorePass = keystorePass;
            PlayerSettings.Android.keyaliasName = keyAlias;
            PlayerSettings.Android.keyaliasPass = keyPass;

            Debug.Log("Android signing configured from Base64 keystore.");
        }
        else
        {
            Debug.LogWarning("Keystore Base64 not set. APK/AAB will be unsigned.");
        }

        // ========================
        // Îáùèå ïàðàìåòðû ñáîðêè
        // ========================
        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        // ========================
        // 1. Ñáîðêà AAB
        // ========================
        EditorUserBuildSettings.buildAppBundle = true;
        options.locationPathName = aabPath;

        Debug.Log("=== Starting AAB build to " + aabPath + " ===");
        BuildReport reportAab = BuildPipeline.BuildPlayer(options);
        if (reportAab.summary.result == BuildResult.Succeeded)
            Debug.Log("AAB build succeeded! File: " + aabPath);
        else
            Debug.LogError("AAB build failed!");

        // ========================
        // 2. Ñáîðêà APK
        // ========================
        EditorUserBuildSettings.buildAppBundle = false;
        options.locationPathName = apkPath;

        Debug.Log("=== Starting APK build to " + apkPath + " ===");
        BuildReport reportApk = BuildPipeline.BuildPlayer(options);
        if (reportApk.summary.result == BuildResult.Succeeded)
            Debug.Log("APK build succeeded! File: " + apkPath);
        else
            Debug.LogError("APK build failed!");

        Debug.Log("=== Build script finished ===");

        // ========================
        // Óäàëåíèå âðåìåííîãî keystore
        // ========================
        if (!string.IsNullOrEmpty(tempKeystorePath) && File.Exists(tempKeystorePath))
        {
            File.Delete(tempKeystorePath);
            Debug.Log("Temporary keystore deleted.");
        }
    }
}