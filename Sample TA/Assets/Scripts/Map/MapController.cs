using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour {

    public static MapController 
        mapController;

    public Animator 
        animJawa, 
        animSumatra,
        animBackgroundCatalogue, 
        animCatalogueBook, 
        animCatalogueBookContent;

    public GameObject 
        jawa, 
        sumatera, 
        catalogue, 
        catalogueContent, 
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
        if (isBookOpening == true && animCatalogueBook.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && animCatalogueBook.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.2f) {
            catalogueContent.SetActive(true);

            animCatalogueBookContent.SetBool("Idle", false);
            animCatalogueBookContent.SetBool("FadeIn", true);

            isBookOpening = false;
        }

        if (isBookClosing == true && animCatalogueBook.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f && animCatalogueBook.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.2f) {
            CatalogueController.catalogueController.itemInformation.SetActive(false);
            CatalogueController.catalogueController.missionInformation.SetActive(false);
            catalogue.SetActive(false);

            isBookClosing = false;
        }
    }

    public void ButtonHighlightJawaFunction() {
        CatalogueController.catalogueController.locationName = "Jawa";

        titleCatalogue.text = "Jawa";

        catalogueJawa.SetActive(true);
        catalogueSumatera.SetActive(false);

        animJawa.SetBool("Highlight", true);
        animJawa.SetBool("Idle", false);
    }

    public void ButtonExitJawaFunction() {
        animJawa.SetBool("Highlight", false);
        animJawa.SetBool("Idle", true);
    }

    public void ButtonHighlightSumatraFunction() {
        CatalogueController.catalogueController.locationName = "Sumatera";

        titleCatalogue.text = "Sumatera";

        catalogueJawa.SetActive(false);
        catalogueSumatera.SetActive(true);

        animSumatra.SetBool("Highlight", true);
        animSumatra.SetBool("Idle", false);
    }

    public void ButtonExitSumatraFunction() {
        animSumatra.SetBool("Highlight", false);
        animSumatra.SetBool("Idle", true);
    }

    public void ButtonOpenCatalogueFunction() {
        catalogue.SetActive(true);
        CatalogueController.catalogueController.locationInformation.SetActive(true);

        animBackgroundCatalogue.SetBool("FadeIn", true);
        animBackgroundCatalogue.SetBool("Idle", false);
        animCatalogueBook.SetBool("OpenBook", true);
        animCatalogueBook.SetBool("Idle", false);

        CatalogueController.catalogueController.animLocation.SetBool("Idle", false);
        CatalogueController.catalogueController.animLocation.SetBool("FadeOut", false);
        CatalogueController.catalogueController.animLocation.SetBool("FadeIn", true);

        isBookOpening = true;
        CatalogueController.catalogueController.isLocationShowing = true;
    }

    public void ButtonCloseCatalogueFunction() {
        if (animCatalogueBookContent.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) {
            catalogueContent.SetActive(false);
            CatalogueController.catalogueController.locationInformation.SetActive(false);

            animBackgroundCatalogue.SetBool("FadeIn", false);
            animBackgroundCatalogue.SetBool("FadeOut", true);
            animCatalogueBook.SetBool("OpenBook", false);
            animCatalogueBook.SetBool("CloseBook", true);

            isBookClosing = true;
            CatalogueController.catalogueController.isLocationShowing = false;
            CatalogueController.catalogueController.isItemShowing = false;
            CatalogueController.catalogueController.isMissionShowing = false;
        }
    }
}
