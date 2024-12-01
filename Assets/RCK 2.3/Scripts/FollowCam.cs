using UnityEngine;

public class SmoothCameraSwitch : MonoBehaviour
{
    public Transform targetTrout; // Outside Ÿ��
    public Transform targetTrin;  // Inside Ÿ��
    public float transitionSpeed = 3.0f; // ��ȯ �ӵ�
    public float insideFOV = 46f;  // Inside ���¿����� FOV
    public float outsideFOV = 60f; // Outside ���¿����� FOV
    public GameObject player;
    private Camera cam; // ī�޶� ������Ʈ
    private Transform camTransform;
    private Transform currentTarget; // ���� Ÿ��
    private Transform nextTarget;    // ���� Ÿ��
    private bool isSwitching = false; // ��ȯ ������ Ȯ��
    private float targetFOV; // ���� ��ǥ�� �ϴ� FOV ��

    void Start()
    {
        player.GetComponent<VehicleControl>().movement = true;
        camTransform = GetComponent<Transform>();
        cam = GetComponent<Camera>();

        // �ʱ� Ÿ�ٰ� FOV ����
        currentTarget = targetTrout; // �ʱ� ���´� Inside
        nextTarget = targetTrin;
        targetFOV = insideFOV;

        if (cam != null)
        {
            cam.fieldOfView = insideFOV; // �ʱ� FOV ����
        }
    }

    void Update()
    {
        // C Ű�� ������ �� ��ȯ ����
        if (Input.GetKeyDown(KeyCode.C) && !isSwitching)
        {
            isSwitching = true;

            // Ÿ�� ��ȯ
            Transform temp = currentTarget;
            currentTarget = nextTarget;
            nextTarget = temp;

            // FOV ��ȯ
            targetFOV = (currentTarget == targetTrin) ? outsideFOV : insideFOV;
        }
    }

    void LateUpdate()
    {
        if (currentTarget == null)
        {
            Debug.LogWarning("Ÿ���� �������� �ʾҽ��ϴ�!");
            return;
        }

        if (isSwitching)
        {
            // Ÿ���� ��ġ�� �ε巴�� �̵�
            camTransform.position = Vector3.Lerp(camTransform.position,
                currentTarget.position, Time.deltaTime * transitionSpeed);

            // Ÿ���� �������� �ε巴�� ȸ��
            Quaternion targetRotation = Quaternion.LookRotation(currentTarget.forward);
            camTransform.rotation = Quaternion.Slerp(camTransform.rotation, targetRotation, Time.deltaTime * transitionSpeed);

            // FOV ��ȯ ó��
            if (cam != null)
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * transitionSpeed);
            }

            // ��ȯ �Ϸ� üũ
            if (Vector3.Distance(camTransform.position, currentTarget.position) < 0.1f &&
                Quaternion.Angle(camTransform.rotation, targetRotation) < 1.0f &&
                Mathf.Abs(cam.fieldOfView - targetFOV) < 0.1f)
            {
                isSwitching = false; // ��ȯ �Ϸ�
            }
        }
        else
        {
            // ���� Ÿ�� �������� ����
            camTransform.position = currentTarget.position;
            camTransform.rotation = Quaternion.LookRotation(currentTarget.forward);

            // FOV ��� ����
            if (cam != null)
            {
                cam.fieldOfView = targetFOV;
            }
        }
    }
}
