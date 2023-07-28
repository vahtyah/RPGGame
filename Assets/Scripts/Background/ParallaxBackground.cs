using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Background
{
    public class ParallaxBackground : MonoBehaviour
    {
        private Camera _camera;
        private float _xPosition;
        [SerializeField] private float _parallaxEffect;

        private void Start()
        {
            _camera = Camera.main;
            _xPosition = transform.position.x;
        } 

        private void Update()
        {
            var distanceToMove = _camera.transform.position.x * _parallaxEffect;
            transform.position = new Vector3(_xPosition + distanceToMove, transform.position.y);
        }
    }
} 