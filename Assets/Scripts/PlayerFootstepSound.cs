using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFootstepSound : MonoBehaviour
{
    [Header("Player 상태")]
    public PlayerState playerState;

    [Header("발걸음 소리")]
    public AudioSource footstepSource;
    public AudioClip outsideFootstep;
    public AudioClip insideFootstep;

    [Header("씬 이름")]
    public string outsideSceneName = "OutsideScene";
    public string insideSceneName = "InsideScene";

    private bool isInside = false;

    void Start()
    {
        if (playerState == null)
            playerState = GetComponent<PlayerState>();

        if (footstepSource == null)
            footstepSource = GetComponent<AudioSource>();

        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == insideSceneName)
            isInside = true;
        else if (currentSceneName == outsideSceneName)
            isInside = false;

        StopFootstep();
    }

    void Update()
    {
        bool isMoving =
            Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 ||
            Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0;

        // 귀신 상태이거나 안 움직이면 발소리 끄기
        if (playerState == null || playerState.IsGhost || !isMoving)
        {
            StopFootstep();
            return;
        }

        // 시녀/빙의 상태일 때만 발소리 재생
        if (playerState.IsPossessed)
        {
            AudioClip targetClip = isInside ? insideFootstep : outsideFootstep;

            if (targetClip == null)
            {
                StopFootstep();
                return;
            }

            if (footstepSource.clip != targetClip)
            {
                footstepSource.Stop();
                footstepSource.clip = targetClip;
            }

            footstepSource.loop = true;

            if (!footstepSource.isPlaying)
                footstepSource.Play();
        }
        else
        {
            StopFootstep();
        }
    }

    void StopFootstep()
    {
        if (footstepSource != null && footstepSource.isPlaying)
            footstepSource.Stop();
    }
}