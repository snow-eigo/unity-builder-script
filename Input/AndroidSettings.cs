using System.Collections.Generic;
using UnityEditor;

namespace UnityBuilderScript.Input
{
    public class AndroidSettings
    {
        public static void Apply(Dictionary<string, string> options)
        {
            EditorUserBuildSettings.buildAppBundle = options["customBuildPath"].EndsWith(".aab");

            if (options.TryGetValue("androidKeystoreName", out var keystoreName) && !string.IsNullOrEmpty(keystoreName))
                PlayerSettings.Android.keystoreName = keystoreName;

            if (options.TryGetValue("androidKeystorePass", out var keystorePass) && !string.IsNullOrEmpty(keystorePass))
                PlayerSettings.Android.keystorePass = keystorePass;

            if (options.TryGetValue("androidKeyaliasName", out var keyaliasName) && !string.IsNullOrEmpty(keyaliasName))
                PlayerSettings.Android.keyaliasName = keyaliasName;

            if (options.TryGetValue("androidKeyaliasPass", out var keyaliasPass) && !string.IsNullOrEmpty(keyaliasPass))
                PlayerSettings.Android.keyaliasPass = keyaliasPass;
        }
    }
}
