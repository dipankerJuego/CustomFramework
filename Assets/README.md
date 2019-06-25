## XRCustomFramework

### Introduction
XRCustomFramework uses Unity XR API and other SDK which does not support XR API for most of the VR and MR devices.

> **Requires** the Unity version 2019.1.1f1 (or above)

### Setting up the project
 * Create a new project in the Unity software version 2018.3.10f1 (or above) using 3D Template or open an existing project.
 * Create new project and download XRCustomFramework project and copy/paste in empty project. Or copy/paste in your existing project.
 * Ensure `Virtual Reality Supported` is checked:
  * In the Unity software select `Main Menu -> Edit -> Project Settings` to open the `Project Settings` window.
  * Select `Player` from the left hand menu in the `Project Settings` window.
  * In the `Player` settings panel expand `XR Settings`.
  * In `XR Settings` ensure the `Virtual Reality Supported` option is checked.
 * Ensure the project `Scripting Runtime Version` is set to `.NET 4.x Equivalent`:
  * In the Unity software select `Main Menu -> Edit -> Project Settings` to open the `Project Settings` inspector.
  * Select `Player` from the left hand menu in the `Project Settings` window.
  * In the `Player` settings panel expand `Other Settings`.
  * Ensure the `Scripting Runtime Version` is set to `.NET 4.x Equivalent`.
 
 > Note: Unity 2019.1 requires additional project setup before importing VRTK.
 
 * Download and install the `XR Legacy Input Helpers` from the Unity Package Manager.
  * In the Unity software select `Main Menu -> Window -> Package Manager` to open the `Package Manager` window.
  * Select `XR Legacy Input Helpers` from the `Packages` tab in the `Package Manager` window.
  * Click the `Install` button located in the bottom right of the `Package Manager` window.
  * The `XR Legacy Input Helpers` package will now download and install into the project.
  
### Importing XRCustomFramework
 * Download the XRCustomFramework project and Paste inside the project `Assets/` directory.
 
### Contents
 XRCustomFramework contains following parts:
 
 * XRCustomFramework Build setting.
 * XRCustomFramework XR API