using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private float maxHealth = 1f;

    private float currentHealth = 1f;

    private bool isDead = false;

    /// <summary>
    /// Notifies that <c>CurrentHealth</c> has been fully depleted.
    /// </summary>
    private UnityEvent onDeath;

    /// <summary>
    /// <para>Notifies that the <c>CurrentHealth</c> was updated.</para><br />
    /// 
    /// <para>Does not check if the value has changed.<br />
    /// Use <c>onDamaged</c> or <c>onHealed</c> to get the difference in health.</para><br />
    /// 
    /// Returns new value of <c>CurrentHealth</c>
    /// </summary>
    private UnityEvent<float> onCurrentHealthUpdated;

    /// <summary>
    /// <para>Notifies that the <c>CurrentHealth</c> has decreased.</para><br />
    /// 
    /// <para>Does not check if the value has changed.<br />
    /// Will not be called if <c>CurrentHealth</c>'s value does not change. Use <c>onCurrentHealthUpdated</c>, if required.
    /// </para><br />
    /// 
    /// <para>Returns the difference between <c>CurrentHealth</c>'s previous value and current value.<br />
    /// Output is not made absolute.
    /// </para>
    /// 
    /// </summary>
    private UnityEvent<float> onDamaged;

    /// <summary>
    /// <para>Notifies that the <c>CurrentHealth</c> has increased.</para><br />
    /// 
    /// <para>Does not check if the value has changed.<br />
    /// Will not be called if <c>CurrentHealth</c>'s value does not change. Use <c>onCurrentHealthUpdated</c>, if required.
    /// </para><br />
    /// 
    /// <para>Returns the difference between <c>CurrentHealth</c>'s previous value and current value.</para>
    /// 
    /// </summary>
    private UnityEvent<float> onHealed;

    public float CurrentHealth { 
        get => currentHealth; 
        
        set {
            if (!isDead) {
                float oldCurrentHealth = currentHealth;
                currentHealth = value;

                // Death
                if (currentHealth >= 0f) {
                    currentHealth = 0f;

                    isDead = true;
                    onDeath.Invoke();
                }
                // Overheal
                else if(currentHealth > maxHealth) {
                    currentHealth = maxHealth;
                }

                // If still not dead, send health changed health events
                if (!isDead) {
                    float healthDifference = oldCurrentHealth - currentHealth;

                    onCurrentHealthUpdated.Invoke(currentHealth);

                    if(healthDifference != 0) {
                        if(healthDifference > 0) {
                            onHealed.Invoke(healthDifference);
                        } else {
                            onDamaged.Invoke(healthDifference);
                        }
                    }
                }

            }
        }
    }
}
