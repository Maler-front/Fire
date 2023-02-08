using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 1000f;
    private float _health = 500f;

    [SerializeField] private Slider _slider;

    public void ChangeHealth(float health)
    {
        _health += health;

        if(_health > _maxHealth)
        {
            _health = _maxHealth;
        }

        if (_health <= 0)
        {
            SceneManager.LoadScene("SampleScene");
        }

        _slider.value = _health;
    }
}
