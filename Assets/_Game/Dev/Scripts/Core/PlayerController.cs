using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [HideInInspector] public float Speed;
    [HideInInspector] public float dragSpeed = 0f;

    [SerializeField] private Animator anim;
    [SerializeField] public Rigidbody rb;
    
    public Vector3 GetPlayerPosition() => rb.transform.position;
    
    public enum AnimationStates
    {
        Idle,
        Walk
    }
    
    AnimationStates _animState = AnimationStates.Idle;

    AnimationStates AnimState
    {
        get => _animState;
        set
        {
            if(_animState != value)
            {
                _animState = value;
                anim.SetTrigger(_animState.ToString());
            }
        }
    }
    
    public void PlayerAnimChange(AnimationStates animationStates)
    {
        AnimState = animationStates;
    }
    
    public void OnGameStarted()
    {
        rb.isKinematic = false;
    }

    private void Start()
    {
        InitPlayer();
    }

    void InitPlayer()
    {
        Speed = Configs.Player.speed;
    }

    private void Update()
    {
        if (GameManager.isRunning)
        {
            Move();
        }
    }

    public void Move()
    {
        rb.velocity = rb.transform.forward * Speed * dragSpeed;

        HandleAnimation();
    }

    void HandleAnimation()
    {
        if (dragSpeed > 0 && AnimState != AnimationStates.Walk)
        {
            AnimState = AnimationStates.Walk;
        } else if (dragSpeed == 0 && AnimState != AnimationStates.Idle)
        {
            AnimState = AnimationStates.Idle;
        }
    }
    
    public void Rotate(Quaternion rot)
    {
        rb.rotation = rot;
    }
    
    public void ForceStop()
    {
        dragSpeed = 0f;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
    }
    
}
