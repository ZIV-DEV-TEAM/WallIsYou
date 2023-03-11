using Bank;
using DG.Tweening;
using OnlineLeaderboards;
using UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Player
{
    public class PlayerAction : MonoBehaviour, IInteractable, IInit<Die>
    {
        public event UnityAction DestroyPlayer;

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
        public event UnityAction<Mesh> PlayerChangedMesh;
        public event UnityAction<Vector3> PlayerChangedPosition;

        private Reborn _reborn;

        private PauseDelegate _pauseDelegate;
        private Vector3 _direction;

        public Reborn RebornDelegate => _reborn;
        public PauseDelegate PauseDelegate => _pauseDelegate;
        public Score Score => score;
        public Death DeathBank => deathBank;

        public void Construct(PositionController positionController,bool isPaused, bool isDead, Die die, Reborn reborn, PauseDelegate pauseDelegate)
        {
            _positionController = positionController;
            _isPaused = isPaused;
            _isDead = isDead;
            _die = die;
            _reborn = reborn;
            _pauseDelegate = pauseDelegate;
        }

        void Awake()
        {
            _changeMesh = new ChangeMesh(meshCollider, meshFilter);
            _direction = Vector3.forward;
            _pauseDelegate = Pause;
            _reborn = Reborn;
        }

        void FixedUpdate()
        {
            if (_isDead || _isPaused) _currentSpeed = 0;
            else _currentSpeed = speed;
            rigidbody.velocity = _direction * _currentSpeed;
        }

        public void AddScore(int value = 1)
        {
            score.Add(value);
        }

        public void Die()
        {
            _isDead = true;
            deathBank.Add(1);
            _die?.Invoke();
        }

        private void Pause()
        {
            _isPaused = !_isPaused;
        }

        private void Reborn()
        {
            _isDead = false;
            transform.DOMoveZ(transform.position.z - 15, 0.5f);
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
            //transform.DORotate(new Vector3(0, transform.rotation.eulerAngles.y+180,  transform.rotation.eulerAngles.z+180), 0.5f);
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
            clone.Construct(_positionController, _isPaused, _isDead, _die, _reborn, _pauseDelegate);
            clone.SetPosition(key);
            clone.SetMesh(newMesh);
            clone.DestroyPlayer = null;
            clone.PlayerChangedMesh = null;
            clone.PlayerChangedPosition = null;
            return clone;
        }
    }
    
    public delegate void Reborn();

    public delegate void Die();
}