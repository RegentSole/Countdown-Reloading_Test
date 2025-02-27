using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    // Список всех способностей
    public List<Ability> abilities = new List<Ability>();
    
    void Start()
    {
        foreach (var ability in abilities)
        {
            ability.Initialize();
        }
    }
}