using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rod : MonoBehaviour, IObjectExperiment
{
    
    [SerializeField] private Transform _transform;
    [SerializeField] private float _speedPosition;
    
    private bool _isEnd = false;

    private Vector3 _endPosition = new Vector3(0,2.0f,0);
    private Vector3 _startPosition = new Vector3(0, 3.4f, 0);

    public void Play()
    {
        StartCoroutine(TransformRod(_endPosition, true));
    }
    
    private IEnumerator TransformRod(Vector3 position, bool endFlag)
    {
        bool isPlay = true;
        while (isPlay)
        {
            _transform.localPosition =
                Vector3.MoveTowards(_transform.localPosition, position, Time.deltaTime * _speedPosition);

            if (Vector3.Distance(_transform.localPosition, position) <= 0 )
            {
                _transform.localPosition = position;
                _isEnd = endFlag;
                isPlay = false;
            }
            yield return new WaitForSeconds(0);
        }
    }
    

    public void Back()
    {
        StartCoroutine(TransformRod(_startPosition, false));
    }

    public bool IsEnd()
    {
        return _isEnd;
    }
}
