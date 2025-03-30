using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool onGround { get; private set; } = false;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Ground")) onGround = true;
    }
    void OnTriggerStay(Collider other) { OnTriggerEnter(other); }
    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Ground")) onGround = false;
    }
}
