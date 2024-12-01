using UnityEngine;

public class CameraPathFollower : MonoBehaviour
{
    public Transform[] targets; // ������ Ÿ�� ������Ʈ �迭
    public float moveSpeed = 5.0f; // ī�޶��� �̵� �ӵ�
    public float rotationSpeed = 0.5f; // ȸ�� �ӵ�
    public float delayTime = 1.0f; // ���� ���� �ð�
    private float startTime; // ���� �������� ������ �ð�
    private int currentTargetIndex = 0; // ���� Ÿ�� �ε���
    private bool isRotating = false; // ȸ�� ������ ����
    private Quaternion targetRotation; // ��ǥ ȸ��
    public SmoothCameraSwitch cameraSwitchScript; // SmoothCameraSwitch ��ũ��Ʈ ����

    void Start()
    {
        targetRotation = transform.rotation; // �ʱ� ȸ���� ����
        startTime = Time.time + delayTime; // ���� ���� �ð� ����
        cameraSwitchScript.enabled = false; // �ʱ⿡�� SmoothCameraSwitch ��Ȱ��ȭ
    }

    void Update()
    {
        if (targets.Length == 0 || Time.time < startTime) return; // Ÿ���� ���ų� ���� ���� �ð��� ������ �ʾҴٸ� �ƹ� �۾��� ���� ����

        Transform target = targets[currentTargetIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (isRotating)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, targetRotation) < 1.0f)
            {
                transform.rotation = targetRotation;
                isRotating = false; // ȸ�� �Ϸ�
            }
        }

        if (!isRotating && Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            if (currentTargetIndex == 1)
            {
                targetRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, -90, 0));
                isRotating = true;
            }

            currentTargetIndex += 1;

            if (currentTargetIndex >= targets.Length)
            {
                enabled = false; // CameraPathFollower ��Ȱ��ȭ
                Invoke("ActivateCameraSwitch", 1.0f); // 1�� �ڿ� SmoothCameraSwitch Ȱ��ȭ
            }
        }
    }

    void ActivateCameraSwitch()
    {
        cameraSwitchScript.enabled = true; // SmoothCameraSwitch ��ũ��Ʈ�� Ȱ��ȭ
    }
}
