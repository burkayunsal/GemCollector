
using DG.Tweening;
using MyBox;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] Transform target;
    [SerializeField] bool useEditorOffset;
    [ConditionalField(nameof(useEditorOffset), false, false)]
    [SerializeField] Vector3 offset;
    bool isInitialized = false;
    public void SetOffset(Vector3 endVal) => offset = endVal;
    public void SetOffset(Vector3 endVal, float time)
    {
        DOTween.To(() => offset, (x) => offset = x, endVal, time).SetEase(Ease.Linear);
    }
    

    delegate void OnUpdate();
    OnUpdate onUpdate;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        if (target == null) return;

        if (useEditorOffset)
        {
            SetOffset(transform.position - target.position);
        }

        isInitialized = true;
    }

    public void LateUpdate()
    {
        if (isInitialized)
        {
            onUpdate?.Invoke();
        }
    }

    public void OnGameStarted()
    {
        onUpdate = FollowTarget;
    }

    void FollowTarget()
    {
        if (target)
        {
            transform.position = target.position + offset;
        }
    }
}
