using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float direction;
    private float lifeTime;

    private BoxCollider2D boxCollider;
    private Animator ani;

    [Header("Sound Settings")]
    [Tooltip("Drag and drop the sound effect for when the projectile hits.")]
    [SerializeField] private AudioClip hitSound;
    private AudioSource audioSource;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false; // Prevent audio from playing on instantiation
        }
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifeTime += Time.deltaTime;
        if (lifeTime > 3) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hit = false;
        }
        else
        {
            hit = true;
            boxCollider.enabled = false;
            ani.SetTrigger("Hit");

            // Play the hit sound effect
            if (hitSound != null)
            {
                audioSource.PlayOneShot(hitSound);
            }
        }
    }

    public void SetDirection(float _direction)
    {
        lifeTime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScalex = transform.localScale.x;
        if (Mathf.Sign(localScalex) != _direction)
        {
            localScalex = -localScalex;
        }

        transform.localScale = new Vector3(localScalex, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
