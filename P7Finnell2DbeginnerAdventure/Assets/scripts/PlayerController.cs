using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    // Variables related to player character movement
    public InputAction MoveAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;
    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    int currentHealth;
    public float speed = 3.9f;
    public float timeInvincible = 9.0f;
    bool isInvincible;
    float damageCooldown;

    // Start is called before the first frame update
    void Start()
    {
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }
    }


    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * 3.9f * Time.deltaTime;
        rigidbody2d.MovePosition(position);

    }
   public void ChangeHealth (int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
        if (amount < 0)
        {
            return;
        }
        isInvincible = true;
        damageCooldown = timeInvincible;
    }   
}