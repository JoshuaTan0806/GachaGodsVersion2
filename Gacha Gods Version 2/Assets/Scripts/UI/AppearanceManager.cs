using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AppearanceManager : MonoBehaviour
{
    Character character;
    AppearanceData appearanceData => character.Appearance;

    [SerializeField] CharacterAppearance characterPrefab;

    [SerializeField] Button LHat;
    [SerializeField] Button RHat;
    [SerializeField] TextMeshProUGUI HatIndex;
    [SerializeField] Button LBack;
    [SerializeField] Button RBack;
    [SerializeField] TextMeshProUGUI BackIndex;
    [SerializeField] Button LShoe;
    [SerializeField] Button RShoe;
    [SerializeField] TextMeshProUGUI ShoesIndex;
    [SerializeField] Button LOutfit;
    [SerializeField] Button ROutfit;
    [SerializeField] TextMeshProUGUI OutfitIndex;
    [SerializeField] Button LWeapon;
    [SerializeField] Button RWeapon;
    [SerializeField] TextMeshProUGUI WeaponIndex;
    [SerializeField] Button LFace;
    [SerializeField] Button RFace;
    [SerializeField] TextMeshProUGUI FaceIndex;
    [SerializeField] Button LHairstyle;
    [SerializeField] Button RHairstyle;
    [SerializeField] TextMeshProUGUI HairstyleIndex;

    [SerializeField] Button SaveButton;

    private void Awake()
    {
        LoadApperanceData();

        LHat.onClick.AddListener(CycleHatLeft);
        RHat.onClick.AddListener(CycleHatRight);
        LOutfit.onClick.AddListener(CycleOutfitLeft);
        ROutfit.onClick.AddListener(CycleOutfitRight);
        LBack.onClick.AddListener(CycleBackLeft);
        RBack.onClick.AddListener(CycleBackRight);
        LShoe.onClick.AddListener(CycleShoeLeft);
        RShoe.onClick.AddListener(CycleShoeRight);
        LWeapon.onClick.AddListener(CycleWeaponLeft);
        RWeapon.onClick.AddListener(CycleWeaponRight);
        LFace.onClick.AddListener(CycleFaceLeft);
        RFace.onClick.AddListener(CycleFaceRight);
        LHairstyle.onClick.AddListener(CycleHairstyleLeft);
        RHairstyle.onClick.AddListener(CycleHairstyleRight);
        SaveButton.onClick.AddListener(SaveApperance);
    }

    public void LoadApperanceData()
    {
        foreach (var character in CharacterManager.Characters)
        {
            character.Appearance.LoadAppearance(character);
        }
    }

    public void Initialise(Character character)
    {
        this.character = character;
        characterPrefab.Initialise(character);
    }

    public void CycleFaceRight()
    {
        appearanceData.CycleFaceRight();
        FaceIndex.text = appearanceData.FaceIndex.ToString();
    }

    public void CycleFaceLeft()
    {
        appearanceData.CycleFaceLeft();
        FaceIndex.text = appearanceData.FaceIndex.ToString();
    }

    public void CycleOutfitRight()
    {
        appearanceData.CycleOutfitRight();
        OutfitIndex.text = appearanceData.OutfitIndex.ToString();
    }

    public void CycleOutfitLeft()
    {
        appearanceData.CycleOutfitLeft();
        OutfitIndex.text = appearanceData.OutfitIndex.ToString();
    }

    public void CycleHairstyleRight()
    {
        appearanceData.CycleHairstyleRight();
        HairstyleIndex.text = appearanceData.HairStyleIndex.ToString();
    }

    public void CycleHairstyleLeft()
    {
        appearanceData.CycleHairstyleLeft();
        HairstyleIndex.text = appearanceData.HairStyleIndex.ToString();
    }

    public void CycleBackRight()
    {
        appearanceData.CycleBackRight();
        BackIndex.text = appearanceData.BackIndex.ToString();
    }

    public void CycleBackLeft()
    {
        appearanceData.CycleBackLeft();
        BackIndex.text = appearanceData.BackIndex.ToString();
    }

    public void CycleHatRight()
    {
        appearanceData.CycleHatRight();
        HatIndex.text = appearanceData.HatIndex.ToString();
    }

    public void CycleHatLeft()
    {
        appearanceData.CycleHatLeft();
        HatIndex.text = appearanceData.HatIndex.ToString();
    }

    public void CycleShoeRight()
    {
        appearanceData.CycleShoeRight();
        ShoesIndex.text = appearanceData.ShoesIndex.ToString();
    }

    public void CycleShoeLeft()
    {
        appearanceData.CycleShoeLeft();
        ShoesIndex.text = appearanceData.ShoesIndex.ToString();
    }

    public void CycleWeaponRight()
    {
        appearanceData.CycleWeaponRight();
        WeaponIndex.text = appearanceData.WeaponIndex.ToString();
    }

    public void CycleWeaponLeft()
    {
        appearanceData.CycleWeaponLeft();
        WeaponIndex.text = appearanceData.WeaponIndex.ToString();
    }

    public void SaveApperance()
    {
        appearanceData.SaveAppearance(character);
    }
}
