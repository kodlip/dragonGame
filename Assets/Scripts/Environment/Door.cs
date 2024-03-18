using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Vector3 _startPosition;
    private bool _isOpened;
    private void Start()
    {
        _startPosition = transform.position;
    }

    public void OpenDoor()
    {
        if (_isOpened) return;
        _isOpened = true;
        var position = new Vector3(_startPosition.x, _startPosition.y + 7, _startPosition.z);
        transform.DOMove(position, 1f);
    }

    public void CloseDoor()
    {
        if (!_isOpened) return;
        transform.DOMove(_startPosition, 1f);
        _isOpened = false;
    }
}
