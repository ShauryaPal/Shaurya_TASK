using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TarodevController
{
    /// <summary>
    /// VERY primitive animator example.
    /// </summary>
    public class PlayerAnimator : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private Animator _animator;

        [SerializeField] private Transform _animatedBody;

        // [Header("Settings")] [SerializeField, Range(1f, 3f)]
        // private float _maxIdleSpeed = 2;

        [SerializeField] private float _maxTilt = 5;
        [SerializeField] private float _tiltSpeed = 20;

        [Header("Particles")] [SerializeField] private ParticleSystem _jumpParticles;
        [SerializeField] private ParticleSystem _launchParticles;
        [SerializeField] private ParticleSystem _moveParticles;
        [SerializeField] private ParticleSystem _landParticles;

        [Header("Audio Clips")] [SerializeField]
        private AudioClip[] _footsteps;

        private AudioSource _source;
        private IPlayerController _player;
        private bool _grounded;
        private ParticleSystem.MinMaxGradient _currentGradient;
        private float _animatedBodyOriginalXScale;
        internal bool isPlayingUnskippableAnimation = false;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            _player = GetComponentInParent<IPlayerController>();
            _animatedBodyOriginalXScale = _animatedBody.localScale.x;
        }

        private void OnEnable()
        {
            _player.Jumped += OnJumped;
            _player.GroundedChanged += OnGroundedChanged;

            _moveParticles.Play();
        }

        private void OnDisable()
        {
            _player.Jumped -= OnJumped;
            _player.GroundedChanged -= OnGroundedChanged;

            _moveParticles.Stop();
        }

        private void Update()
        {
            if (_player == null || isPlayingUnskippableAnimation) return;

            DetectGroundColor();

            HandleSpriteFlip();

            HandleIdleSpeed();

            HandleCharacterTilt();
        }

        private void HandleSpriteFlip()
        {
            if (_player.FrameInput.x != 0) _animatedBody.localScale = new Vector3(_player.FrameInput.x < 0 ? -_animatedBodyOriginalXScale : _animatedBodyOriginalXScale, _animatedBody.localScale.y, _animatedBody.localScale.z);
        }

        private void HandleIdleSpeed()
        {
            var inputStrength = Mathf.Abs(_player.FrameInput.x);
            
            if(_grounded)
                _animator.Play(inputStrength > 0 ? WalkKey : IdleKey);
            // _anim.SetFloat(IdleSpeedKey, Mathf.Lerp(1, _maxIdleSpeed, inputStrength));
            
            _moveParticles.transform.localScale = Vector3.MoveTowards(_moveParticles.transform.localScale, Vector3.one * inputStrength, 2 * Time.deltaTime);
        }

        private void HandleCharacterTilt()
        {
            var runningTilt = _grounded ? Quaternion.Euler(0, 0, _maxTilt * _player.FrameInput.x) : Quaternion.identity;
            _animator.transform.up = Vector3.RotateTowards(_animator.transform.up, runningTilt * Vector2.up, _tiltSpeed * Time.deltaTime, 0f);
        }

        private void OnJumped()
        {
            _animator.Play(JumpKey);
            // _anim.SetTrigger(JumpKey);
            // _anim.ResetTrigger(GroundedKey);


            if (_grounded) // Avoid coyote
            {
                SetColor(_jumpParticles);
                SetColor(_launchParticles);
                _jumpParticles.Play();
            }
        }

        private void OnGroundedChanged(bool grounded, float impact)
        {
            _grounded = grounded;
            
            if (grounded)
            {
                DetectGroundColor();
                SetColor(_landParticles);

                // _anim.SetTrigger(GroundedKey);
                _animator.Play(IdleKey);
                _source.PlayOneShot(_footsteps[Random.Range(0, _footsteps.Length)]);
                _moveParticles.Play();

                _landParticles.transform.localScale = Vector3.one * Mathf.InverseLerp(0, 40, impact);
                _landParticles.Play();
            }
            else
            {
                _animator.Play(JumpKey);
                _moveParticles.Stop();
            }
        }

        private void DetectGroundColor()
        {
            var hit = Physics2D.Raycast(transform.position, Vector3.down, 2);

            if (!hit || hit.collider.isTrigger || !hit.transform.TryGetComponent(out SpriteRenderer r)) return;
            var color = r.color;
            _currentGradient = new ParticleSystem.MinMaxGradient(color * 0.9f, color * 1.2f);
            SetColor(_moveParticles);
        }

        private void SetColor(ParticleSystem ps)
        {
            var main = ps.main;
            main.startColor = _currentGradient;
        }

        public void PlayUnskippableAnimation(int animation, Action callback)
        {
            isPlayingUnskippableAnimation = true;
            StartCoroutine(Coroutine_PlayUnskippableAnimation(animation, callback));
        }

        IEnumerator Coroutine_PlayUnskippableAnimation(int animation, Action callback)
        {
            _animator.Play(animation);

            yield return new WaitForSeconds(0.1f);
            
            while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 || _animator.IsInTransition(0))
                yield return null;

            isPlayingUnskippableAnimation = false;
            callback?.Invoke();
        }

        // private static readonly int GroundedKey = Animator.StringToHash("Grounded");
        private static readonly int IdleKey = Animator.StringToHash("Idle");
        private static readonly int WalkKey = Animator.StringToHash("Walk");
        private static readonly int JumpKey = Animator.StringToHash("Jump");
        public static readonly int EatKey = Animator.StringToHash("Eat");
        public static readonly int AttackKey = Animator.StringToHash("Attack");
    }
}