# Immersal HoloKit Samples

## Overview

This repository provides sample projects illustrating the integration of the Immersal SDK with the [HoloKit SDK](https://github.com/holoi/holokit-unity-sdk) in Unity. While the Immersal SDK offers localization features, the HoloKit SDK brings to the table stereoscopic rendering and interaction capabilities.

This repository includes two distinct sample scenes:

1. **Stereo Rendering Sample**: Demonstrates the integration of HoloKit SDK's stereoscopic rendering with the Immersal SDK.

   <img width="500" alt="stereo rendering" src="https://github.com/holoi/immersal-holokit-samples/assets/44870300/aea5e3f3-6714-408e-8e15-a6e2062abdaa">

3. **Multiplayer AR Sample**: Showcases how to set up a multiplayer AR session using Unity's Netcode for GameObjects package with the [MultipperConnecvitity transport](https://github.com/Unity-Technologies/multiplayer-community-contributions/tree/main/Transports/com.community.netcode.transport.multipeer-connectivity).

<img width="200" alt="multiplayer ar" src="https://github.com/holoi/immersal-holokit-samples/assets/44870300/2a180887-96af-4c4f-bb51-417491fd8bca">

## Compatibility

This project was built with Unity 2022.3.7f1.

## Building the Sample Scenes 

### Stereo Rendering

- **Location**: Navigate to `Assets/HoloKit Sample Scenes/Immersal_HoloKit_StereoRendering.unity`.
  
- **Setup**:
    - Download your specific Immersal map file from the Immersal Developer Portal.
    - Import the map into your Unity project.
    - Drag and drop the map file onto the `Map File` slot of the `ARMap` GameObject present in the scene.

   <img width="700" alt="ARMap" src="https://github.com/holoi/immersal-holokit-samples/assets/44870300/084bc554-5aaa-4bdc-a574-4e3d54ccb5e6">

   Before building, ensure that `Immersal_HoloKit_StereoRendering` is the sole selected scene for build.

   <img width="560" alt="scene selection" src="https://github.com/holoi/immersal-holokit-samples/assets/44870300/2ff9c6b2-655e-4a84-b633-b6003ab7660d">

### Multiplayer AR

- **Location**: Navigate to `Assets/HoloKit Sample Scenes/Immersal_HoloKit_MultiplayerAR.unity`.

- **Setup**:
    - As before, download your Immersal map file from the Immersal Developer Portal.
    - Import it into Unity.
    - Drag and position the map file in the `Map File` slot of the `ARMap` GameObject in the scene.

   <img width="700" alt="ARMap" src="https://github.com/holoi/immersal-holokit-samples/assets/44870300/4aa55c1c-84c8-4ec3-b939-470895705b46">

   When building, ensure `Immersal_HoloKit_MultiplayerAR` is the only selected scene.

   <img width="560" alt="scene selection" src="https://github.com/holoi/immersal-holokit-samples/assets/44870300/b356475c-a365-4bd5-a696-7aac0ffef86b">
