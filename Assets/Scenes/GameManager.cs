using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float score = 0;
    [SerializeField] private BallController ball;
    [SerializeField] private GameObject pinCollection;
    [SerializeField] private Transform pinAnchor;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private TextMeshProUGUI scoreText;
    private FallTrigger[] fallTriggers;
    private GameObject pinObjects;

    private void Start() {
        inputManager.OnResetPressed.AddListener(HandleReset);
        SetPins();
    }

    private void HandleReset() {
        ball.ResetBall();
        SetPins();
    }

    private void SetPins() {
        // Destroy existing pins if they exist
        if (pinObjects) {
            foreach (Transform child in pinObjects.transform) {
                Destroy(child.gameObject);
            }
            Destroy(pinObjects);
        }

        pinObjects = Instantiate(pinCollection, pinAnchor.transform.position, Quaternion.identity, pinAnchor);

        // Find all FallTrigger components (including inactive ones)
        fallTriggers = FindObjectsByType<FallTrigger>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        // Subscribe to the OnPinFall event for each pin
        foreach (FallTrigger pin in fallTriggers) {
            pin.OnPinFall.AddListener(IncrementScore);
        }
    }

    private void IncrementScore() {
        score++;
        scoreText.text = $"Score: {score}";
    }
}