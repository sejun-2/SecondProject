using UnityEngine;

public class ParallExBackground : MonoBehaviour
{
    [SerializeField] private Transform cam; // ī�޶�
    [SerializeField] private float parallexEffectMultiplier = 0.5f; // �з����� ����

    private Vector3 previousCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        previousCamPos = cam.position;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = cam.position - previousCamPos;
        transform.position -= new Vector3(deltaMovement.x * parallexEffectMultiplier, deltaMovement.y * parallexEffectMultiplier, 0);
        previousCamPos = cam.position;
    }
}
