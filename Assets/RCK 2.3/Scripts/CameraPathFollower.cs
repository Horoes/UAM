using UnityEngine;

public class CameraPathFollower : MonoBehaviour
{
    public Transform[] targets; // 추적할 타겟 오브젝트 배열
    public float moveSpeed = 5.0f; // 카메라의 이동 속도
    public float rotationSpeed = 0.1f; // 회전 속도 (조절 가능)
    public float delayTime = 1.0f; // 시작 지연 시간
    private float startTime; // 실제 움직임을 시작할 시간
    private int currentTargetIndex = 0; // 현재 타겟 인덱스
    private Quaternion originalRotation; // 초기 회전값
    private Quaternion targetRotation; // 최종 목표 회전
    public SmoothCameraSwitch cameraSwitchScript; // SmoothCameraSwitch 스크립트 참조

    void Start()
    {
        originalRotation = transform.rotation; // 초기 회전값 저장
        targetRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, -90, 0)); // 최종 목표 회전값 설정
        startTime = Time.time + delayTime; // 시작 지연 시간 설정
        cameraSwitchScript.enabled = false; // 초기에는 SmoothCameraSwitch 비활성화
    }

    void FixedUpdate()
    {
        if (targets.Length == 0 || Time.time < startTime) return; // 타겟이 없거나 시작 지연 시간이 지나지 않았다면 아무 작업도 하지 않음

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
            Invoke("ActivateCameraSwitch", 1.0f); // 마지막 타겟 도달 후 1초 뒤에 활성화
            this.enabled = false; // 이 스크립트 비활성화
        }
    }

    void ActivateCameraSwitch()
    {
        cameraSwitchScript.enabled = true; // SmoothCameraSwitch 스크립트를 활성화
    }
}
