using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Vector3 _defaultAngle = new Vector3(50,180,0);
    private Vector3 _topDownAngle = new Vector3(90, 180, 0);
    private Vector2 _defaultOffset = new Vector2(30, 25);
    private Vector2 _topDownOffset = new Vector2(35, 0);
    private bool isDefault = true;
    public Vector2 _offset;
    public Vector3 _angle;

    private void Start()
    {
        _offset = _defaultOffset;
        _angle = _defaultAngle;
    }

    public void toggleCamera()
    {
        if (isDefault)
        {
            isDefault = false;
            _offset = _topDownOffset;
            _angle = _topDownAngle;
        }
        else
        {
            isDefault = true;
            _offset = _defaultOffset;
            _angle = _defaultAngle;
        }

        transform.eulerAngles = _angle;

    }
}
