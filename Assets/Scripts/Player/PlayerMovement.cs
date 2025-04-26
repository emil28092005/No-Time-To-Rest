using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Horizontal movement")]
    public float speed = 1f;
    public float sprintSpeedMultiplier = 2f;
    public float crouchSpeedMultiplier = 0.5f;
    public float crouchHeightMultiplier = 0.5f;
    [Header("Vertical movement")]
    public float jumpForce = 1f;
    public float gravityMultiplier = 1f;

    [Header("Debug info")]
    [SerializeField] bool isCrouching = false;
    [SerializeField] Vector3 gravityVelocity;
    [SerializeField] bool grounded = false;

    CharacterController characterController;
    GroundDetector ground;

    void Start() {
        characterController = GetComponent<CharacterController>();
        ground = GetComponentInChildren<GroundDetector>();
    }

    void Update() {
        grounded = ground.onGround;
        HandleCrouching();
        HandleMovement();
        HandleJumping();
        DoGravity();
    }

    void HandleCrouching() {
        if (Input.GetKeyDown(KeyCode.C)) {
            isCrouching = !isCrouching;
            if (isCrouching) {
                characterController.center += Vector3.down * (characterController.height * (1 - crouchHeightMultiplier)) / 2;
                characterController.height *= crouchHeightMultiplier;
            } else {
                characterController.height /= crouchHeightMultiplier;
                characterController.center -= Vector3.down * (characterController.height * (1 - crouchHeightMultiplier)) / 2;
            }
        }
    }

    void HandleJumping() {
        if (Input.GetKey(KeyCode.Space) && grounded) {
            gravityVelocity = Vector3.up * (jumpForce * gravityMultiplier);
        }
    }

    void HandleMovement() {
        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");
        Vector3 movementVec = new(horInput, 0, verInput);
        movementVec = transform.TransformDirection(movementVec);
        movementVec = Vector3.ClampMagnitude(movementVec * speed, speed) * Time.deltaTime;
        float modifier = isCrouching ? crouchSpeedMultiplier : Input.GetKey(KeyCode.LeftShift) ? sprintSpeedMultiplier : 1;

        characterController.Move(movementVec * modifier);
    }

    void DoGravity() {
        if (grounded && gravityVelocity.y < 0) gravityVelocity = Vector3.zero;
        gravityVelocity += Physics.gravity * Time.deltaTime;
        characterController.Move(gravityVelocity * (Time.deltaTime * gravityMultiplier));
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        hit.gameObject.SendMessage("ReverseOnControllerColliderHit", hit, SendMessageOptions.DontRequireReceiver);
    }
}
