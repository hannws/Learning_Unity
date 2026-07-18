using UnityEngine;
using UnityEngine.InputSystem;

public class FlipperController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool isLeftFlipper;
    [SerializeField] private float restAngle = 0f;
    [SerializeField] private float activeAngle = 55f;
    [SerializeField] private float rotateSpeed = 700f;

    private PinballInput input;

    private bool isPressed;

    private void Awake()
    {
        input = new PinballInput();
    }

    private void OnEnable()
    {
        input.Enable();

        if (isLeftFlipper)
        {
            input.Gameplay.LeftFlipper.started += _ => isPressed = true;
            input.Gameplay.LeftFlipper.canceled += _ => isPressed = false;
        }
        else
        {
            input.Gameplay.RightFlipper.started += _ => isPressed = true;
            input.Gameplay.RightFlipper.canceled += _ => isPressed = false;
        }
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Update()
    {
        float targetAngle = isPressed ? activeAngle : restAngle;

        Quaternion targetRotation =
            Quaternion.Euler(0, targetAngle, 0);

        transform.localRotation = Quaternion.RotateTowards(
            transform.localRotation,
            targetRotation,
            rotateSpeed * Time.deltaTime);
    }
}