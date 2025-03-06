using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    public float sprintSpeedMultiplier = 2f;
    public float crouchSpeedMultiplier = 0.5f;
    public float crouchHeightMultiplier = 0.5f;
    public float jumpForce = 1f;
    public float gravityMultiplier = 1f;

    Rigidbody rb;
    CharacterController characterController;
    [SerializeField] bool isCrouching = false;
    Vector3 charControllerCenter;

    void Start() {
        rb = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        charControllerCenter = characterController.center;
    }

    void Update() {
        HandleCrouching();
        HandleJumping();
        HandleMovement();
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
            if (characterController.isGrounded) rb.AddForce(Vector3.up * (characterController.height / 5), ForceMode.VelocityChange);
        }
    }

    void HandleJumping() {
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
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
        characterController.Move(rb.linearVelocity * (gravityMultiplier * Time.deltaTime));
        if (characterController.isGrounded) rb.linearVelocity *= 0.2f;
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        hit.gameObject.SendMessage("ReverseOnControllerColliderHit", hit, SendMessageOptions.DontRequireReceiver);
    }
}
