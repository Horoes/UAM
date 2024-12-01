using UnityEngine;

public class SmoothCameraSwitch : MonoBehaviour
{
    public Transform targetTrout; // Outside 타겟
    public Transform targetTrin;  // Inside 타겟
    public float transitionSpeed = 3.0f; // 전환 속도
    public float insideFOV = 46f;  // Inside 상태에서의 FOV
    public float outsideFOV = 60f; // Outside 상태에서의 FOV
    public GameObject player;
    private Camera cam; // 카메라 컴포넌트
    private Transform camTransform;
    private Transform currentTarget; // 현재 타겟
    private Transform nextTarget;    // 다음 타겟
    private bool isSwitching = false; // 전환 중인지 확인
    private float targetFOV; // 현재 목표로 하는 FOV 값

    void Start()
    {
        player.GetComponent<VehicleControl>().movement = true;
        camTransform = GetComponent<Transform>();
        cam = GetComponent<Camera>();

        // 초기 타겟과 FOV 설정
        currentTarget = targetTrout; // 초기 상태는 Inside
        nextTarget = targetTrin;
        targetFOV = insideFOV;

        if (cam != null)
        {
            cam.fieldOfView = insideFOV; // 초기 FOV 설정
        }
    }

    void Update()
    {
        // C 키를 눌렀을 때 전환 시작
        if (Input.GetKeyDown(KeyCode.C) && !isSwitching)
        {
            isSwitching = true;

            // 타겟 전환
            Transform temp = currentTarget;
            currentTarget = nextTarget;
            nextTarget = temp;

            // FOV 전환
            targetFOV = (currentTarget == targetTrin) ? outsideFOV : insideFOV;
        }
    }

    void LateUpdate()
    {
        if (currentTarget == null)
        {
            Debug.LogWarning("타겟이 설정되지 않았습니다!");
            return;
        }

        if (isSwitching)
        {
            // 타겟의 위치로 부드럽게 이동
            camTransform.position = Vector3.Lerp(camTransform.position,
                currentTarget.position, Time.deltaTime * transitionSpeed);

            // 타겟의 방향으로 부드럽게 회전
            Quaternion targetRotation = Quaternion.LookRotation(currentTarget.forward);
            camTransform.rotation = Quaternion.Slerp(camTransform.rotation, targetRotation, Time.deltaTime * transitionSpeed);

            // FOV 전환 처리
            if (cam != null)
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * transitionSpeed);
            }

            // 전환 완료 체크
            if (Vector3.Distance(camTransform.position, currentTarget.position) < 0.1f &&
                Quaternion.Angle(camTransform.rotation, targetRotation) < 1.0f &&
                Mathf.Abs(cam.fieldOfView - targetFOV) < 0.1f)
            {
                isSwitching = false; // 전환 완료
            }
        }
        else
        {
            // 현재 타겟 기준으로 고정
            camTransform.position = currentTarget.position;
            camTransform.rotation = Quaternion.LookRotation(currentTarget.forward);

            // FOV 즉시 적용
            if (cam != null)
            {
                cam.fieldOfView = targetFOV;
            }
        }
    }
}
