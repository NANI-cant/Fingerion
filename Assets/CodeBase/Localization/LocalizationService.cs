using System.Collections.Generic;
using System.Xml;
using Architecture.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.Events;

public class LocalizationService {
    private readonly TextAsset _localizationXML;
    private readonly IPersistentProgressService _persistentProgressService;
    private Dictionary<string, List<string>> _localizationMap;
    
    public int SelectedLanguage { get; private set; }

    public UnityAction LanguageChanged;

    public LocalizationService(
        TextAsset localizationXML,
        IPersistentProgressService persistentProgressService
    ) {
        _localizationXML = localizationXML;
        _persistentProgressService = persistentProgressService;
        if (_persistentProgressService.Progress == null) {
            _persistentProgressService.Loaded += InitializeDelayed;
        }
        else {
            SelectedLanguage = _persistentProgressService.Progress.Language;
            InitializeMap();
        }
    }

    private void InitializeDelayed() {
        SelectedLanguage = _persistentProgressService.Progress.Language;
        InitializeMap();
        _persistentProgressService.Loaded -= InitializeDelayed;
    }

    private void InitializeMap() {
        _localizationMap = new Dictionary<string, List<string>>();

        XmlDocument xml = new XmlDocument();
        xml.LoadXml(_localizationXML.text);

        foreach (XmlNode key in xml["Keys"].ChildNodes) {
            string keyName = key.Attributes["Name"].Value;

            List<string> translations = new List<string>();
            foreach (XmlNode translate in key["Translates"].ChildNodes) {
                translations.Add(translate.InnerText);
            }
            _localizationMap[keyName] = translations;
        }
    }

    public void ChangeLanguage(int language) {
        SelectedLanguage = language;
        _persistentProgressService.Progress.Language = language;
        _persistentProgressService.Save();
        LanguageChanged?.Invoke();
    }

    public string GetLocalizedText(string key) {
        if (_localizationMap.ContainsKey(key)) {
            return _localizationMap[key][(SelectedLanguage)];
        }

        return "No Definition for key: " + key;
    }
}
