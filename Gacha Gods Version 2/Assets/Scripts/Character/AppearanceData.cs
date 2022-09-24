using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearanceData : ScriptableObject
{
    public FaceData CurrentFace => faces[faceIndex];
    public int FaceIndex => faceIndex;
    int faceIndex = 0;

    public OutfitData CurrentOutfit => outfits[outfitIndex];
    public int OutfitIndex => outfitIndex;
    int outfitIndex = 0;

    public HairstyleData CurrentHairStyle => hairStyles[hairStyleIndex];
    public int HairStyleIndex => hairStyleIndex;
    int hairStyleIndex = 0;

    public BackData CurrentBack => backs[backIndex];
    public int BackIndex => backIndex;
    int backIndex = 0;

    public HatData CurrentHat => hats[hatIndex];
    public int HatIndex => hatIndex;
    int hatIndex = 0;

    public ShoeData CurrentShoes => shoes[shoesIndex];
    public int ShoesIndex => shoesIndex;
    int shoesIndex = 0;

    public WeaponData CurrentWeapon => weapons[weaponIndex];
    public int WeaponIndex => weaponIndex;
    int weaponIndex = 0;

    [SerializeField] List<FaceData> faces;
    [SerializeField] List<OutfitData> outfits;
    [SerializeField] List<HairstyleData> hairStyles;
    [SerializeField] List<BackData> backs;
    [SerializeField] List<HatData> hats;
    [SerializeField] List<ShoeData> shoes;
    [SerializeField] List<WeaponData> weapons;

    public void LoadAppearance(Character character)
    {
        faceIndex = PlayerPrefs.GetInt(character.name + FaceData.ID);
        outfitIndex = PlayerPrefs.GetInt(character.name + OutfitData.ID);
        hairStyleIndex = PlayerPrefs.GetInt(character.name + HairstyleData.ID);
        backIndex = PlayerPrefs.GetInt(character.name + BackData.ID);
        hatIndex = PlayerPrefs.GetInt(character.name + HatData.ID);
        shoesIndex = PlayerPrefs.GetInt(character.name + ShoeData.ID);
        weaponIndex = PlayerPrefs.GetInt(character.name + WeaponData.ID);
    }

    public void SaveAppearance(Character character)
    {
        PlayerPrefs.SetInt(character.name + FaceData.ID, faceIndex);
        PlayerPrefs.SetInt(character.name + OutfitData.ID, outfitIndex);
        PlayerPrefs.SetInt(character.name + HairstyleData.ID, hairStyleIndex);
        PlayerPrefs.SetInt(character.name + BackData.ID, backIndex);
        PlayerPrefs.SetInt(character.name + HatData.ID, hatIndex);
        PlayerPrefs.SetInt(character.name + ShoeData.ID, shoesIndex);
        PlayerPrefs.SetInt(character.name + WeaponData.ID, weaponIndex);
    }

    public void CycleFaceRight()
    {
        if (faceIndex == faces.Count)
            faceIndex = 0;
        else
            faceIndex++;
    }

    public void CycleFaceLeft()
    {
        if (faceIndex == 0)
            faceIndex = faces.Count - 1;
        else
            faceIndex--;
    }

    public void CycleOutfitRight()
    {
        if (outfitIndex == outfits.Count)
            outfitIndex = 0;
        else
            outfitIndex++;
    }

    public void CycleOutfitLeft()
    {
        if (outfitIndex == 0)
            outfitIndex = outfits.Count - 1;
        else
            outfitIndex--;
    }

    public void CycleHairstyleRight()
    {
        if (hairStyleIndex == hairStyles.Count)
            hairStyleIndex = 0;
        else
            hairStyleIndex++;
    }

    public void CycleHairstyleLeft()
    {
        if (hairStyleIndex == 0)
            hairStyleIndex = hairStyles.Count - 1;
        else
            hairStyleIndex--;
    }

    public void CycleBackRight()
    {
        if (backIndex == backs.Count)
            backIndex = 0;
        else
            backIndex++;
    }

    public void CycleBackLeft()
    {
        if (backIndex == 0)
            backIndex = backs.Count - 1;
        else
            backIndex--;
    }

    public void CycleHatRight()
    {
        if (hatIndex == hats.Count)
            hatIndex = 0;
        else
            hatIndex++;
    }

    public void CycleHatLeft()
    {
        if (hatIndex == 0)
            hatIndex = hats.Count - 1;
        else
            hatIndex--;
    }

    public void CycleShoeRight()
    {
        if (shoesIndex == shoes.Count)
            shoesIndex = 0;
        else
            shoesIndex++;
    }

    public void CycleShoeLeft()
    {
        if (shoesIndex == 0)
            shoesIndex = shoes.Count - 1;
        else
            shoesIndex--;
    }

    public void CycleWeaponRight()
    {
        if (weaponIndex == weapons.Count)
            weaponIndex = 0;
        else
            weaponIndex++;
    }

    public void CycleWeaponLeft()
    {
        if (weaponIndex == 0)
            weaponIndex = weapons.Count - 1;
        else
            weaponIndex--;
    }
}