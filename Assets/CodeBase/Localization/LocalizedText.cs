using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TMP_Text))]
public class LocalizedText : MonoBehaviour {
    [SerializeField] private string _localizationKey;

    private LocalizationService _localizationService;
    private TMP_Text _text;

    private void Awake() {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void Construct(LocalizationService localizationService) {
        _localizationService = localizationService;
        _localizationService.LanguageChanged += ChangeLanguage;
        ChangeLanguage();
    }

    public void SetKey(string key) {
        _localizationKey = key;
        ChangeLanguage();
    }

    private void OnDestroy() {
        _localizationService.LanguageChanged -= ChangeLanguage;
    }

    private void ChangeLanguage() {
        _text.text = _localizationService.GetLocalizedText(_localizationKey);
    }
}
