# DotnetToolbar

![image](https://user-images.githubusercontent.com/97656046/282954887-1d691cb1-b24b-4827-be11-c96cd83d5a12.png)

Welcome to DotnetToolbar, the ultimate Visual Studio 2022 extension that brings the power of dotnet commands right to your fingertips, revolutionizing the way you manage .NET projects and aligning seamlessly with CI/CD workflows

## Description
In modern development environments, .NET projects are often built using CI/CD practices. Visual Studio's MSBuild can different results, making it essential to have a toolbar that replicates the CI/CD pipeline within the Visual Studio environment. This toolbar facilitates the direct execution of dotnet commands, simplifying development.

With the release of dotnet 6, publishing has evolved significantly from traditional .NET Framework builds. Specific publish settings are required for the desired output. The toolbar's main purpose is to streamline this process by seamlessly integrating with properly configured project files, enabling swift execution of these dotnet commands.

Additionally, the toolbar supports packaging projects using the 'dotnet pack' command and efficiently pushing NuGet updates with 'dotnet nuget push.' For enhanced security and convenience, it leverages the Windows Credential Manager for secure key storage, eliminating manual key management.

This Toolbar focuses on optimizing .NET development workflows, ensuring consistency and efficiency throughout (and without) the CI/CD pipeline within Visual Studio.

The project works in conjuction with [Coree.Template.Project](https://github.com/carsten-riedel/Coree.Template.Project) i have created with preset csproj settings. 
Use the Coree Project Templates with preset csproj settings (edit them) and than quickly pack/push or publish your builds.
(with this approches in conjunction you will be able to create a single file executable within minutes)

## Preperation (Prerequirments)
  
### Windows Setup

Normal install and download procedure.
  1. [Download/Install Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
  2. For development `Visual Studio extension development` need to be checked in the Visual Studio installer.

# Install DotnetToolbar Visual Studio Extension

## Manual download from Visual Studio Marketplace
[Marketplace > Visual Studio > Tools > DotnetToolbar](https://marketplace.visualstudio.com/items?itemName=Coree-CarstenRiedel.CoreeDotnetToolbar)

![image](https://github.com/carsten-riedel/Coree.VisualStudio.DotnetToolbar/assets/97656046/0e21991a-6a0b-4098-92d9-bc9b10d97e0e)

![image](https://github.com/carsten-riedel/Coree.VisualStudio.DotnetToolbar/assets/97656046/99aace5b-00de-4419-bd04-5d2e7f46dff3)


## Visual Studio 2022 IDE Install
![image](https://user-images.githubusercontent.com/97656046/285142705-52f226a2-1e5d-4093-9e6a-7402e6b43870.png)

# Usage
Directly execute dotnet commands from the toolbar, with the option to customize command-line parameters in the settings menu.
The configuration settings are integrated with the standard configuration manager.

![image](https://user-images.githubusercontent.com/97656046/285170297-32269767-d4ba-4eca-89b5-48592c6706ba.png)

![image](https://user-images.githubusercontent.com/97656046/285170963-95286bbc-0097-4e02-a0e1-3f51afeaad75.png)

## Educational

For more information and resources for Visual Studio Extensibility:
  - [MS Learn: Visual Studio Extensibility](https://learn.microsoft.com/en-US/visualstudio/extensibility/?view=vs-2022)
  - [MS Learn: Visual Studio Community Toolkit](https://learn.microsoft.com/en-us/visualstudio/extensibility/vsix/visual-studio-community-toolkit?view=vs-2022)
  - [Github: VSSDK-Extensibility](https://github.com/microsoft/VSExtensibility)
  - [Github: VSSDK-Extensibility-Samples](https://github.com/Microsoft/VSSDK-Extensibility-Samples)

