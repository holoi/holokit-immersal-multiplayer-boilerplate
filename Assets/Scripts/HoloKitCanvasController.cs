using System.Collections.Generic;
using UnityEngine;
using HoloInteractive.XR.HoloKit;
using Immersal.AR;

/// <summary>
/// This script is responsible for deactivating canvases when switching to stereo mode.
/// </summary>
public class HoloKitCanvasController : MonoBehaviour
{
    [SerializeField] List<GameObject> m_Canvases;

    private void Start()
    {
        FindObjectOfType<HoloKitCameraManager>().OnScreenRenderModeChanged += OnScreenRenderModeChanged;
    }

    private void OnScreenRenderModeChanged(ScreenRenderMode renderMode)
    {
        foreach (var canvas in m_Canvases)
        {
            canvas.SetActive(renderMode != ScreenRenderMode.Stereo);
        }

        if (renderMode == ScreenRenderMode.Mono)
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }

    public void OnChangeScreenOrientation()
    {
        if (Screen.orientation == ScreenOrientation.Portrait)
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        else
            Screen.orientation = ScreenOrientation.Portrait;
    }
}
