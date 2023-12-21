# HoloKit Multiplayer AR Boilerplates

## Overview

This repository provides two multiplayer AR boilerplates, serving as foundations for developing your multiplayer AR projects.

Implementing a multiplayer AR project involves addressing two critical aspects: network communication and coordinate system synchronization. Network communication is essential for exchanging data across connected devices, and coordinate system synchronization ensures that virtual content is rendered consistently in the real world locations across different devices, each with its own independent local coordinate system.

For networking, our boilerplates utilize [Unity's Netcode for GameObjects](https://docs-multiplayer.unity3d.com/netcode/current/about/) in conjunction with [Apple's Multipeer Connectivity transport package](https://github.com/Unity-Technologies/multiplayer-community-contributions/tree/main/Transports/com.community.netcode.transport.multipeer-connectivity). Netcode for GameObjects is Unity's official solution for multiplayer games, while the MultipeerConnectivity framework, the technology behind AirDrop, enables easy and stable low-latency connections between nearby Apple devices. Beyond the MultipeerConnectivity framework, we also offer a local router transport solution using [Unity Transport package](https://docs.unity3d.com/Packages/com.unity.transport@1.4/manual/index.html). This allows client devices to connect to the host by entering the host's IP address within the local network.

There are primarily two types of AR localization mechanisms: the "cold start" approach and the "absolute coordinate" approach. The "cold start" approach involves the device initiating the AR session without prior knowledge of its surroundings, requiring it to simultaneously construct a virtual map and localize itself within it. In contrast, the "absolute coordinate" approach means the device begins with complete information about its environment, focusing only on relocalizing itself within a pre-constructed map. We have a [specialized article](https://docs.holokit.io/creators/tutorials/tutorial-x-the-concept-and-implementation-of-multiplayer-ar) that explains the concept and implementation of multiplayer AR in detail.

We offer two boilerplates: one for the "cold start" approach and another for the "absolute coordinate" approach. For "cold start" cases, we use the [Image Tracking Relocalization package](https://github.com/holoi/com.holoi.xr.image-tracking-relocalization) for coordinate system synchronization. For "absolute coordinate" cases, we employ the [Immersal SDK](https://immersal.gitbook.io/sdk/), which enables users to pre-scan the environment and create point cloud map data on the cloud. This facilitates large-scale pre-scanned AR tracking.

Apart from differing in their approach to coordinate system synchronization, the two boilerplates share a majority of their codebase in other aspects.

These boilerplates are specifically designed for multiplayer AR experiences and require knowledge in networking programming, with a particular emphasis on Unity's Netcode for GameObjects. Multiplayer development fundamentally differs from single-player game development, necessitating a network-centric approach in both design and implementation. For thoese new to Netcode for GameObjects, we recommend familiarizing yourself with its [documentation](https://docs-multiplayer.unity3d.com/netcode/current/about/).

HoloKit is an optioinal tool that provides stereoscopic rendering capabilities, but you can build your mobile AR project without HoloKit using these samples. 

## Project Environment

Please be aware that these boilerplates are designed exclusively for iOS devices.

We have successfully tested them with the following software versions:

- Unity 2023.2.2f1
- Xcode 15.1 beta 3

In theory, other versions of Unity and Xcode that are close to these should also be compatible. However, if you encounter any issues during the build process, feel free to raise an issue in the repository.

## Image Tracking Relocalization Boilerplate

This boilerplate utilizes MultipeerConnectivity for its network transport layer and employs image tracking relocalization for coordinate system synchronization. The synchronization process aligns the client devices' coordinate origins with that of the host. This ensures that virtual objects, assigned specific coordinates, appear at the same physical locations on both host the client devices.

![ezgif-3-e8820418d2](https://github.com/holoi/holokit-immersal-multiplayer-boilerplate/assets/44870300/f6c6531e-8da8-4cb9-98ba-21db8bac9d9d)

The accompanying GIF demonstrates the process: two iPhones connect over a local network. The client device then scans the marker image displayed on the host's screen. After detecting a series of stable image poses, the client calculates the relative translation and rotation needed to align its local coordinate origin with the host's. Users need to verify the accuracy of this synchronization by checking if the alignment marker rendered in AR closely matches the host device's real-time physical location. Once synchronized, the HoloKit markers, intended to appear atop each connected device, will follow their respective devices as seen through the iPhone screen.

To get started, navigate to `Assets/Scenes/Multiplayer AR Boilerplate_Image Tracking Relocalization.unity` to open the scene, and then build it for your iOS devices. Familiarize yourself with the workflow and try to understand the code structure. You can then modify the code to tailor it to your project's needs.

For a detailed understanding of how the image tracking relocalization method functions, please refer to the README file of the [image tracking relocalization package](https://github.com/holoi/com.holoi.xr.image-tracking-relocalization)

## Immersal Boilerplate

## How To Choose Network Transport

## How To Choose Coordinate System Synchronization Method











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
