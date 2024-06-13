using UnityEngine;
using Mech;

public class Version2Mechanic2 : MonoBehaviour, IMechanic
{
    private enum State
    {
        Growing,
        Smashing,
    }

    private State currentState = State.Growing;
    private GameObject orb;
    private SpriteRenderer orbRenderer;
    private CircleCollider2D orbCollider;
    private Color orbOriginalColor;
    private float growStartTime;
    [SerializeField] private float timeBetweenOrbs = 3.0f; // Time between orbs
    private Vector3 lastPlayerPosition;
    private bool orbGrowing = false;

    [SerializeField] private float minSize = 0.1f; // Minimum size of the orb
    [SerializeField] private float maxSize = 1.0f; // Maximum size of the orb
    [SerializeField] private float growthSpeed = 1.0f; // Growth speed of the orb

    private void Awake()
    {
        if (GameManager.instance.GetVersion() != 2)
        {
            this.enabled = false;
        }
        else
        {
            this.enabled = true;
        }

        orb = GameObject.Find("Orb");

        if (orb != null)
        {
            orbCollider = orb.GetComponent<CircleCollider2D>();
            orbRenderer = orb.GetComponent<SpriteRenderer>();
            orbOriginalColor = orbRenderer.color;
        }
    }

    public void Execute()
    {
        switch (currentState)
        {
            case State.Growing:
                StartGrowing();
                break;

            case State.Smashing:
                SmashOrb();
                break;
        }
    }

    private void StartGrowing()
    {
        if (orb == null) return;

        // Capture the player's last position
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            lastPlayerPosition = player.transform.position;
            orb.transform.position = lastPlayerPosition;
            orb.transform.localScale = new Vector3(minSize, minSize, minSize); // Start with minimum size
            orbRenderer.color = Color.grey; // Change color to grey
            orbCollider.enabled = false; // Disable collider during growing
            growStartTime = Time.time;
            orbGrowing = true;
            currentState = State.Smashing;
            Debug.Log("Growing started");
        }
    }

    private void SmashOrb()
    {
        if (orb == null) return;

        float elapsedTime = Time.time - growStartTime;

        if (elapsedTime < timeBetweenOrbs)
        {
            // Grow the orb in size based on the growth speed
            float growFactor = elapsedTime / (timeBetweenOrbs / growthSpeed);
            float size = Mathf.Lerp(minSize, maxSize, growFactor);
            orb.transform.localScale = new Vector3(size, size, size);
        }
        else
        {
            // Orb is fully grown, revert to original color and enable collider
            orbRenderer.color = orbOriginalColor;
            orbCollider.enabled = true; // Enable collider once the orb is in position
            orbGrowing = false;
            currentState = State.Growing; // Reset state for the next execution
            Debug.Log("Orb fully grown and ready to smash");
        }
    }

    public void Update()
    {
        if (currentState == State.Smashing && orbGrowing && orb != null)
        {
            SmashOrb();
        }
    }

    public void Disable()
    {
        currentState = State.Growing;
        orbGrowing = false;
        ResetOrb();
    }

    private void OnDisable()
    {
        ResetOrb();
    }

    private void ResetOrb()
    {
        if (orb != null)
        {
            orb.transform.localScale = new Vector3(minSize, minSize, minSize); // Ensure the orb size resets to minSize
            orbRenderer.color = orbOriginalColor;
            orbCollider.enabled = true; // Ensure collider is disabled until the orb is active again
        }
    }
}
