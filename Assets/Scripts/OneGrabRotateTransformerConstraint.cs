using Oculus.Interaction;
using UnityEngine;
public class OneGrabRotateTransformerConstraint : MonoBehaviour
{

    [SerializeField] private new Collider collider;

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
    private OneGrabRotateTransformer _oneGrabRotateTransformer;

    private void Awake()
    {
        _oneGrabRotateTransformer = GetComponent<OneGrabRotateTransformer>();
    }


    // Update is called once per frame
    private void Update()
    {
        if (maxConstraintTransform)
        {
            if (axis == EulerAngles.X)
                _oneGrabRotateTransformer.Constraints.MaxAngle.Value = maxConstraintTransform.localEulerAngles.x - angleDeltaWithConstraintElement;
            else if (axis == EulerAngles.Y)
                _oneGrabRotateTransformer.Constraints.MaxAngle.Value = maxConstraintTransform.localEulerAngles.y - angleDeltaWithConstraintElement;
            else if (axis == EulerAngles.Z)
                _oneGrabRotateTransformer.Constraints.MaxAngle.Value = maxConstraintTransform.localEulerAngles.z - angleDeltaWithConstraintElement;
        }

        if (minConstraintTransform)
        {
            if (axis == EulerAngles.X)
                _oneGrabRotateTransformer.Constraints.MinAngle.Value = minConstraintTransform.localEulerAngles.x + angleDeltaWithConstraintElement;
            else if (axis == EulerAngles.Y)
                _oneGrabRotateTransformer.Constraints.MinAngle.Value = minConstraintTransform.localEulerAngles.y + angleDeltaWithConstraintElement;
            else if (axis == EulerAngles.Z)
                _oneGrabRotateTransformer.Constraints.MinAngle.Value = minConstraintTransform.localEulerAngles.z + angleDeltaWithConstraintElement;
        }


        if (preventGrabIfAmplitudeIsLow && collider.enabled)
        {
            if (_oneGrabRotateTransformer.Constraints.MaxAngle.Value - _oneGrabRotateTransformer.Constraints.MinAngle.Value < preventGrabTolerance)
            {
                collider.enabled = false;
            }
        }
        if (preventGrabIfAmplitudeIsLow && !collider.enabled)
        {
            if (_oneGrabRotateTransformer.Constraints.MaxAngle.Value - _oneGrabRotateTransformer.Constraints.MinAngle.Value > preventGrabTolerance)
            {
                collider.enabled = true;
            }
        }
    }

    private enum EulerAngles
    {
        X,
        Y,
        Z
    }
}