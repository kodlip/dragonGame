using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateForDoor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _disabledSprite;
    [SerializeField] private Sprite _enabledSprite;
    [SerializeField] private Door _door;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out EarthCube earthCube))
        {
            _spriteRenderer.sprite = _enabledSprite;
            if (_door != null)
            {
                _door.OpenDoor();
            }
        }
    }
    
     private void OnTriggerExit2D(Collider2D col)
     {
         _spriteRenderer.sprite = _disabledSprite;
         if (col.TryGetComponent(out EarthCube earthCube))
         {
             if (_door != null)
             {
                 _door.CloseDoor();
             }
         }
     }
}
