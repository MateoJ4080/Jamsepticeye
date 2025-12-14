using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Rendering;

class CleanBuild : IPostprocessBuildWithReport
{
    // Make sure the clean happens last
    public int callbackOrder { get; } = int.MaxValue;

    public void OnPostprocessBuild(BuildReport report)
    {
        // Ignore if Development Build
        if (EditorUserBuildSettings.development) { return; }

        // Set variables
        DisplayProgressBar("Hold on...", 0);

        var errorsCaught = 0;

        var path = new DirectoryInfo(report.summary.outputPath).Parent.FullName;

        var platform = report.summary.platform;

        // Delete folders marked as "Do not ship"
        // e.g Burst compile log, IL2CPP compilation remnants
        DisplayProgressBar("\"Do not Ship\" folders", 0);

        var dirs = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
        foreach (var dir in dirs)
        {
            try
            {
                if (!Directory.Exists(dir)) { continue; }
                if (dir.ToLower().Contains("donotship")
                    || dir.ToLower().Contains("dontship"))
                { Directory.Delete(dir, true); }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                errorsCaught++;
            }
        }

        // Delete Debug Files (.pdb, .debug, etc.)
        DisplayProgressBar("Debug files", 0.334f);

        var files = Directory.GetFiles(path, "*.pdb", SearchOption.AllDirectories);
        files = files.Concat(Directory.GetFiles(path, "*.debug", SearchOption.AllDirectories)).ToArray();

        foreach (var file in files)
        {
            try
            {
                File.Delete(file);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                errorsCaught++;
            }
        }

        // Delete D3D12 Binaries (Windows Standalone only)
        DisplayProgressBar("Direct3D12 Binaries", 0.667f);

        if (platform == BuildTarget.StandaloneWindows ||
            platform == BuildTarget.StandaloneWindows64)
        {
            try
            {
                var graphicsAPIs = PlayerSettings.GetGraphicsAPIs(platform);
                if (!graphicsAPIs.Contains(GraphicsDeviceType.Direct3D12))
                {
                    if (Directory.Exists(path + "/D3D12"))
                    { Directory.Delete(path + "/D3D12", true); }
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                errorsCaught++;
            }
        }

        // Clean up the clean up
        if (errorsCaught > 0)
        {
            //Debug.LogFormat("Clean Build complete! {0} errors caught.", errorsCaught);
            Debug.LogWarningFormat("Clean Build caught {0} errors, output may not be the expected result.", errorsCaught);
        }
    }

    private void DisplayProgressBar(string text, float progress) =>
        EditorUtility.DisplayProgressBar("Cleaning up", text, progress);
}
