using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDoorInteractable : MonoBehaviour, IInteractable
{
    [Header("Scene")]
    [SerializeField] private string targetSceneName;

    [Header("Spawn")]
    [SerializeField] private string targetSpawnId;

    [Header("Prompt")]
    [SerializeField] private string promptText = "E: 이동하기";

    //문소리 적용 코드
    [Header("Door SFX")]
    [SerializeField] private AudioSource doorAudioSource;
    [SerializeField] private AudioClip doorSound;
    [SerializeField] private float sceneLoadDelay = 0.7f;
    //여기까지

    private bool isLoading;

    public string GetPromptText(PlayerState playerState)
    {
        return promptText;
    }

    public bool CanInteract(PlayerState playerState)
    {
        return !isLoading;
    }

    public void Interact(PlayerState playerState)
    {
        if (isLoading) return;

        if (string.IsNullOrEmpty(targetSceneName))
        {
            Debug.LogError("[SceneDoorInteractable] Target Scene Name이 비어 있음");
            return;
        }

        SceneTransitionData.NextSpawnId = targetSpawnId;

        isLoading = true;

        Debug.Log($"[SceneDoorInteractable] 씬 이동: {targetSceneName}, SpawnId: {targetSpawnId}");

        //Door 상호작용
        if (playerState != null && playerState.IsPossessed && doorSound != null)
        {
            if (doorAudioSource == null)
                doorAudioSource = GetComponent<AudioSource>();

            if (doorAudioSource == null)
                doorAudioSource = gameObject.AddComponent<AudioSource>();

            doorAudioSource.PlayOneShot(doorSound);
            Invoke(nameof(LoadTargetScene), sceneLoadDelay);
        }
        else
        {
            LoadTargetScene();
        }
        //여기까지
    }

    private void LoadTargetScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}