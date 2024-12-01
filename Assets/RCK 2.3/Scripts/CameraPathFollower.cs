using UnityEngine;

public class CameraPathFollower : MonoBehaviour
{
    public Transform[] targets; // ������ Ÿ�� ������Ʈ �迭
    public float moveSpeed = 5.0f; // ī�޶��� �̵� �ӵ�
    public float rotationSpeed = 0.1f; // ȸ�� �ӵ� (���� ����)
    public float delayTime = 1.0f; // ���� ���� �ð�
    private float startTime; // ���� �������� ������ �ð�
    private int currentTargetIndex = 0; // ���� Ÿ�� �ε���
    private Quaternion originalRotation; // �ʱ� ȸ����
    private Quaternion targetRotation; // ���� ��ǥ ȸ��
    public SmoothCameraSwitch cameraSwitchScript; // SmoothCameraSwitch ��ũ��Ʈ ����

    void Start()
    {
        originalRotation = transform.rotation; // �ʱ� ȸ���� ����
        targetRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, -90, 0)); // ���� ��ǥ ȸ���� ����
        startTime = Time.time + delayTime; // ���� ���� �ð� ����
        cameraSwitchScript.enabled = false; // �ʱ⿡�� SmoothCameraSwitch ��Ȱ��ȭ
    }

    void FixedUpdate()
    {
        if (targets.Length == 0 || Time.time < startTime) return; // Ÿ���� ���ų� ���� ���� �ð��� ������ �ʾҴٸ� �ƹ� �۾��� ���� ����

        if (currentTargetIndex < targets.Length)
        {
            Transform target = targets[currentTargetIndex];
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                currentTargetIndex++;
            }
        }
        else
        {
            Invoke("ActivateCameraSwitch", 1.0f); // ������ Ÿ�� ���� �� 1�� �ڿ� Ȱ��ȭ
            this.enabled = false; // �� ��ũ��Ʈ ��Ȱ��ȭ
        }
    }

    void ActivateCameraSwitch()
    {
        cameraSwitchScript.enabled = true; // SmoothCameraSwitch ��ũ��Ʈ�� Ȱ��ȭ
    }
}
