using UnityEngine;
using System.Collections;

namespace TestRunner
{
    public class InputController : MonoBehaviour
    {
        private CharacterMovement _characterMovement;
        private bool _jumping;

        private void Awake()
        {
            _characterMovement = GetComponent<CharacterMovement>();
        }

        private void Update()
        {
            if (!_jumping)
            {
                _jumping = Input.GetButtonDown("Jump");
            }
        }
        private void FixedUpdate()
        {
            _characterMovement.Move(1, _jumping);
            _jumping = false;
        }
    }
}
