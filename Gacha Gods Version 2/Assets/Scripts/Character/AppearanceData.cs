using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearanceData : ScriptableObject
{
    public FaceData CurrentFace => Faces[faceIndex];
    int faceIndex = 0;
    public OutfitData CurrentOutfit => Outfits[outfitIndex];
    int outfitIndex = 0;
    public HairstyleData CurrentHairStyle => HairStyles[hairStyleIndex];
    int hairStyleIndex = 0;
    public BackData CurrentBack => Backs[backIndex];
    int backIndex = 0;
    public HatData CurrentHat => Hats[hatIndex];
    int hatIndex = 0;
    public ShoeData CurrentShoes => Shoes[shoesIndex];
    int shoesIndex = 0;
    public WeaponData CurrentWeapon => Weapons[weaponIndex];
    int weaponIndex = 0;

    [SerializeField] List<FaceData> Faces;
    [SerializeField] List<OutfitData> Outfits;
    [SerializeField] List<HairstyleData> HairStyles;
    [SerializeField] List<BackData> Backs;
    [SerializeField] List<HatData> Hats;
    [SerializeField] List<ShoeData> Shoes;
    [SerializeField] List<WeaponData> Weapons;

    public void CycleFaceRight()
    {
        if (faceIndex == Faces.Count)
            faceIndex = 0;
        else
            faceIndex++;
    }

    public void CycleFaceLeft()
    {
        if (faceIndex == 0)
            faceIndex = Faces.Count - 1;
        else
            faceIndex--;
    }

    public void CycleOutfitRight()
    {
        if (outfitIndex == Outfits.Count)
            outfitIndex = 0;
        else
            outfitIndex++;
    }

    public void CycleOutfitLeft()
    {
        if (outfitIndex == 0)
            outfitIndex = Outfits.Count - 1;
        else
            outfitIndex--;
    }

    public void CycleHairstyleRight()
    {
        if (hairStyleIndex == HairStyles.Count)
            hairStyleIndex = 0;
        else
            hairStyleIndex++;
    }

    public void CycleHairstyleLeft()
    {
        if (hairStyleIndex == 0)
            hairStyleIndex = HairStyles.Count - 1;
        else
            hairStyleIndex--;
    }

    public void CycleBackRight()
    {
        if (backIndex == Backs.Count)
            backIndex = 0;
        else
            backIndex++;
    }

    public void CycleBackLeft()
    {
        if (backIndex == 0)
            backIndex = Backs.Count - 1;
        else
            backIndex--;
    }

    public void CycleHatRight()
    {
        if (hatIndex == Hats.Count)
            hatIndex = 0;
        else
            hatIndex++;
    }

    public void CycleHatLeft()
    {
        if (hatIndex == 0)
            hatIndex = Hats.Count - 1;
        else
            hatIndex--;
    }

    public void CycleShoeRight()
    {
        if (shoesIndex == Shoes.Count)
            shoesIndex = 0;
        else
            shoesIndex++;
    }

    public void CycleShoeLeft()
    {
        if (shoesIndex == 0)
            shoesIndex = Shoes.Count - 1;
        else
            shoesIndex--;
    }

    public void CycleWeaponRight()
    {
        if (weaponIndex == Weapons.Count)
            weaponIndex = 0;
        else
            weaponIndex++;
    }

    public void CycleWeaponLeft()
    {
        if (weaponIndex == 0)
            weaponIndex = Weapons.Count - 1;
        else
            weaponIndex--;
    }
}

