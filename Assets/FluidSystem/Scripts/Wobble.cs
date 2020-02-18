using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{

    [SerializeField] private float MaxWobble = 0.03f;
    [SerializeField] private float WobbleSpeed = 1f;
    [SerializeField] private float Recovery = 1f;
    
    private Renderer rend;
    private Vector3 lastPos;
    private Vector3 velocity;
    private Vector3 lastRot;  
    private Vector3 angularVelocity;
    
    private float wobbleAmountX;
    private float wobbleAmountZ;
    private float wobbleAmountToAddX;
    private float wobbleAmountToAddZ;
    
    private float pulse;
    private float time = 0.5f;
    
    private MeshRenderer _mesh;

    private Transform wobbleTransform;
    
    
    // Use this for initialization
    void Start()
    {
        wobbleTransform = transform;
        _mesh = GetComponent<MeshRenderer>();
        rend = GetComponent<Renderer>();
    }
    
    private void Update()
    {
        time += Time.deltaTime;

        MakeASineWave();
        
        SetShadersFloat();
        
        CalculateVelocity();
        
        AddClampedVelocityToWobble();
        
        KeepLastPosition();

        DecreaseWobbleOverTime();
    }

    public void SetRecovery(float recovery)
    {
        Recovery = recovery;
    }

    public float GetRecovery()
    {
        return Recovery;
    }

    private void DecreaseWobbleOverTime()
    {
        wobbleAmountToAddX = Mathf.Lerp(wobbleAmountToAddX, 0, Time.deltaTime * (Recovery));
        wobbleAmountToAddZ = Mathf.Lerp(wobbleAmountToAddZ, 0, Time.deltaTime * (Recovery));
    }

    private void MakeASineWave()
    {
        pulse = 2 * Mathf.PI * WobbleSpeed;
        wobbleAmountX = wobbleAmountToAddX * Mathf.Sin(pulse * time);
        wobbleAmountZ = wobbleAmountToAddZ * Mathf.Sin(pulse * time);
    }

    private void SetShadersFloat()
    {
        rend.material.SetFloat("_WobbleX", wobbleAmountX);
        rend.material.SetFloat("_WobbleZ", wobbleAmountZ);
    }

    private void CalculateVelocity()
    {
        velocity = (lastPos - wobbleTransform.position) / Time.deltaTime;
        angularVelocity = wobbleTransform.rotation.eulerAngles - lastRot;
    }

    private void AddClampedVelocityToWobble()
    {
        wobbleAmountToAddX += Mathf.Clamp((velocity.x + (angularVelocity.z * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);
        wobbleAmountToAddZ += Mathf.Clamp((velocity.z + (angularVelocity.x * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);

    }
        
    private void KeepLastPosition()
    {
        lastPos = wobbleTransform.position;
        lastRot = wobbleTransform.rotation.eulerAngles;
    }



}