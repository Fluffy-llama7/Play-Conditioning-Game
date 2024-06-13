using UnityEngine;
using Mech;

public class Version2Mechanic2 : MonoBehaviour, IMechanic
{
    [SerializeField] private float minSize = 0.1f;
    [SerializeField] private float maxSize = 1.0f;
    [SerializeField] private float growthSpeed = 1.0f;
    [SerializeField] private float timeBetweenOrbs = 3.0f;
    private enum State { Falling, Landed }
    private State currentState = State.Falling;
    private GameObject orb;
    private SpriteRenderer orbRenderer;
    private CircleCollider2D orbCollider;
    private Color orbOriginalColor;
    private float fallStartTime;
    private Vector3 lastPlayerPosition;
    private bool orbFalling = false;

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
            case State.Falling:
                StartFalling();
                break;

            case State.Landed:
                OrbLanded();
                break;
        }
    }

    private void StartFalling()
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
            fallStartTime = Time.time;
            orbFalling = true;
            currentState = State.Landed;
            Debug.Log("Growing started");
        }
    }

    private void OrbLanded()
    {
        if (orb == null) return;

        float elapsedTime = Time.time - fallStartTime;

        if (elapsedTime < timeBetweenOrbs)
        {
            float growFactor = elapsedTime / (timeBetweenOrbs / growthSpeed);
            float size = Mathf.Lerp(minSize, maxSize, growFactor);
            orb.transform.localScale = new Vector3(size, size, size);
        }
        else
        {
            orbRenderer.color = orbOriginalColor;
            orbCollider.enabled = true; // Enable collider once the orb is in position
            orbFalling = false;
            currentState = State.Falling; // Reset state for the next execution
        }
    }

    public void Update()
    {
        if (currentState == State.Landed && orbFalling && orb != null)
        {
            OrbLanded();
        }
    }

    public void Disable()
    {
        currentState = State.Falling;
        orbFalling = false;
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
