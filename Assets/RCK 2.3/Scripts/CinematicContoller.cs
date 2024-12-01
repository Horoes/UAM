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
        // �÷��̾� �Է� ��Ȱ��ȭ
        player.GetComponent<VehicleControl>().movement = false;

        // ī�޶�� Dolly Cart ����
        cinemachineCamera.Follow = dollyCart.transform;
        cinemachineCamera.LookAt = dollyCart.transform;

        // �ó׸�ƽ ����
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

        // �ó׸�ƽ ���� �� �÷��̾� �Է� Ȱ��ȭ
        player.GetComponent<VehicleControl>().movement = true;
    }
}
