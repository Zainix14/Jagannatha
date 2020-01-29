using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A Mettre sur Cam_Render | 
/// Capture de Cube Map | 
/// Converion en Render Texture 2D |
/// Mono / Stereo = nombres d'oeil |
/// Camera Rotation Aware |
/// Script by Leni |
/// </summary>
public class SC_RenderCubeMapToTex : MonoBehaviour
{

    public RenderTexture cubemapLeft;
    public RenderTexture cubemapRight;
    public RenderTexture equirect;
    public float f_StereoSeparation = 0.064f;

    Camera cam;

    public enum RenderType { Stereo, Mono, Both };
    public RenderType renderType = RenderType.Mono;

    public bool followCam = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Capture360();
    }

    public void Capture360()
    {
        CheckForCamera();
        CaptureCubemaps();
        ConvertCubeToEquirect();
    }

    private void CheckForCamera()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            cam = GetComponentInParent<Camera>();
        }
        if (cam == null)
        {
            Debug.LogWarning("Stereo 360 capture node has no camera or parent camera");
        }
    }

    private void CaptureCubemaps()
    {
        switch (renderType)
        {
            case RenderType.Mono:
                if (followCam)
                {
                    cam.stereoSeparation = 0f;
                    cam.RenderToCubemap(cubemapLeft, 63, Camera.MonoOrStereoscopicEye.Left); //because stereo rendering does follow camera
                }
                else
                {
                    cam.RenderToCubemap(cubemapLeft, 63, Camera.MonoOrStereoscopicEye.Mono);
                }
                break;

            case RenderType.Stereo:
                cam.stereoSeparation = f_StereoSeparation;
                cam.RenderToCubemap(cubemapLeft, 63, Camera.MonoOrStereoscopicEye.Left);
                cam.RenderToCubemap(cubemapRight, 63, Camera.MonoOrStereoscopicEye.Right);
                if (!followCam)
                {
                    Debug.LogWarning("Follow Cam is always enabled for Stereo Rendering");
                }

                break;
        }
    }

    private void ConvertCubeToEquirect()
    {
        switch (renderType)
        {
            case RenderType.Mono:
                if (followCam)
                {
                    cubemapLeft.ConvertToEquirect(equirect, Camera.MonoOrStereoscopicEye.Mono);
                }
                else
                {
                    cubemapLeft.ConvertToEquirect(equirect, Camera.MonoOrStereoscopicEye.Mono);
                }
                break;

            case RenderType.Stereo:
                cubemapLeft.ConvertToEquirect(equirect, Camera.MonoOrStereoscopicEye.Left);
                cubemapRight.ConvertToEquirect(equirect, Camera.MonoOrStereoscopicEye.Right);
                break;
        }
    }

}
