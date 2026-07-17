using UnityEngine;
using UnityEngine.InputSystem;

public class LauncherController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform handle;
    [SerializeField] private Transform ballSocket;
    [SerializeField] private Transform launchDirection;
    [SerializeField] private Rigidbody ball;

    [Header("Launcher Settings")]
    [SerializeField] private float pullDistance = 0.6f;
    [SerializeField] private float pullSpeed = 8f;
    [SerializeField] private float launchForce = 25f;

    private PinballInput input;

    private Vector3 handleStartPos;
    private Vector3 socketStartPos;

    private bool isPulling;

    private void Awake()
    {
        input = new PinballInput();

        handleStartPos = handle.localPosition;
        socketStartPos = ballSocket.localPosition;

        ball.isKinematic = true;
    }

    private void OnEnable()
    {
        input.Enable();

        input.Gameplay.Launch.started += OnLaunchStarted;
        input.Gameplay.Launch.canceled += OnLaunchReleased;
    }

    private void OnDisable()
    {
        input.Gameplay.Launch.started -= OnLaunchStarted;
        input.Gameplay.Launch.canceled -= OnLaunchReleased;

        input.Disable();
    }

    private void Update()
    {
        Vector3 handleTarget = handleStartPos;
        Vector3 socketTarget = socketStartPos;

        if (isPulling)
        {
            handleTarget += Vector3.back * pullDistance;
            socketTarget += Vector3.back * pullDistance;
        }

        handle.localPosition = Vector3.Lerp(
            handle.localPosition,
            handleTarget,
            pullSpeed * Time.deltaTime);

        ballSocket.localPosition = Vector3.Lerp(
            ballSocket.localPosition,
            socketTarget,
            pullSpeed * Time.deltaTime);
    }

    private void OnLaunchStarted(InputAction.CallbackContext context)
    {
        isPulling = true;
    }

    private void OnLaunchReleased(InputAction.CallbackContext context)
    {
        isPulling = false;

        ball.transform.SetParent(null);

        ball.isKinematic = false;

        ball.linearVelocity = Vector3.zero;
        ball.angularVelocity = Vector3.zero;

        ball.AddForce(
            launchDirection.forward * launchForce,
            ForceMode.Impulse);
    }
}