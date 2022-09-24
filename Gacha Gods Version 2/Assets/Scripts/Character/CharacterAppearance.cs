using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAppearance : MonoBehaviour
{
    [SerializeField] Transform HairStylePos;
    [SerializeField] Transform HatPos;
    [SerializeField] Transform FacePos;
    [SerializeField] Transform BackPos;
    [SerializeField] Transform TopPos;
    [SerializeField] Transform BottomPos;
    [SerializeField] Transform LSleevePos;
    [SerializeField] Transform RSleevePos;
    [SerializeField] Transform LShoePos;
    [SerializeField] Transform RShoePos;
    [SerializeField] Transform LWeaponPos;
    [SerializeField] Transform RWeaponPos;

    public void Initialise(Character character)
    {
        return;
        EquipOutfit(character.Appearance.CurrentOutfit);
        EquipHat(character.Appearance.CurrentHat);
        EquipWeapon(character.Appearance.CurrentWeapon);
        EquipShoe(character.Appearance.CurrentShoes);
        EquipHairStyle(character.Appearance.CurrentHairStyle);
        EquipBack(character.Appearance.CurrentBack);
        EquipFace(character.Appearance.CurrentFace);
    }

    public void EquipOutfit(OutfitData outfitData)
    {
        ReplaceCosmetic(outfitData.TopPrefab, TopPos);
        ReplaceCosmetic(outfitData.BottomPrefab, BottomPos);
        ReplaceCosmetic(outfitData.LSleevePrefab, LSleevePos);
        ReplaceCosmetic(outfitData.RSleevePrefab, RSleevePos);
    }

    public void EquipFace(FaceData faceData)
    {
        ReplaceCosmetic(faceData.FacePrefab, FacePos);
    }

    public void EquipHat(HatData hatData)
    {
        ReplaceCosmetic(hatData.HatPrefab, HatPos);
    }

    public void EquipWeapon(WeaponData weaponData)
    {
        ReplaceCosmetic(weaponData.LHWeaponPrefab, LWeaponPos);
        ReplaceCosmetic(weaponData.RHWeaponPrefab, RWeaponPos);
    }

    public void EquipBack(BackData backData)
    {
        ReplaceCosmetic(backData.BackPrefab, BackPos);
    }

    public void EquipShoe(ShoeData shoeData)
    {
        ReplaceCosmetic(shoeData.LShoePrefab, LShoePos);
        ReplaceCosmetic(shoeData.RShoePrefab, RShoePos);
    }

    public void EquipHairStyle(HairstyleData hairstyleData)
    {
        ReplaceCosmetic(hairstyleData.HairstylePrefab, HairStylePos);
    }

    void ReplaceCosmetic(GameObject gameObject, Transform transform)
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
