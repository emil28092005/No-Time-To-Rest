using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public LayerMask groundMask;
    public bool onGround { get; private set; } = false;

    public void OnTriggerEnter(Collider other) {
        if ((1 << other.gameObject.layer & groundMask) != 0) onGround = true;
        // onGround = true;
    }
    void OnTriggerStay(Collider other) { OnTriggerEnter(other); }
    void OnTriggerExit(Collider other) {
        if ((1 << other.gameObject.layer & groundMask) != 0) onGround = false;
        // onGround = false;
    }
}
