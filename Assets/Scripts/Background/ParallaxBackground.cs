using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

namespace Background
{
    public class ParallaxBackground : MonoBehaviour
    {
        [SerializeField] private float parallaxEffect;
        
        private Camera _camera;
        private float _xPosition;
        private float _lenght;
        private void Start()
        {
            _camera = Camera.main;
            _lenght = GetComponent<SpriteRenderer>().bounds.size.x;
            _xPosition = transform.position.x;
        } 

        private void Update()
        {
            var cameraPosition = _camera.transform.position;
            var distanceMoved = cameraPosition.x * (1 - parallaxEffect);
            var distanceToMove = cameraPosition.x * parallaxEffect;
            transform.position = new Vector3(_xPosition + distanceToMove, transform.position.y);

            if (distanceMoved > _xPosition + _lenght)
                _xPosition += _lenght;
            else if (distanceMoved < _xPosition - _lenght)
                _xPosition -= _lenght;
        }
    }
} 