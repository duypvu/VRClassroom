using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WhiteboardEraser : MonoBehaviour
{
    [SerializeField] private Transform _end;
    [SerializeField] private int _penSize = 5;

    private Renderer _renderer;
    private Color[] _colors;
    private float _tipHeight;

    private RaycastHit _touch;
    private Whiteboard _whiteboard;
    private Vector2 _touchPos, _lastTouchPos;
    private bool _touchedLastFrame;
    private Quaternion _lastTouchRot;

    void Start()
    {
        // _whiteboard = GameObject.Find("_whiteboard");
        // GetComponent<Renderer>().material.color = _whiteboard.GetComponent<Renderer>().material.GetColor("_Color");
        _renderer = _end.GetComponent<Renderer>();
        _colors = Enumerable.Repeat(Color.white, _penSize * _penSize).ToArray();
        _tipHeight = _end.localScale.y;
    }

    void Update()
    {
        Erase();
    }

    private void Erase()
    {
        if(Physics.Raycast(_end.position, transform.up, out _touch, _tipHeight))
        {
            if(_touch.transform.CompareTag("Whiteboard"))
            {
                if(_whiteboard == null)
                {
                    _whiteboard = _touch.transform.GetComponent<Whiteboard>();
                }

                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);
                var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (_penSize/2));
                var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (_penSize/2));
                if(y<0 || y>_whiteboard.textureSize.y || x<0 || x>_whiteboard.textureSize.x) return;

                if(_touchedLastFrame)
                {
                    _whiteboard.texture.SetPixels(x, y, _penSize, _penSize, _colors);
                    for(float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                        _whiteboard.texture.SetPixels(lerpX, lerpY, _penSize, _penSize, _colors);
                    }
                    transform.rotation = _lastTouchRot;
                    _whiteboard.texture.Apply();
                }
                _lastTouchPos = new Vector2(x, y);
                _lastTouchRot = transform.rotation;
                _touchedLastFrame = true;
                return;
            }
        }
        _whiteboard = null;
        _touchedLastFrame = false;
    }
}

