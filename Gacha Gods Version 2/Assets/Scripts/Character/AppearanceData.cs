using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearanceData : ScriptableObject
{
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
