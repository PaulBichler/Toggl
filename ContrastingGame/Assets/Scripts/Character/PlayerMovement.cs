using System;
using System.Collections;
using Game;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Rigidbody2D),typeof(DistanceJoint2D))]
    public sealed class PlayerMovement : MonoBehaviour
    {
        public CharacterController2D controller2D;
        public Animator animator;
        private float _horizontalMove = 0f;
        private Vector2 _force = Vector2.zero;
        public float moveSpeed = 5;
        private bool _jump = false;
        [Header("Hook")]
        private DistanceJoint2D _distanceJoint2D;
        private Vector3 _targetPos;
        private RaycastHit2D _hit2D;
        public float maxDistance = 10f;
        public LayerMask collisionLayer;
        public LineRenderer line;
        private Rigidbody2D _rb;
        private Vector2 _dragDirection;
        public float dragForce = 10f;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsJumping = Animator.StringToHash("IsJumping");
        private static readonly int YVelocity = Animator.StringToHash("yVelocity");
        private static readonly int IsGrappling = Animator.StringToHash("IsGrappling");
        private static int _finishTimer = 0;
        private Vector2 _grappleStartPoint = Vector2.zero;
        public float grappleRange = 6f;
        private AudioSource _source;
        public AudioClip levelComplete;
        public AudioClip jump;
        public AudioClip fall;
        public AudioClip grapple;
        public AudioClip fallDeath;
        public AudioClip swap;
        
        private void Start()
        {
            Physics2D.autoSimulation = true;
            this._distanceJoint2D = GetComponent<DistanceJoint2D>();
            this._rb = GetComponent<Rigidbody2D>();
            this._distanceJoint2D.enabled = false;
            this.line.enabled = false;
            _source = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                LevelState.PauseMenu.SetActive(true);
            }
            
            if (_distanceJoint2D.distance > 1f)
            {
                _distanceJoint2D.distance = 0;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _targetPos = LevelState.MainCamera.ScreenToWorldPoint(Input.mousePosition);
                _targetPos.z = 0;
                var position = transform.position;
                _grappleStartPoint = position;
                _hit2D = Physics2D.Raycast(position, _targetPos - position, maxDistance, collisionLayer);

                if (_hit2D.collider != null && _hit2D.collider.gameObject.GetComponent<Rigidbody2D>() != null && Vector2.Distance(_hit2D.transform.position, transform.position) <= grappleRange)
                {
                    _source.PlayOneShot(grapple);
                    animator.SetBool(IsGrappling, true);
                    _distanceJoint2D.enabled = true;
                    _distanceJoint2D.connectedBody = _hit2D.collider.GetComponent<Rigidbody2D>();
                    _distanceJoint2D.distance = Vector2.Distance(position, _hit2D.point);
                    _dragDirection = (Vector2)(_hit2D.transform.position - transform.position).normalized;
                    line.enabled = true;
                    line.SetPosition(0,position);
                    line.SetPosition(1,_hit2D.point);
                }
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                line.SetPosition(0,transform.position);
            }
            
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if(_distanceJoint2D.isActiveAndEnabled) AddGrapplingForce();
            }
            
            _horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
            animator.SetFloat(Speed, Mathf.Abs(_horizontalMove));
            if (Input.GetButtonDown("Jump"))
            {
                _source.PlayOneShot(jump);
                _jump = true;
                animator.SetBool(IsJumping, _jump);
            }
            
            animator.SetFloat(YVelocity, _rb.velocity.y);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Hook") && _distanceJoint2D.isActiveAndEnabled)
            {
                AddGrapplingForce();
            }
            if (other.gameObject.CompareTag("Finish"))
            {
                _finishTimer++;
            }
            else
            {
                _finishTimer = 0;
            }
        }


        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Finish"))
            {
                _finishTimer++;
                if (_finishTimer > 25)
                {
                    _source.PlayOneShot(levelComplete);
                    LevelState.Completed();
                }
            }
        }

        private void FixedUpdate()
        {
            controller2D.Move(_horizontalMove * Time.deltaTime, false, _jump);
            _jump = false;
            
        }

        private void AddGrapplingForce()
        {
            float distance = Vector2.Distance(_grappleStartPoint, transform.position);
            _rb.AddForce((Time.deltaTime * dragForce * distance) * _dragDirection, ForceMode2D.Impulse);
            controller2D.force = true;
            _distanceJoint2D.enabled = false;
            line.enabled = false;
        }

        public void PlayDeathAnim()
        {
            _source.PlayOneShot(fallDeath);
            Physics2D.autoSimulation = false;
            animator.Play("player_death");
        }

        public void Die()
        {
            LevelState.Die();
        }
    }
}
