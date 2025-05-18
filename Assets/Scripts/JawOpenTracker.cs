using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class JawOpenTracker : MonoBehaviour
{
    private ARFace face;

    // Index có thể thay đổi tùy mesh, đây là index phổ biến cho Android (ARCore)
    private int upperLipIndex = 13;
    private int lowerLipIndex = 14;

    public float jawOpenThreshold = 0.02f; 
    public float jawCloseThreshold = 0.015f;

    public ParticleSystem mouthEffect;
    public Animator animator;

    void Awake()
    {
        face = GetComponent<ARFace>();
    }

    void Update()
    {
        if (face == null || face.vertices == null || face.vertices.Length <= lowerLipIndex)
            return;

        Vector3 upperLip = face.vertices[upperLipIndex];
        Vector3 lowerLip = face.vertices[lowerLipIndex];

        float jawDistance = Vector3.Distance(upperLip, lowerLip);

        if (jawDistance > jawOpenThreshold)
        {
            if (!mouthEffect.isPlaying && mouthEffect)
                mouthEffect.Play();
            if (animator)
            {
                animator.SetBool("isJawOpen", true);
            }
        }
        else if (jawDistance < jawCloseThreshold)
        {
            if (mouthEffect.isPlaying && mouthEffect)
                mouthEffect.Stop();
            if (animator)
            {
                animator.SetBool("isJawOpen", false);
            }
        }
    }
}


