using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineMover : MonoBehaviour
{
    public rail rail;
    private int _CurrentSeg=0;
    private float _Transition;
    private bool _IsCompleted;
    private bool _CanMove = false;

    public float speed = 2.5f;
    public bool isReversed=false;
    public bool isLooping;
    public PlayMode mode;

    private void Awake()
    {
        rail = GameObject.FindGameObjectWithTag("Rail").GetComponent<rail>();
        _CurrentSeg = 0;
    }

    private void Update()
    {
        if (!rail)
            return;

        if (!_IsCompleted)
        {
            Play(!isReversed);
        }
        
    }

    private void Play(bool forward)
    {
       
        float m = (rail.Nodes[_CurrentSeg + 1].position - rail.Nodes[_CurrentSeg].position).magnitude;
        float s = (Time.deltaTime * 1 / m) * speed;
        _Transition += (forward) ? s : -s;
        
        if (_Transition > 1)
        {
            _Transition = 0;
            _CurrentSeg++;
            if (_CurrentSeg == rail.Nodes.Length - 1)
            {
                if (isLooping)
                {
                    isReversed = !isReversed;
                    _CurrentSeg = rail.Nodes.Length - 2;
                    _Transition = 1;
                }
                else
                {
                    _IsCompleted = true;
                    return;
                }
            }
        }

        else if (_Transition < 0)
        {
            _Transition = 1;
            _CurrentSeg--;
            if(_CurrentSeg==0)
            {
                isReversed = !isReversed;
            }
        }

        transform.position = rail.PositionOnRail(_CurrentSeg, _Transition, mode);
        transform.rotation = rail.Orientation(_CurrentSeg, _Transition);
    }
}
