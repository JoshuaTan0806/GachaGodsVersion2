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
        LoadAppearance();

        LHat.onClick.AddListener(delegate { CycleHatLeft(); Initialise(character); });
        RHat.onClick.AddListener(delegate { CycleHatRight(); Initialise(character); });
        LOutfit.onClick.AddListener(delegate { CycleOutfitLeft(); Initialise(character); });
        ROutfit.onClick.AddListener(delegate { CycleOutfitRight(); Initialise(character); });
        LBack.onClick.AddListener(delegate { CycleBackLeft(); Initialise(character); });
        RBack.onClick.AddListener(delegate { CycleBackRight(); Initialise(character); });
        LShoe.onClick.AddListener(delegate { CycleShoeLeft(); Initialise(character); });
        RShoe.onClick.AddListener(delegate { CycleShoeRight(); Initialise(character); });
        LWeapon.onClick.AddListener(delegate { CycleWeaponLeft(); Initialise(character); });
        RWeapon.onClick.AddListener(delegate { CycleWeaponRight(); Initialise(character); });
        LFace.onClick.AddListener(delegate { CycleFaceLeft(); Initialise(character); });
        RFace.onClick.AddListener(delegate { CycleFaceRight(); Initialise(character); });
        LHairstyle.onClick.AddListener(delegate { CycleHairstyleLeft(); Initialise(character); });
        RHairstyle.onClick.AddListener(delegate { CycleHairstyleRight(); Initialise(character); });
        SaveButton.onClick.AddListener(delegate { SaveApperance(); Initialise(character); });
    }

    public void LoadAppearance()
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