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
        List<Trait> traits = new List<Trait>();

        foreach (var item in CharacterManager.ActiveTraits)
        {
            traits.Add(item.Key);
        }

        traits = traits.OrderByDescending(x => CharacterManager.ActiveTraits[x]).ToList();

        for (int i = 0; i < traits.Count; i++)
        {
            Trait trait = traits[i];

            TextMeshProUGUI text = SetsHolder.GetChild(i).GetComponent<TextMeshProUGUI>();

            text.gameObject.SetActive(true);

            text.SetText(trait.name + ": " + CharacterManager.ActiveTraits[trait]);
        }
    }
}
