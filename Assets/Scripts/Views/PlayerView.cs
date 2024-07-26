using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class PlayerView : MonoBehaviour
    {
        private int animationId = Animator.StringToHash("AnimationID");

        [SerializeField]
        private float _speed = 2;

        [SerializeField]
        private float _speedUp = 2;

        [SerializeField]
        private float _rotationSpeed = 40f;

        [SerializeField]
        private Animator _controller;

        public void PlayAnimation(PlayerAnimations anim)
        {
            _controller.SetInteger(animationId, (int) anim);
        }

        public void Rotate(float direction)
        {
            transform.Rotate(Vector3.up, direction * _rotationSpeed);
        }

        public void Move(float direction, bool isSpeedUp)
        {
            var speed = isSpeedUp ? _speed + _speedUp : _speed; 
            transform.position += transform.forward * direction * speed;
        }
    }
}