using UnityEngine;
using System.Collections;

namespace TestRunner
{
    [RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
    public class CharacterMovement : MonoBehaviour
    {
        private const float GroundedRadius = 0.2f;
        private const string GroundedAnimationName = "Ground";
        private const string VerticalSpeedAnimationName = "vSpeed";
        private const string SpeedAnimationName = "Speed";

        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce = 800;
        [SerializeField] private Transform _groundCheck;
        private Animator animator;
        private Rigidbody2D rigidBody;
        private bool _isGrounded;
        private float groundCheckTimer = 0.1f;
        private bool jumpOffSet;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody2D>();
        }
 
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            if (!jumpOffSet)
            {


                var colliders = Physics2D.OverlapCircleAll(_groundCheck.position, GroundedRadius);
                for (var i = 0; i < colliders.Length; ++i)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        _isGrounded = true;
                    }
                }
            }

            animator.SetBool(GroundedAnimationName, _isGrounded);
            animator.SetFloat(VerticalSpeedAnimationName, rigidBody.velocity.y);
        }

        public void Move(float move, bool jump)
        {
            if (_isGrounded)
            {
                animator.SetFloat(SpeedAnimationName, move);
                rigidBody.velocity = new Vector2(move * _speed, rigidBody.velocity.y);
            }

            if (_isGrounded && jump)
            {
                _isGrounded = false;
                animator.SetBool(GroundedAnimationName, _isGrounded);
                rigidBody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);

                jumpOffSet = true;
                Invoke("SetJumpOffSet", groundCheckTimer);
            }
        }

        private void SetJumpOffSet()
        {
            jumpOffSet = false;
        }
    }
}
