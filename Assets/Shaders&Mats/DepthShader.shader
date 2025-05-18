Shader "Custom/DepthShader"
{
    SubShader
    {
        Tags { "Queue" = "Geometry-1" }

        // Vô hình nhưng vẫn ghi vào depth buffer
        Pass
        {
            ColorMask 0    // Không render ra màn hình (invisible)
            ZWrite On      // Ghi vào depth buffer
        }
    }

    FallBack Off
}
