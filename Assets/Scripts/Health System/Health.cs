using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {
    [SerializeField]
    private float maxHealth = 1f;

    // TODO: Replace with serialized range
    [SerializeField]
    private float currentHealth = 1f;

    [SerializeField]
    private bool isDead = false;

    /// <summary>
    /// Notifies that <c>CurrentHealth</c> has been fully depleted.
    /// </summary>
    public UnityEvent death;

    /// <summary>
    /// <para>Notifies that the <c>CurrentHealth</c> was updated.</para><br />
    /// 
    /// Returns new value of <c>CurrentHealth</c>
    /// </summary>
    public UnityEvent<float, float> currentHealthUpdated;

    /// <summary>
    /// <para>Notifies that the <c>CurrentHealth</c> has decreased.</para><br />
    /// 
    /// <para>Not triggered if <c>IsDead</c> is true</para>
    /// 
    /// <para>Returns the difference between <c>CurrentHealth</c>'s previous value and current value.<br />
    /// Output is always negative.
    /// </para>
    /// 
    /// </summary>
    public UnityEvent<float> damaged;

    /// <summary>
    /// <para>Notifies that the <c>CurrentHealth</c> has increased.</para><br />
    /// 
    /// <para>Returns the difference between <c>CurrentHealth</c>'s previous value and current value.</para>
    /// Output is always positive.
    /// </summary>
    public UnityEvent<float> healed;

    /// <summary>
    /// <para>Notifies that the <c>MaxHealth</c> was updated.</para><br />
    /// 
    /// <para>Does not check if the value has changed.</para><br />
    /// 
    /// Returns new value of <c>MaxHealth</c>
    /// </summary>
    public UnityEvent<float> maxHealthUpdated;

    public float CurrentHealth {
        get => this.currentHealth;

        set {
            if (!IsDead) {
                var oldCurrentHealth = currentHealth;

                currentHealth = value;

                Func<float> getHealthDifference = () => {
                    return currentHealth - oldCurrentHealth;
                };

                var healthDifference = getHealthDifference();

                // if health changed
                if (healthDifference != 0) {
                    //to make sure we call the is dead event after all other event
                    var queueIsDead = false;


                    var isDamage = true;
                    //is damage
                    if (healthDifference < 0) {
                        if (currentHealth <= 0) {
                            queueIsDead = true;

                            currentHealth = 0;
                        }
                    }

                    //is healing
                    else {
                        isDamage = false;
                        if (currentHealth > maxHealth) {
                            currentHealth = maxHealth;
                        }
                    }

                    // the health diff accounting for clamping on death and overhealing
                    var trueHealthDifference = getHealthDifference();

                    if (isDamage)
                        damaged.Invoke(trueHealthDifference);

                    else
                        healed.Invoke(trueHealthDifference);

                    currentHealthUpdated.Invoke(currentHealth, trueHealthDifference);

                    IsDead = queueIsDead;
                }
            }
        }
    }

    public float MaxHealth {
        get => this.maxHealth;

        set {
            this.maxHealth = value;
            if (maxHealth < 0) {
                maxHealth = 0;
            }
            if (currentHealth > maxHealth) {
                // don't use property, as to not invoke onDamaged event
                currentHealth = maxHealth;
            }

            maxHealthUpdated.Invoke(maxHealth);
        }
    }

    public bool IsDead {
        get => isDead;

        set {
            isDead = value;
            if (isDead) {
                currentHealth = 0;

                death.Invoke();
            }
        }
    }

    //TODO: Replace with serialized range
    private void Start() {
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }
}
