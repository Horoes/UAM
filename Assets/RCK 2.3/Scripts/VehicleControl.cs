using UnityEngine;

public class VehicleControl : MonoBehaviour
{
    public Transform Steer;
    public float speed = 100.0f;
    public float climbRate = 1f;
    public float descentRate = 20.0f;
    public float turnSpeed = 700.0f;
    public float maxSteerAngle = 30.0f;
    private float steer = 0;
    private float accel = 0.0f;
    public bool brake;
    public bool isLifted = false;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Update()
    {
        steer = Mathf.MoveTowards(steer, Input.GetAxis("Horizontal"), 0.2f);
        accel = Input.GetAxis("Vertical");
        brake = Input.GetButton("Jump");
    }

    void FixedUpdate()
    {
        if (!brake)
        {
            Vector3 force = transform.forward * accel * speed;
            rb.AddForce(force, ForceMode.Acceleration);
        }

        if (brake)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.fixedDeltaTime * 5.0f);
        }

        float steerAngle = steer * maxSteerAngle;
        Steer.localRotation = Quaternion.Euler(0, steerAngle, 0);

        // 상하 이동에 관계없이 항상 회전 처리
        Quaternion targetRotation = Quaternion.Euler(0, rb.rotation.eulerAngles.y + steer * turnSpeed * Time.fixedDeltaTime, 0);
        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * 5);

        HandleElevation();
    }

    void HandleElevation()
    {
        if (Mathf.Abs(steer) >= 0.8 && brake)
        {
            float verticalDirection = steer > 0 ? 1 : -1;
            Vector3 targetVelocity = new Vector3(rb.velocity.x, climbRate * verticalDirection, rb.velocity.z);
            rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, Time.fixedDeltaTime * 5);

            float targetPitch = verticalDirection * -30;
            Quaternion targetRotation = Quaternion.Euler(targetPitch, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 2);
        }
        else
        {
            // 항상 기존 회전 각도를 유지하며 상하 이동만 조절
            Vector3 targetVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, Time.fixedDeltaTime * 5);
        }
    }

}
