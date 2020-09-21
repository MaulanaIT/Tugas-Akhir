using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour {

    public static MapController 
        mapController;

    public AudioSource 
        audioButton;

    public AudioClip 
        audioButtonClick, 
        audioButtonSelect;

    public GameObject 
        jawa, 
        sumatera, 
        catalogue, 
        catalogueJawa, 
        catalogueSumatera;

    public Text 
        titleCatalogue;

    public bool
        isBookOpening,
        isBookClosing;

    private void Awake() {
        if (mapController == null) {
            mapController = this;
        } else if (mapController != this) {
            Destroy(gameObject);
        }
    }

    void Start() {

    }

    void Update() {

    }

    public void ButtonHighlightJawaFunction() {
        CatalogueController.catalogueController.locationName = "Jawa";

        titleCatalogue.text = "Jawa";

        catalogueJawa.SetActive(true);
        catalogueSumatera.SetActive(false);
    }

    public void ButtonHighlightSumatraFunction() {
        CatalogueController.catalogueController.locationName = "Sumatera";

        titleCatalogue.text = "Sumatera";

        catalogueJawa.SetActive(false);
        catalogueSumatera.SetActive(true);
    }

    public void ButtonOpenCatalogueFunction() {
        audioButton.Stop();
        audioButton.clip = audioButtonSelect;
        audioButton.Play();

        catalogue.SetActive(true);
    }

    public void ButtonCloseCatalogueFunction() {
        audioButton.Stop();
        audioButton.clip = audioButtonSelect;
        audioButton.Play();

        CatalogueController.catalogueController.locationInformation.SetActive(false);
        CatalogueController.catalogueController.listPlantInformation.SetActive(false);
        CatalogueController.catalogueController.questInformation.SetActive(false);

        catalogue.SetActive(false);
    }
}
