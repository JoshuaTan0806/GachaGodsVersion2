using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearanceData : ScriptableObject
{
    public FaceData CurrentFace => currentFace;
    FaceData currentFace;
    public OutfitData CurrentOutfit => currentOutfit;
    OutfitData currentOutfit;
    public HairstyleData CurrentHairStyle => currentHairStyle;
    HairstyleData currentHairStyle;
    public BackData CurrentBack => currentBack;
    BackData currentBack;
    public HatData CurrentHat => currentHat;
    HatData currentHat;
    public ShoeData CurrentShoes => currentShoes;
    ShoeData currentShoes;
    public WeaponData CurrentWeapon => currentWeapon;
    WeaponData currentWeapon;

    [SerializeField] FaceData DefaultFace;
    [SerializeField] OutfitData DefaultOutfit;
    [SerializeField] HairstyleData DefaultHairstyle;
    [SerializeField] BackData DefaultBack;
    [SerializeField] HatData DefaultHat;
    [SerializeField] ShoeData DefaultShoes;
    [SerializeField] WeaponData DefaultWeapon;

    [SerializeField] List<FaceData> Faces;
    [SerializeField] List<OutfitData> Outfits;
    [SerializeField] List<HairstyleData> HairStyles;
    [SerializeField] List<BackData> Backs;
    [SerializeField] List<HatData> Hats;
    [SerializeField] List<ShoeData> Shoes;
    [SerializeField] List<WeaponData> Weapons;
}
