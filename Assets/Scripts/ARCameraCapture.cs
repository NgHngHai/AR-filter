using UnityEngine;
using System.IO;
using System.Collections;
using System;

public class ARCameraCapture : MonoBehaviour
{
    public Camera arCamera; // Kéo AR Camera vào đây
    public int width = 1080;
    public int height = 1920;

    public FlashEffect flashEffect;

    public void CapturePhoto()
    {
        if (flashEffect != null)
            flashEffect.PlayFlash();

        StartCoroutine(CaptureRoutine());
    }

    private IEnumerator CaptureRoutine()
    {
        yield return new WaitForEndOfFrame();

        // Tạo RenderTexture
        RenderTexture rt = new RenderTexture(width, height, 24);
        arCamera.targetTexture = rt;
        arCamera.Render();

        RenderTexture.active = rt;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        // Reset lại camera
        arCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        // Lưu ảnh tạm để ghi vào Gallery
        string filename = "AR_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string path = Path.Combine(Application.temporaryCachePath, filename);
        File.WriteAllBytes(path, tex.EncodeToPNG());

        // Ghi ảnh vào Gallery
        NativeGallery.SaveImageToGallery(path, "AR Captures", filename);

        Destroy(tex);
    }
}
