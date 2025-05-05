using UnityEngine;
using System.IO;
using System.Collections;

public class ARCameraCapture : MonoBehaviour
{
    public Camera arCamera;  // ← Drag AR Camera từ AR Session Origin vào đây
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

        // Reset
        arCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        // Lưu file
        string path = Path.Combine(Application.persistentDataPath, "AR_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png");
        File.WriteAllBytes(path, tex.EncodeToPNG());
        Debug.Log("Ảnh đã lưu: " + path);

        Destroy(tex);
    }
}
