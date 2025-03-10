// Copyright Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;
using System.IO;

public class Dojo : ModuleRules
{
    protected readonly string Version = "1.0.1";
    protected string VersionPath { get => Path.Combine(ModuleDirectory, Version); }
    protected string LibraryPath { get => Path.Combine(VersionPath, "lib"); }

    public Dojo(ReadOnlyTargetRules Target) : base(Target)
    {
        PCHUsage = ModuleRules.PCHUsageMode.UseExplicitOrSharedPCHs;
        
        //Type = ModuleType.External;

        PublicSystemIncludePaths.Add(Path.Combine(VersionPath, "include"));

        PublicDependencyModuleNames.AddRange(
            new string[]
            {
                "Core",
                // ... other dependencies ...
            }
        );

        PrivateDependencyModuleNames.AddRange(
            new string[]
            {
                "CoreUObject",
                "Engine",
                // ... other dependencies ...
            }
        );

        if (Target.Platform == UnrealTargetPlatform.Mac)
        {
            string DylibPath = Path.Combine(LibraryPath, "Mac", "libdojo_c.dylib");
            PublicAdditionalLibraries.Add(DylibPath);
            
            // Add runtime dependency for Mac
            RuntimeDependencies.Add("$(PluginDir)/Source/Dojo/" + Version + "/lib/Mac/libdojo_c.dylib");
            
            // Delay load the dylib
            PublicDelayLoadDLLs.Add("libdojo_c.dylib");
        }
        else if (Target.Platform == UnrealTargetPlatform.IOS)
        {
            // Use iOS library
            PublicAdditionalLibraries.Add(Path.Combine(ModuleDirectory, Version, "lib", "IOS", "libdojo_c.a"));

            // Add runtime dependency for Mac
            //RuntimeDependencies.Add("$(PluginDir)/Source/Dojo/" + Version + "/lib/iOS/libdojo_c.a");
            
            // Delay load the dylib
            //PublicDelayLoadDLLs.Add("libdojo_c.a");
            
            
            //PublicAdditionalFrameworks.Add(
            //                new Framework(
            //                    "Dojo",                                                        // Framework name
            //                    "../../ThirdPartyFrameworks/Dojo.embeddedframework.zip"       // Zip name
            //
                    //null, true                              // Resources we need copied and staged
            //                )
            //);
            
        }
    }
}
