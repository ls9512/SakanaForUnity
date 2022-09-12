using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Aya.Sakana
{
    public class Sakana : MonoBehaviour
    {
        [Header("Component")]
        public RectTransform TargetRect;
        public RectTransform FixedPointRect;
        public AudioClip AudioClip;

        [Header("Param")]
        public float Freq;
        public float Decay;

        [NonSerialized] public UIMouseListener MouseListener;
        [NonSerialized] public LineRenderer LineRenderer;
        [NonSerialized] public AudioSource AudioSource;
        [NonSerialized] public bool IsRunning;

        private bool _isMouseDown;
        private Vector3 _startPos;
        private Vector3 _playPos;
        private Vector3 _initPos;
        private float _strength;
        private float _counter;

        public void Awake()
        {
            MouseListener = GetComponentInChildren<UIMouseListener>();
            LineRenderer = GetComponentInChildren<LineRenderer>();
            AudioSource = GetComponentInChildren<AudioSource>();

            MouseListener.OnDown += OnPointerDown;
            MouseListener.OnUp += OnPointerUp;
        }

        public void Start()
        {
            _initPos = TargetRect.anchoredPosition3D;
            UpdateLine();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isMouseDown = true;
            _startPos = MousePointToAnchoredPosition(Input.mousePosition);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isMouseDown = false;
            var endPos = MousePointToAnchoredPosition(Input.mousePosition);
            Play(endPos);
        }

        public void Play()
        {
            var randRange = Screen.width / 2f;
            var playPos = TargetRect.anchoredPosition3D + new Vector3(Random.Range(-randRange, randRange), Random.Range(-randRange, randRange), 0f);
            Play(playPos);
        }

        public void Play(Vector3 playPos)
        {
            _playPos = playPos;
            _strength = (_playPos - _initPos).magnitude / 10f;
            IsRunning = true;
            _counter = 0f;

            PlayAudio();
        }

        public void Update()
        {
            var deltaTime = Time.deltaTime;
            var mouseOffset = _startPos - _initPos;
            if (_isMouseDown)
            {
                var currentMousePos = MousePointToAnchoredPosition(Input.mousePosition);
                UpdateState(currentMousePos - mouseOffset);
            }
            else if (IsRunning)
            {
                _counter += Decay * deltaTime;
                if (_counter >= _strength)
                {
                    _counter = _strength;
                    IsRunning = false;
                }

                var timeFactor = _counter / _strength;
                var shakeFactor = Mathf.Sin(Mathf.PI / 2f + _counter * Freq) * Mathf.Lerp(1f, 0f, timeFactor);
                var pos = Vector3.LerpUnclamped(_initPos, _playPos - mouseOffset, shakeFactor);
                pos = Vector3.LerpUnclamped(pos, _initPos, timeFactor);
                UpdateState(pos);
            }
        }

        public void UpdateState(Vector3 currentPos)
        {
            TargetRect.anchoredPosition3D = currentPos;

            var direction = TargetRect.position - FixedPointRect.position;
            var up = FixedPointRect.transform.up;
            var angle = Vector3.SignedAngle(up, direction, Vector3.forward);
            TargetRect.localEulerAngles = new Vector3(0f, 0f, angle);

            UpdateLine();
        }

        public void UpdateLine()
        {
            if (LineRenderer == null) return;
            var lineCount = 10;
            var up = FixedPointRect.transform.up;
            var height = (TargetRect.position - FixedPointRect.position).magnitude;
            LineRenderer.positionCount = lineCount;
            for (var pointIndex = 0; pointIndex < lineCount; pointIndex++)
            {
                var lineFactor = pointIndex * 1f / (lineCount - 1);
                LineRenderer.SetPosition(pointIndex, Bezier(
                    TargetRect.position,
                    FixedPointRect.position + up * height * 0.6f,
                    FixedPointRect.position + up * height * 0.3f,
                    FixedPointRect.position, lineFactor
                ));
            }
        }

        public void PlayAudio()
        {
            if (AudioClip == null || AudioSource == null) return;
            AudioSource.PlayOneShot(AudioClip);
        }

        protected Vector3 MousePointToAnchoredPosition(Vector3 mousePosition)
        {
            var result = mousePosition - new Vector3(Screen.width, Screen.height, 0f) / 2f;
            return result;
        }

        protected static Vector3 Bezier(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            var p0p1 = (1f - t) * p0 + t * p1;
            var p1p2 = (1f - t) * p1 + t * p2;
            var p2p3 = (1f - t) * p2 + t * p3;
            var p0p1p2 = (1f - t) * p0p1 + t * p1p2;
            var p1p2p3 = (1f - t) * p1p2 + t * p2p3;
            var result = (1f - t) * p0p1p2 + t * p1p2p3;
            return result;
        }
    }
}