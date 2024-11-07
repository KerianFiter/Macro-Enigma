using System;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Serialization;

public class OneGrabRotateTransformerConstraint : MonoBehaviour
{
    private OneGrabRotateTransformer _oneGrabRotateTransformer;
    private float _startAngle;
    private float _startMinAngle;
    private float _startMaxAngle;

    
    [SerializeField] private Collider collider;

    [Tooltip("L'élément qui va restreindre la rotation maximum de cet élément")]
    [SerializeField] private Transform maxConstraintTransform;
    
    [Tooltip("L'élément qui va restreindre la rotation minimum de cet élément")]
    [SerializeField] private Transform minConstraintTransform;

    [Tooltip("L'axe qui va être restreint")]
    [SerializeField] private EulerAngles axis;
    
    [Tooltip("Si > 0, va empecher la superposition de l'élément avec les éléments de contrainte")]
    [SerializeField] private float angleDeltaWithConstraintElement;

    [Tooltip("Va empecher le grab si l'amplitude de rotation est trop faible")]
    [SerializeField] private bool preventGrabIfAmplitudeIsLow;
    
    [Tooltip("Tolérance pour le grab si l'amplitude de rotation est trop faible")]
    [SerializeField] private float preventGrabTolerance;

    private void Awake()
    {
        _oneGrabRotateTransformer = GetComponent<OneGrabRotateTransformer>();
    }

    private void Start()
    {
        if(axis == EulerAngles.x)
            _startAngle = transform.localEulerAngles.x;
        else if(axis == EulerAngles.y)
            _startAngle = transform.localEulerAngles.y;
        else if(axis == EulerAngles.z)
            _startAngle = transform.localEulerAngles.z;

        _startMaxAngle = _oneGrabRotateTransformer.Constraints.MaxAngle.Value;
        _startMinAngle = _oneGrabRotateTransformer.Constraints.MinAngle.Value;
    }

    private enum EulerAngles
    {
        x,
        y,
        z
    }
    

    // Update is called once per frame
    void Update()
    {
        if (maxConstraintTransform)
        {
            if(axis == EulerAngles.x)
                _oneGrabRotateTransformer.Constraints.MaxAngle.Value = maxConstraintTransform.localEulerAngles.x - angleDeltaWithConstraintElement;
            else if(axis == EulerAngles.y)
                _oneGrabRotateTransformer.Constraints.MaxAngle.Value = maxConstraintTransform.localEulerAngles.y - angleDeltaWithConstraintElement;
            else if(axis == EulerAngles.z)
                _oneGrabRotateTransformer.Constraints.MaxAngle.Value = maxConstraintTransform.localEulerAngles.z - angleDeltaWithConstraintElement;
        }
        
        if (minConstraintTransform)
        {
            if(axis == EulerAngles.x)
                _oneGrabRotateTransformer.Constraints.MinAngle.Value = minConstraintTransform.localEulerAngles.x + angleDeltaWithConstraintElement;
            else if(axis == EulerAngles.y)
                _oneGrabRotateTransformer.Constraints.MinAngle.Value = minConstraintTransform.localEulerAngles.y + angleDeltaWithConstraintElement;
            else if(axis == EulerAngles.z)
                _oneGrabRotateTransformer.Constraints.MinAngle.Value = minConstraintTransform.localEulerAngles.z + angleDeltaWithConstraintElement;
        }
        
        
        if (preventGrabIfAmplitudeIsLow && collider.enabled)
        {
            if ((_oneGrabRotateTransformer.Constraints.MaxAngle.Value - _oneGrabRotateTransformer.Constraints.MinAngle.Value) < preventGrabTolerance)
            {
                collider.enabled = false;
            }
        }
        if(preventGrabIfAmplitudeIsLow && !collider.enabled)
        {
            if ((_oneGrabRotateTransformer.Constraints.MaxAngle.Value - _oneGrabRotateTransformer.Constraints.MinAngle.Value) > preventGrabTolerance)
            {
                collider.enabled = true;
            }
        }
    }
}
