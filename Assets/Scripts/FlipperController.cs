using UnityEngine;
using UnityEngine.InputSystem;

public class FlipperController : MonoBehaviour
{
    [SerializeField] private bool isLeftFlipper;

    [SerializeField] private float pressedAngle = 55f;
    [SerializeField] private float releasedAngle = 0f;

    private HingeJoint hinge;
    private JointSpring spring;

    private PinballInput input;

    private void Awake()
    {
        hinge = GetComponent<HingeJoint>();

        spring = hinge.spring;

        input = new PinballInput();
    }

    private void OnEnable()
    {
        input.Enable();

        if (isLeftFlipper)
        {
            input.Gameplay.LeftFlipper.started += _ => SetAngle(pressedAngle);
            input.Gameplay.LeftFlipper.canceled += _ => SetAngle(releasedAngle);
        }
        else
        {
            input.Gameplay.RightFlipper.started += _ => SetAngle(-pressedAngle);
            input.Gameplay.RightFlipper.canceled += _ => SetAngle(releasedAngle);
        }
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void SetAngle(float angle)
    {
        spring.targetPosition = angle;
        hinge.spring = spring;
    }
}