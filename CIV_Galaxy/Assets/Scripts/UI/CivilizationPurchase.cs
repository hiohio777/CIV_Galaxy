using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class CivilizationPurchase : NoRegisterMonoBehaviour
{
    [SerializeField] private AudioClip soundEffect;
    private CivilizationFromPlayerSelect fromPlayerSelect;
    private IAPButton iapButton;

    private void Start()
    {
        fromPlayerSelect = GetComponentInParent<CivilizationFromPlayerSelect>();
        iapButton = GetComponent<IAPButton>();


        if (StatisticsPurchasePlayer.IsValidatePurchase(iapButton.productId) == false)
        {
            // Ещё не куплено
            iapButton.onPurchaseComplete.AddListener(OnPurchaseComplete);
            iapButton.onPurchaseFailed.AddListener(OnPurchaseFailure);

            var product = CodelessIAPStoreListener.Instance.GetProduct(iapButton.productId);
            iapButton.priceText.text = $"<color=green>{LocalisationGame.Instance.GetLocalisationString("buy")}</color>\r\n{product.metadata.localizedPrice}";

            fromPlayerSelect.button.interactable = false;
        }
        else Activate();
    }

    private void OnPurchaseComplete(Product product)
    {
        StatisticsPurchasePlayer.MakePurchase(product.definition.id);
        PlaySoundEffect(soundEffect);

        Activate();
    }

    private void Activate()
    {
        // Уже куплено
        fromPlayerSelect.button.interactable = true;

        iapButton.priceText.enabled = false;
        var button = GetComponent<Button>();
        button.interactable = false;
        button.image.enabled = false;
    }

    private void OnPurchaseFailure(Product product, PurchaseFailureReason reason)
    {
        Debug.Log("Purchase of product " + product.definition.id + " failed because " + reason);
    }
}
