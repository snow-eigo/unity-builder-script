using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace UnityBuilderScript.Input
{
    public class ArgumentsParser
    {
        static readonly string Eol = Environment.NewLine;
        static readonly string[] Secrets = { "androidKeystorePass", "androidKeyaliasName", "androidKeyaliasPass" };

        public static Dictionary<string, string> GetValidatedOptions()
        {
            ParseCommandLineArguments(out var validatedOptions);

        if (!validatedOptions.TryGetValue("projectPath", out var projectPath))
        {
            Console.WriteLine("Missing argument -projectPath");
            EditorApplication.Exit(110);
        }

        if (!validatedOptions.TryGetValue("buildTarget", out var buildTarget))
        {
            Console.WriteLine("Missing argument -buildTarget");
            EditorApplication.Exit(120);
        }

        if (!Enum.IsDefined(typeof(BuildTarget), buildTarget))
        {
            EditorApplication.Exit(121);
        }

        if (!validatedOptions.TryGetValue("customBuildPath", out var customBuildPath))
        { 
            Console.WriteLine("Missing argument -customBuildPath");
            EditorApplication.Exit(130);
        }

        const string DEFAULT_CUSTOM_BUILD_NAME = "TestBuild";

        if (!validatedOptions.TryGetValue("customBuildName", out var customBuildName))
        {
            Console.WriteLine($"Missing argument -customBuildName, defaulting to {DEFAULT_CUSTOM_BUILD_NAME}.");
            validatedOptions.Add("customBuildName", DEFAULT_CUSTOM_BUILD_NAME);
        } 
        else if (customBuildName == "")
        {
            Console.WriteLine($"Invalid argument -customBuildName, defaulting to {DEFAULT_CUSTOM_BUILD_NAME}.");
            validatedOptions.Add("customBuildName", DEFAULT_CUSTOM_BUILD_NAME);
        }

        return validatedOptions;
        }

        static void ParseCommandLineArguments(out Dictionary<string, string> providedArguments)
        {
            providedArguments = new Dictionary<string, string>();
            var args = Environment.GetCommandLineArgs();

            Console.WriteLine(
            $"{Eol}" +
            $"###########################{Eol}" +
            $"#    Parsing settings     #{Eol}" +
            $"###########################{Eol}" +
            $"{Eol}"
            );

            // Extract flags with optional values
            for (int current = 0, next = 1; current < args.Length; current++, next++)
            {
                // Parse flag
                var isFlag = args[current].StartsWith("-");

                if (!isFlag) continue;

                var flag = args[current].TrimStart('-');

                // Parse optional value
                var flagHasValue = next < args.Length && !args[next].StartsWith("-");
                var value = flagHasValue ? args[next].TrimStart('-') : "";
                var secret = Secrets.Contains(flag);
                var displayValue = secret ? "*HIDDEN*" : "\"" + value + "\"";

                // Assign
                Console.WriteLine($"Found flag \"{flag}\" with value {displayValue}.");
                providedArguments.Add(flag, value);
            }
        }
    }
}
