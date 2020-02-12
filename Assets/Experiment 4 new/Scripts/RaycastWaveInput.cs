using System.Collections;
using System.Collections.Generic;
using Es.WaveformProvider;
using UnityEngine;

public class RaycastWaveInput : MonoBehaviour
{
    [SerializeField]
    private Texture2D waveform;

    [SerializeField, Range(0f, 1f)]
    private float waveScale = 0.05f;

    [SerializeField, Range(0f, 1f)]
    private float strength = 0.1f;

    [SerializeField] private float maxRaycastDistance = 0.1f;
    
    void Update()
    {
        ThrowRaycast();   
    }

    private void ThrowRaycast()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hitInfo, maxRaycastDistance))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hitInfo.distance, Color.yellow);

            var waveObject = hitInfo.transform.GetComponent<WaveConductor>();
            if (waveObject != null)
                waveObject.Input(waveform, hitInfo, waveScale, strength);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
