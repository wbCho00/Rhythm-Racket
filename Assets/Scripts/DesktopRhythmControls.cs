using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.XR;

public class DesktopRhythmControls : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float arrowLookSpeed = 100f;
    [SerializeField] private float mouseSensitivity = 0.12f;
    [SerializeField] private float eyeHeight = 1.65f;
    [SerializeField] private float xrStartupWait = 1.5f;

    private Transform viewCamera;
    private CharacterController characterController;
    private float yaw;
    private float pitch;
    private bool desktopMode;
    private string hitMessage = "";
    private float hitMessageUntil;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void AttachToPlayerRig()
    {
        if (Application.isBatchMode)
            return;

        GameObject rig = GameObject.Find("XR Rig");
        if (rig != null && rig.GetComponent<DesktopRhythmControls>() == null)
            rig.AddComponent<DesktopRhythmControls>();
    }

    private IEnumerator Start()
    {
        // Give XR Plug-in Management time to start a connected headset.
        yield return new WaitForSecondsRealtime(xrStartupWait);

        if (XRSettings.isDeviceActive)
        {
            Destroy(this);
            yield break;
        }

        EnableDesktopMode();
    }

    private void EnableDesktopMode()
    {
        Camera cameraComponent = GetComponentInChildren<Camera>(true);
        if (cameraComponent == null)
        {
            Debug.LogWarning("Desktop controls could not find a camera under XR Rig.");
            enabled = false;
            return;
        }

        viewCamera = cameraComponent.transform;
        yaw = transform.eulerAngles.y;
        pitch = 0f;

        DisableXRTracking();
        SetDesktopCameraHeight();
        SetupDesktopUI();

        characterController = GetComponent<CharacterController>();
        if (characterController == null)
            characterController = gameObject.AddComponent<CharacterController>();

        characterController.height = 1.8f;
        characterController.radius = 0.3f;
        characterController.center = new Vector3(0f, 0.9f, 0f);
        characterController.stepOffset = 0.3f;
        characterController.slopeLimit = 45f;
        characterController.skinWidth = 0.08f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        desktopMode = true;
    }

    private void DisableXRTracking()
    {
        foreach (Behaviour behaviour in GetComponentsInChildren<Behaviour>(true))
        {
            if (behaviour != this && behaviour.GetType().Name == "TrackedPoseDriver")
                behaviour.enabled = false;
        }

        string[] desktopHiddenObjects =
        {
            "LeftHand Controller",
            "RightHand Controller",
            "LeftHand Ray",
            "RightHand Ray"
        };

        Transform[] children = GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            for (int i = 0; i < desktopHiddenObjects.Length; i++)
            {
                if (child.name == desktopHiddenObjects[i])
                {
                    child.gameObject.SetActive(false);
                    break;
                }
            }
        }
    }

    private void SetDesktopCameraHeight()
    {
        Transform cameraOffset = viewCamera.parent;
        if (cameraOffset != null && cameraOffset.name == "Camera Offset")
        {
            Vector3 offset = cameraOffset.localPosition;
            offset.y = eyeHeight;
            cameraOffset.localPosition = offset;
        }
    }

    private static void SetupDesktopUI()
    {
        EventSystem eventSystem = EventSystem.current;
        if (eventSystem == null)
            eventSystem = Object.FindObjectOfType<EventSystem>();

        if (eventSystem == null)
            eventSystem = new GameObject("Desktop Event System").AddComponent<EventSystem>();

        foreach (BaseInputModule module in eventSystem.GetComponents<BaseInputModule>())
            module.enabled = module is InputSystemUIInputModule;

        InputSystemUIInputModule inputModule = eventSystem.GetComponent<InputSystemUIInputModule>();
        if (inputModule == null)
            inputModule = eventSystem.gameObject.AddComponent<InputSystemUIInputModule>();

        if (inputModule.actionsAsset == null)
            inputModule.AssignDefaultActions();

        inputModule.enabled = true;
    }

    private void Update()
    {
        if (!desktopMode)
            return;

        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
            return;

        float deltaTime = Time.deltaTime;
        float lookHorizontal = 0f;
        float lookVertical = 0f;

        if (keyboard.leftArrowKey.isPressed)
            lookHorizontal -= 1f;
        if (keyboard.rightArrowKey.isPressed)
            lookHorizontal += 1f;
        if (keyboard.upArrowKey.isPressed)
            lookVertical -= 1f;
        if (keyboard.downArrowKey.isPressed)
            lookVertical += 1f;

        yaw += lookHorizontal * arrowLookSpeed * deltaTime;
        pitch += lookVertical * arrowLookSpeed * deltaTime;

        Mouse mouse = Mouse.current;
        if (mouse != null && mouse.rightButton.isPressed)
        {
            Vector2 mouseDelta = mouse.delta.ReadValue();
            yaw += mouseDelta.x * mouseSensitivity;
            pitch -= mouseDelta.y * mouseSensitivity;
        }

        pitch = Mathf.Clamp(pitch, -80f, 80f);
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        viewCamera.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        MovePlayer(keyboard, deltaTime);

        if (keyboard.fKey.wasPressedThisFrame)
            TryHitLane(1);
        if (keyboard.vKey.wasPressedThisFrame)
            TryHitLane(2);
        if (keyboard.jKey.wasPressedThisFrame)
            TryHitLane(3);
        if (keyboard.nKey.wasPressedThisFrame)
            TryHitLane(4);
    }

    private void MovePlayer(Keyboard keyboard, float deltaTime)
    {
        Vector2 input = Vector2.zero;
        if (keyboard.aKey.isPressed)
            input.x -= 1f;
        if (keyboard.dKey.isPressed)
            input.x += 1f;
        if (keyboard.sKey.isPressed)
            input.y -= 1f;
        if (keyboard.wKey.isPressed)
            input.y += 1f;

        input = Vector2.ClampMagnitude(input, 1f);
        Vector3 movement = transform.right * input.x + transform.forward * input.y;
        characterController.Move(movement * moveSpeed * deltaTime);
    }

    private void TryHitLane(int lane)
    {
        BallCollisionHandler[] balls = FindObjectsOfType<BallCollisionHandler>();
        BallCollisionHandler bestBall = null;
        int bestScore = 0;
        float bestDistance = float.PositiveInfinity;

        foreach (BallCollisionHandler ball in balls)
        {
            int score = ball.GetDesktopHitScore(lane, out float distance);
            if (score > bestScore || (score == bestScore && score > 0 && distance < bestDistance))
            {
                bestBall = ball;
                bestScore = score;
                bestDistance = distance;
            }
        }

        if (bestBall != null)
        {
            bestBall.RegisterDesktopHit(bestScore);
            hitMessage = bestScore >= 100 ? "PERFECT  +100" : "GOOD  +50";
        }
        else
        {
            hitMessage = "MISS";
        }

        hitMessageUntil = Time.unscaledTime + 0.65f;
    }

    private void OnGUI()
    {
        if (!desktopMode)
            return;

        const float panelWidth = 430f;
        const float panelHeight = 72f;
        float x = 14f;
        float y = Screen.height - panelHeight - 14f;

        GUI.Box(new Rect(x, y, panelWidth, panelHeight), GUIContent.none);
        GUI.Label(new Rect(x + 12f, y + 8f, panelWidth - 24f, 24f),
            "WASD: Move    Hold Right Mouse + Drag / Arrow Keys: Look");
        GUI.Label(new Rect(x + 12f, y + 34f, panelWidth - 24f, 24f),
            "Hit: F Upper Left   V Lower Left   J Upper Right   N Lower Right");

        if (Time.unscaledTime < hitMessageUntil)
            GUI.Label(new Rect(x + panelWidth + 12f, y + 20f, 180f, 28f), hitMessage);
    }
}