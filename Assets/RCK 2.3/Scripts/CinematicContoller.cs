using UnityEngine;
using Cinemachine;
using System.Collections;

public class CinematicController : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineCamera;
    public GameObject player;
    public CinemachineDollyCart dollyCart;

    private void Start()
    {
        // 플레이어 입력 비활성화
        player.GetComponent<VehicleControl>().movement = false;

        // 카메라와 Dolly Cart 설정
        cinemachineCamera.Follow = dollyCart.transform;
        cinemachineCamera.LookAt = dollyCart.transform;

        // 시네마틱 시작
        StartCoroutine(PlayCinematic());
    }

    IEnumerator PlayCinematic()
    {
        float duration = 10.0f;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            dollyCart.m_Position = Mathf.Lerp(0, dollyCart.m_Path.PathLength, elapsedTime / duration);
            yield return null;
        }

        // 시네마틱 종료 후 플레이어 입력 활성화
        player.GetComponent<VehicleControl>().movement = true;
    }
}
