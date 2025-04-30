using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Horizontal movement")]
    public float speed = 5f;
    public float dashVelocity = 10f;
    public float dashTime = 0.5f;
    [Header("Vertical movement")]
    public float jumpForce = 1f;
    public float gravityMultiplier = 1f;

    [Header("Debug info")]
    [SerializeField] Vector3 gravityVelocity;
    [SerializeField] bool grounded = false;
    [SerializeField] float movementInactive = 0;
    [SerializeField] float gravityInactive = 0;
    [SerializeField] Vector3 constantVelocity = Vector3.zero;
    [SerializeField] float constantVelocityActive = 0;

    CharacterController characterController;
    GroundDetector ground;

    void Start() {
        characterController = GetComponent<CharacterController>();
        ground = GetComponentInChildren<GroundDetector>();
    }

    void Update() {
        grounded = ground.onGround;
        HandleJumping();
        HandleMovement();
        HandleDashing();
        DoGravity();
        HandleConstantVelocity();
    }

    void HandleJumping() {
        if (movementInactive > 0) return;
        if (Input.GetKey(KeyCode.Space) && grounded) {
            gravityVelocity = Vector3.up * jumpForce;
        }
    }

    void HandleMovement() {
        if (movementInactive > 0) {
            movementInactive = Mathf.Max(movementInactive -= Time.deltaTime, 0);
            return;
        }
        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");
        Vector3 movementVec = new(horInput, 0, verInput);
        movementVec = transform.TransformDirection(movementVec);
        movementVec = Vector3.ClampMagnitude(movementVec * speed, speed);
        characterController.Move(movementVec * Time.deltaTime);
    }

    void HandleDashing() {
        if (Input.GetKeyDown(KeyCode.LeftShift) && constantVelocityActive == 0) {
            float horInput = Input.GetAxis("Horizontal");
            float verInput = Input.GetAxis("Vertical");
            Vector3 movementVec = new(horInput, 0, verInput);
            if (movementVec.magnitude < 0.1f) return;
            constantVelocity = transform.TransformDirection(movementVec.normalized * dashVelocity);
            constantVelocityActive = dashTime;
            SetInactive(dashTime, dashTime);
        }
    }

    void DoGravity() {
        if (gravityInactive > 0) {
            gravityVelocity = Vector3.zero;
            gravityInactive = Mathf.Max(gravityInactive -= Time.deltaTime, 0);
            return;
        }
        if (grounded && gravityVelocity.y < 0) gravityVelocity = Vector3.zero;
        gravityVelocity += Physics.gravity * (Time.deltaTime * gravityMultiplier);
        characterController.Move(gravityVelocity * Time.deltaTime);
    }

    void HandleConstantVelocity() {
        if (constantVelocityActive > 0) {
            characterController.Move(constantVelocity * Time.deltaTime);
            constantVelocityActive = Mathf.Max(constantVelocityActive - Time.deltaTime, 0);
        }
    }

    void SetInactive(float movement, float gravity) {
        movementInactive = movement;
        gravityInactive = gravity;
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        hit.gameObject.SendMessage("ReverseOnControllerColliderHit", hit, SendMessageOptions.DontRequireReceiver);
    }
}
