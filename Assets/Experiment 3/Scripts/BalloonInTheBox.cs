using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonInTheBox : MonoBehaviour, IObjectExperiment
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _speedPosition;
    [SerializeField] private float _speedRotation;
    [SerializeField] private float _speedScale;
    private bool _isEnd = false;
    private Vector3 _endPosition = new Vector3(-0.007f,0.39f,0.002f);
    private Vector3 _endRotation = Vector3.zero;
    private Vector3 _endScale = Vector3.one;
    
    private Vector3 _startPosition = new Vector3(-0.039f, -0.254f, -0.109f);
    private Vector3 _startRotation = new Vector3(270,270,0);
    private Vector3 _startScale = new Vector3(-0.27f, 0.38f, 0.1f);
    
    public void Play()
    {
        StartCoroutine(TransformBalloon(_endPosition, _endRotation, _endScale, true));
    }

    private IEnumerator TransformBalloon(Vector3 position, Vector3 rotation, Vector3 scale, bool endFlag)
    {
        bool isPlay = true;
        while (isPlay)
        {
            _transform.localPosition =
                Vector3.MoveTowards(_transform.localPosition, position, Time.deltaTime * _speedPosition);
            _transform.localRotation = Quaternion.RotateTowards(_transform.localRotation, Quaternion.Euler(rotation),
                Time.deltaTime * _speedRotation);
            _transform.localScale = Vector3.MoveTowards(_transform.localScale, scale, Time.deltaTime * _speedScale);
            
            if (Vector3.Distance(_transform.localPosition, position) <= 0 &&
                Vector3.Distance(_transform.localEulerAngles, rotation) <= 0 &&
                Vector3.Distance(_transform.localScale, scale) <= 0)
            {
                _transform.localPosition = position;
                _transform.localRotation = Quaternion.Euler(rotation);
                _transform.localScale = scale;
                _isEnd = endFlag;
                isPlay = false;
            }
            yield return new WaitForSeconds(0);
        }
    }
    
    public void Back()
    {
        StartCoroutine(TransformBalloon(_startPosition, _startRotation, _startScale, false));
    }

    public bool IsEnd()
    {
        return _isEnd;
    }
}
