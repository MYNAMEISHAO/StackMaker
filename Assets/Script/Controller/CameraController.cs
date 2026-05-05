using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 offSet;
    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + offSet;
    }
}
