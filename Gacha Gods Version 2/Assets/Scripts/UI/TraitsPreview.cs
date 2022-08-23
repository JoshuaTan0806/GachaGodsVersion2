using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class TraitsPreview : MonoBehaviour
{
    [SerializeField] Transform SetsHolder;

    private void Awake()
    {
        List<ScriptableObject> traits = new List<ScriptableObject>();

        foreach (var item in CharacterManager.ActiveArchetypes)
        {
            traits.Add(item.Key);
        }

        foreach (var item in CharacterManager.ActiveRoles)
        {
            traits.Add(item.Key);
        }

        traits = traits.OrderByDescending(x => x as Archetype != null ? CharacterManager.ActiveArchetypes[(Archetype)x] : CharacterManager.ActiveRoles[(Role)x]).ToList();

        for (int i = 0; i < traits.Count; i++)
        {
            ScriptableObject trait = traits[i];

            TextMeshProUGUI text = SetsHolder.GetChild(i).GetComponent<TextMeshProUGUI>();

            text.gameObject.SetActive(true);

            if (trait as Archetype != null)
                text.SetText(trait.name + ": " + CharacterManager.ActiveArchetypes[(Archetype)trait]);
            else
                text.SetText(trait.name + ": " + CharacterManager.ActiveRoles[(Role)trait]);
        }
    }
}
