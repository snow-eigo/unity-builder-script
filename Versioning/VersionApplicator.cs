using System;
using UnityEditor;

namespace UnityBuilderScript.Versioning
{
    public class VersionApplicator
    {
        public static void SetVersion(string version)
        {
            if (version == "none")
            {
                return;
            }

            Apply(version);
        }

        public static void SetAndroidVersionCode(string androidVersionCode)
        {
            PlayerSettings.Android.bundleVersionCode = int.Parse(androidVersionCode);
        }

        public static void SetIosBuildNumber(string iOSBuildNumber)
        {
            PlayerSettings.iOS.buildNumber = iOSBuildNumber;
        }

        static void Apply(string version)
        {
            PlayerSettings.bundleVersion = version;
            PlayerSettings.macOS.buildNumber = version;
        }
    }
}
