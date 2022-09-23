using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAppearance : MonoBehaviour
{
    [SerializeField] Transform HairStylePos;
    [SerializeField] Transform HatPos;
    [SerializeField] Transform BackPos;
    [SerializeField] Transform TopPos;
    [SerializeField] Transform BottomPos;
    [SerializeField] Transform LSleevePos;
    [SerializeField] Transform RSleevePos;
    [SerializeField] Transform LShoePos;
    [SerializeField] Transform RShoePos;
    [SerializeField] Transform LWeaponPos;
    [SerializeField] Transform RWeaponPos;

    public void MakeCharacter(Character character)
    {
        SafeInstantiate(character.Appearance.CurrentHairStyle.HairstylePrefab, HairStylePos);
        SafeInstantiate(character.Appearance.CurrentHat.HatPrefab, HatPos);
        SafeInstantiate(character.Appearance.CurrentBack.BackPrefab, BackPos);
        SafeInstantiate(character.Appearance.CurrentOutfit.TopPrefab, TopPos);
        SafeInstantiate(character.Appearance.CurrentOutfit.BottomPrefab, BottomPos);
        SafeInstantiate(character.Appearance.CurrentOutfit.LSleevePrefab, LSleevePos);
        SafeInstantiate(character.Appearance.CurrentOutfit.RSleevePrefab, RSleevePos);
        SafeInstantiate(character.Appearance.CurrentShoes.LShoePrefab, LShoePos);
        SafeInstantiate(character.Appearance.CurrentShoes.rShoePrefab, RShoePos);
        SafeInstantiate(character.Appearance.CurrentWeapon.LHWeaponPrefab, LWeaponPos);
        SafeInstantiate(character.Appearance.CurrentWeapon.RHWeaponPrefab, RWeaponPos);

        void SafeInstantiate(GameObject gameObject, Transform position)
        {
            if (gameObject == null || position == null)
                return;

            Instantiate(gameObject, position);
        }
    }
}
