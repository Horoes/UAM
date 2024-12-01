using UnityEngine;

public class CameraPathFollower : MonoBehaviour
{
    public Transform[] targets; // 추적할 타겟 오브젝트 배열
    public float moveSpeed = 5.0f; // 카메라의 이동 속도
    public float rotationSpeed = 0.5f; // 회전 속도
    public float delayTime = 1.0f; // 시작 지연 시간
    private float startTime; // 실제 움직임을 시작할 시간
    private int currentTargetIndex = 0; // 현재 타겟 인덱스
    private bool isRotating = false; // 회전 중인지 여부
    private Quaternion targetRotation; // 목표 회전
    public SmoothCameraSwitch cameraSwitchScript; // SmoothCameraSwitch 스크립트 참조

    void Start()
    {
        targetRotation = transform.rotation; // 초기 회전값 설정
        startTime = Time.time + delayTime; // 시작 지연 시간 설정
        cameraSwitchScript.enabled = false; // 초기에는 SmoothCameraSwitch 비활성화
    }

    void Update()
    {
        if (targets.Length == 0 || Time.time < startTime) return; // 타겟이 없거나 시작 지연 시간이 지나지 않았다면 아무 작업도 하지 않음

        Transform target = targets[currentTargetIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (isRotating)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, targetRotation) < 1.0f)
            {
                transform.rotation = targetRotation;
                isRotating = false; // 회전 완료
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
                enabled = false; // CameraPathFollower 비활성화
                Invoke("ActivateCameraSwitch", 1.0f); // 1초 뒤에 SmoothCameraSwitch 활성화
            }
        }
    }

    void ActivateCameraSwitch()
    {
        cameraSwitchScript.enabled = true; // SmoothCameraSwitch 스크립트를 활성화
    }
}
