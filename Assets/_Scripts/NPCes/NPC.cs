using System.Collections;
using _Scripts.DialogSystem;
using _Scripts.Player.Interaction;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField]
    private TextAsset _inkJsonAsset;

    [SerializeField] 
    private Transform[] _path;

    [SerializeField] 
    private NavMeshAgent _agent;

    [SerializeField] private float _rotationSpeed;
    
    public bool IsTalking { get; set; }

    public bool IsMoving
    {
        get => !_agent.isStopped;
        set
        {
            _agent.isStopped = !value;
        }
    }

    private Vector3 _targetDirection;
    
    private int _pathIndex;

    private void Start()
    {
        if (_path.Length == 0)
            return;
        
        _agent.SetDestination(_path[_pathIndex].position);
    }

    private void Update()
    {
        if (_path.Length == 0)
            return;
        
        if (Vector3.Distance(transform.position, _path[_pathIndex].position) < 0.1f)
        {
            _pathIndex = _pathIndex < _path.Length - 1 ? ++_pathIndex : 0;
            _agent.SetDestination(_path[_pathIndex].position);
        }
    }

    public void Interact(Interactor interactor)
    {
        if (IsTalking) return;

        IsTalking = true;
        _agent.isStopped = true;
        _targetDirection = interactor.transform.position - transform.position;
        StartCoroutine(StartDialog());
    }

    private IEnumerator StartDialog()
    {
        while (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(_targetDirection)) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(_targetDirection), _rotationSpeed * Time.deltaTime);
            yield return null;
        }

        DialogManager.Instance.NPCToTalk = this;
        DialogManager.Instance.StartDialog(_inkJsonAsset);
    }
    
    public void ToogleMovement(bool isStopped) => _agent.isStopped = isStopped;
}
