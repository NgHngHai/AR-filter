using UnityEngine;
using System;

public class GalleryLauncher : MonoBehaviour
{
    public void OpenGallery()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        try
        {
            // Tạo Intent mới để mở ảnh
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", intentClass.GetStatic<string>("ACTION_PICK"));

            // Đặt loại dữ liệu là hình ảnh
            intent.Call<AndroidJavaObject>("setType", "image/*");

            // Lấy Activity hiện tại
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

            // Khởi chạy Activity
            currentActivity.Call("startActivity", intent);
        }
        catch (Exception e)
        {
            Debug.LogError("Lỗi khi mở Gallery: " + e.Message);
        }
#else
        Debug.Log("Android only.");
#endif
    }
}
