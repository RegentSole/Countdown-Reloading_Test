using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class CombinedAbilities : MonoBehaviour
{
    public enum AbilityType { ChangeColor, MoveObject, ScaleUp };

    private Dictionary<AbilityType, Coroutine> activeCoroutines = new Dictionary<AbilityType, Coroutine>();

    [Header("Change Color Settings")]
    public float colorCooldown = 5f;

    [Header("Move Object Settings")]
    public Vector2 moveDistance = new Vector2(10f, 0f);
    public float moveCooldown = 3f;

    [Header("Scale Up Settings")]
    public float scaleFactor = 2f; // Во сколько раз увеличить размер объекта
    public float scaleDuration = 3f; // Сколько секунд держать увеличенным
    public float scaleUpCooldown = 5f; // Время кулдауна для этой способности

    public void UseChangeColorAbility()
    {
        if (!IsAbilityActive(AbilityType.ChangeColor))
        {
            StartCoroutine(CooldownRoutine(AbilityType.ChangeColor, colorCooldown));
            ChangeColor();
            Debug.Log("Цвет был изменён!");
        }
        else
        {
            Debug.Log("Способность в кулдауне.");
        }
    }

    public void UseMoveObjectAbility()
    {
        if (!IsAbilityActive(AbilityType.MoveObject))
        {
            StartCoroutine(CooldownRoutine(AbilityType.MoveObject, moveCooldown));
            MoveObject();
            Debug.Log("Объект был перемещён!");
        }
        else
        {
            Debug.Log("Способность в кулдауне.");
        }
    }

    public void UseScaleUpAbility()
    {
        if (!IsAbilityActive(AbilityType.ScaleUp))
        {
            StartCoroutine(CooldownRoutine(AbilityType.ScaleUp, scaleUpCooldown));
            ScaleUpObject();
            Debug.Log("Размер объекта увеличен!");
        }
        else
        {
            Debug.Log("Способность в кулдауне.");
        }
    }

    private bool IsAbilityActive(AbilityType type)
    {
        return activeCoroutines.ContainsKey(type) && activeCoroutines[type] != null;
    }

    private IEnumerator CooldownRoutine(AbilityType type, float cooldownTime)
    {
        Debug.Log($"Starting cooldown for {type}: {cooldownTime} seconds.");
        activeCoroutines[type] = null;
        yield return new WaitForSeconds(cooldownTime);
        Debug.Log($"{type} cooldown finished.");
        activeCoroutines.Remove(type);
    }

    private void ChangeColor()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = Random.ColorHSV(); // Меняем цвет случайным образом
        }
    }

    private void MoveObject()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        if (rigidbody != null)
        {
            rigidbody.AddForce(moveDistance, ForceMode2D.Impulse); // Перемещаем объект
        }
    }

    private void ScaleUpObject()
    {
        transform.localScale *= scaleFactor; // Увеличение масштаба объекта
        Invoke(nameof(RestoreOriginalScale), scaleDuration); // Возвращение к исходному масштабу через scaleDuration секунд
    }

    private void RestoreOriginalScale()
    {
        transform.localScale /= scaleFactor; // Возврат к исходному масштабу
    }
}