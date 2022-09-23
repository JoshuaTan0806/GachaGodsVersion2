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
        AppearanceData a = character.Appearance;

        EquipOutfit(a.CurrentOutfit);
        EquipHat(a.CurrentHat);
        EquipWeapon(a.CurrentWeapon);
        EquipShoe(a.CurrentShoes);
        EquipHairStyle(a.CurrentHairStyle);
        EquipBack(a.CurrentBack);
    }

    public void EquipOutfit(OutfitData outfitData)
    {
        ReplaceEquipment(outfitData.TopPrefab, TopPos);
        ReplaceEquipment(outfitData.BottomPrefab, BottomPos);
        ReplaceEquipment(outfitData.LSleevePrefab, LSleevePos);
        ReplaceEquipment(outfitData.RSleevePrefab, RSleevePos);
    }

    public void EquipHat(HatData hatData)
    {
        ReplaceEquipment(hatData.HatPrefab, HatPos);
    }

    public void EquipWeapon(WeaponData weaponData)
    {
        ReplaceEquipment(weaponData.LHWeaponPrefab, LWeaponPos);
        ReplaceEquipment(weaponData.RHWeaponPrefab, RWeaponPos);
    }

    public void EquipBack(BackData backData)
    {
        ReplaceEquipment(backData.BackPrefab, BackPos);
    }

    public void EquipShoe(ShoeData shoeData)
    {
        ReplaceEquipment(shoeData.LShoePrefab, LShoePos);
        ReplaceEquipment(shoeData.RShoePrefab, RShoePos);
    }

    public void EquipHairStyle(HairstyleData hairstyleData)
    {
        ReplaceEquipment(hairstyleData.HairstylePrefab, HairStylePos);
    }

    void ReplaceEquipment(GameObject gameObject, Transform transform)
    {
        if (transform == null)
            return;

        if (transform.childCount > 0)
            Destroy(transform.GetChild(0).gameObject);

        if (gameObject == null)
            return;

        Instantiate(gameObject, transform);
    }
}
