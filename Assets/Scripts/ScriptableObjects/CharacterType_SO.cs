using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CharacterTypes",fileName = "new CharacterTypes_SO")]
public class CharacterType_SO : ScriptableObject
{
    [SerializeField] private List<GameObject> characters;

    public int Count => characters.Count;
    public GameObject GetCharacter(int passedIndex)
    {
        if (passedIndex >= 0 && passedIndex < characters.Count)
        {
            return characters[passedIndex];
        }

        return null;
    }
}
