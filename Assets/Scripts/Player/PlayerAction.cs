using Bank;
using DG.Tweening;
using OnlineLeaderboards;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using static UnityEditor.Progress;

namespace Player
{
    public class PlayerAction : MonoBehaviour, IInteractable, IInit<Die>
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private float speed = 5f;
        [SerializeField] private Score score;
        [SerializeField] private Death deathBank;
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private MeshCollider meshCollider;
        private PositionController _positionController;

        public bool _isPaused;
        private bool _isDead;
        protected ChangeMesh _changeMesh;
        private float _currentSpeed;
        private event Die _die;
        private bool _isClone;
        private List<PlayerAction> _clones;
        public event UnityAction<Mesh> PlayerChangedMesh;
        public event UnityAction<Vector3> PlayerChangedPosition;
        public event UnityAction DestroyPlayer;
        public event UnityAction ÑollideWithHintTrigger;
        public event UnityAction<bool> PlayerPause;
        public event UnityAction PlayerReborned;

        private Reborn _reborn;

        private PauseDelegate _pauseDelegate;
        private Vector3 _direction;

        public Reborn RebornDelegate => _reborn;
        public PauseDelegate PauseDelegate => _pauseDelegate;
        public Score Score => score;
        public Death DeathBank => deathBank;

        public void Construct(PositionController positionController, bool isPaused, bool isDead,
            PauseDelegate pauseDelegate, Die die, List<PlayerAction> clones)
        {
            _positionController = positionController;
            _isPaused = isPaused;
            _isDead = isDead;
            _die = die;
            _isClone = true;
            _clones = clones;
        }

        void Awake()
        {
            if (_clones == null)
                _clones = new();
            _changeMesh = new ChangeMesh(meshCollider, meshFilter);
            _direction = Vector3.forward;
            _pauseDelegate += Pause;
            _reborn += Reborn;
        }

        void FixedUpdate()
        {
            if (_isDead || _isPaused) _currentSpeed = 0;
            else _currentSpeed = speed;
            rigidbody.velocity = _direction * _currentSpeed;
        }
        public void OnÑollideWithHintTrigger()
        {
            ÑollideWithHintTrigger?.Invoke();
        }
        public void AddScore(int value = 1)
        {
            score.Add(value);
        }

        public void Die(bool isCallFromOriginal)
        {
            if (isCallFromOriginal)
            {
                foreach (var item in _clones)
                {
                    item.Die(false);
                }
            }
            meshCollider.enabled = false;
            _isDead = true;
            _isPaused = true;
            if (!_isClone)
            {
                deathBank.Add(1);
                _die?.Invoke();
            }
        }

        public void RemoveFromClones(PlayerAction player)
        {
            _clones.Remove(player);
        }

        public void RemoveEverywhere()
        {
            foreach (var item in _clones)
            {   
                item.RemoveFromClones(this);
            }
        }

        public void Pause()
        {
            if (!_isClone)
            {
                foreach (var item in _clones)
                {
                    item.Pause();
                }
            }
            _isPaused = !_isPaused;
            PlayerPause?.Invoke(_isPaused);
            Debug.Log(name);
        }

        public void Reborn()
        {
            if (!_isClone)
            {
                foreach (var item in _clones)
                {
                    item.Reborn();
                }
            }
            _isPaused = false;
            _isDead = false;
            transform.DOMoveZ(transform.position.z - 15, 0.5f).OnKill(()=> meshCollider.enabled = true);
        }


        public void Initialize(Die @delegate)
        {
            _die += @delegate;
        }

        public void SetPositionController(PositionController positionControl)
        {
            _positionController = positionControl;
        }

        public void SetPosition(int key)
        {
            var position = _positionController.GetPosition(key).transform.position;
            transform.DOMoveX(position.x, 0.5f);
            transform.DOMoveY(position.y, 0.5f);
            PlayerChangedPosition?.Invoke(position);
        }

        private void OnDestroy()
        {
            DestroyPlayer?.Invoke();
            DestroyPlayer = null;
            PlayerChangedMesh = null;
            PlayerChangedPosition = null;
        }

        public void SetMesh(Mesh newMesh)
        {
            _changeMesh.SetMesh(newMesh);
            PlayerChangedMesh?.Invoke(newMesh);
        }

        public IInteractable Clone(Mesh newMesh, int key)
        {
            var clone = Instantiate(this);
            List<PlayerAction> clonsAndPlayer = new();

            foreach (var item in _clones)
            {
                if (item != clone)
                {
                    clonsAndPlayer.Add(item);
                }
            }

            clonsAndPlayer.Add(this);
            clone.Construct(_positionController, _isPaused, _isDead, _pauseDelegate, _die, clonsAndPlayer);
            clone.SetPosition(key);
            clone.SetMesh(newMesh);
            
            clone.DestroyPlayer = null;
            clone.PlayerChangedMesh = null;
            clone.PlayerChangedPosition = null;
            _clones.Add(clone);
            return clone;
        }
    }

    public delegate void Reborn();

    public delegate void Die();
}