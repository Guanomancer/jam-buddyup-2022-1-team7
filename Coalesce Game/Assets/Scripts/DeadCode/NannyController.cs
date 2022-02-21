using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Coalesce.DeadCode
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(CharacterController))]
    public class NannyController : MonoBehaviour, IEventDispatcher
    {
        [SerializeField]
        private float _reachDistance = 2.5f;
        [SerializeField]
        private float _pickupTimePerBlock = 1f;
        [SerializeField]
        private Transform _todzillaCarryAnchor;
        [SerializeField]
        private Transform _todzillaCarryCameraFocusAnchor;
        [SerializeField]
        private AutoframeCameraController _cameraFramer;
        [SerializeField]
        private GameObject _mainRoomCameraZone;
        [SerializeField]
        private AudioSource _soundScape;

        private Transform _todzillaCarryReference;
        private Transform _navigationTarget;

        private NavMeshAgent _agent;
        private CharacterController _char;

        private NavMeshPath _path;
        private NavMeshPath _currentPath;

        private bool _couldReachTodzilla;

        private PerimeterBlockDetector _blockDetector;

        public NannyAI Nanny;

        public void SetNavigationTarget(Transform navigationTarget)
        {
            _navigationTarget = navigationTarget;
            if (_navigationTarget != null)
                _agent.SetDestination(_navigationTarget.position);
        }

        public float DistanceToTarget
            => _agent.remainingDistance;

        public IEnumerator PickupBlock(BlockController block, System.Action andThen)
        {
            yield return new WaitForSeconds(_pickupTimePerBlock);
            if (block != null)
            {
                var count = 0;
                var currentBlock = block;
                do
                {
                    _blockDetector.RemoveBlockFromReachList(currentBlock);
                    DestroyImmediate(currentBlock.gameObject);
                    count++;
                    currentBlock = GetBlockClosestTo(transform.position);
                } while (currentBlock != null && count < GameManager.Instance.GameSettings.MaxBlocksToPickUpAtOnce);
            }
            andThen();
        }

        private BlockController GetBlockClosestTo(Vector3 point)
        {
            var blocks = Physics.OverlapSphere(point, GameManager.Instance.GameSettings.MessyBlockDetectionRadius);
            
            float closestSqrMagnitude = 100000f;
            int blockIndex = -1;
            int cnt = 0;
            for (int i = 0; i < blocks.Length; i++)
            {
                if (blocks[i].transform.parent == null ||
                    blocks[i].transform.parent.GetComponent<BlockController>() == null ||
                    !blocks[i].transform.parent.GetComponent<BlockController>().IsMessy()
                    )
                    continue;
                cnt++;
                float sqrMag = (blocks[i].transform.position - point).sqrMagnitude;
                if (sqrMag <= closestSqrMagnitude)
                {
                    closestSqrMagnitude = sqrMag;
                    blockIndex = i;
                }
            }
            return blockIndex == -1 ? null : blocks[blockIndex].transform.parent.GetComponent<BlockController>();
        }

        public IEnumerator PickupTodzilla(Transform todzilla, System.Action andThen)
        {
            _todzillaCarryReference = todzilla;
            _todzillaCarryReference.GetComponent<TodzillaController>().enabled = false;
            _todzillaCarryReference.GetComponent<Rigidbody>().isKinematic = true;
            _todzillaCarryReference.GetComponent<AutoframeCameraController>().SetDistantTarget(_todzillaCarryCameraFocusAnchor);
            yield return new WaitForSeconds(_pickupTimePerBlock);
            _todzillaCarryReference.parent = _todzillaCarryAnchor;
            _todzillaCarryReference.localPosition = Vector3.zero;
            _todzillaCarryReference.localRotation = Quaternion.identity;
            andThen();
        }

        public IEnumerator PutDownTodzilla(Transform dropTarget, System.Action andThen)
        {
            yield return new WaitForSeconds(_pickupTimePerBlock);
            _todzillaCarryReference.parent = null;
            _todzillaCarryReference.position = dropTarget.position + Vector3.up * 0.2f;
            _todzillaCarryReference.GetComponent<TodzillaController>().enabled = true;
            _todzillaCarryReference.GetComponent<Rigidbody>().isKinematic = false;
            _todzillaCarryReference.GetComponent<AutoframeCameraController>().SetDistantTarget(transform);
            andThen();
        }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _char = GetComponent<CharacterController>();
            _path = new NavMeshPath();
            _blockDetector = GetComponent<PerimeterBlockDetector>();
        }

        private void FixedUpdate()
        {
            RecomputeRoute();

            if (!_couldReachTodzilla && CanReacyTodzilla)
            {
                _couldReachTodzilla = true;
                EventRouter.Dispatch(EventName.TodzillaCaught, this);
            }
            else if (!CanReacyTodzilla)
                _couldReachTodzilla = false;
        }

        private void OnEnable()
        {
            SetNannyAsCameraFocus();
            EnableSoundscape();
        }

        private void EnableSoundscape()
        {
            _soundScape.Play();
        }

        private void SetNannyAsCameraFocus()
        {
            _mainRoomCameraZone.SetActive(false);
            _cameraFramer.ResetDistantTarget();
        }

        private void RecomputeRoute()
        {
            if (_navigationTarget == null)
            {
                _agent.SetDestination(transform.position);
                return;
            }

            _path.ClearCorners();
            if (_agent.CalculatePath(_navigationTarget.position, _path))
            {
                _currentPath = _path;
                _path = new NavMeshPath();
                _agent.SetPath(_currentPath);
            }
        }

        private bool CanReacyTodzilla
            => _navigationTarget != null &&
                    _navigationTarget.GetComponent<TodzillaController>() != null &&
                    Vector3.Magnitude(_navigationTarget.position - transform.position) <= _reachDistance;
    }
}
