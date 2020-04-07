using System.Collections.Generic;

using Attributes;

using Interfaces.Delegates.Convert;
using Interfaces.Delegates.GetValue;
using Interfaces.Delegates.Collections;


namespace Delegates.Convert
{
    public class ConvertLanguageToCodeDelegate : IConvertDelegate<string, string>
    {
        private readonly IGetValueDelegate<Dictionary<string, string>> getLanguageCodesDelegate;
        private readonly IFindDelegate<KeyValuePair<string, string>> findLanguageCodeDelegate;

        [Dependencies(
            "Delegates.GetValue.Languages.GetLanguageCodesDelegate,Delegates",
            "Delegates.Collections.System.FindStringKeyStringValuePairDelegate,Delegates")]
        public ConvertLanguageToCodeDelegate(
            IGetValueDelegate<Dictionary<string, string>> getLanguageCodesDelegate,
            IFindDelegate<KeyValuePair<string, string>> findLanguageCodeDelegate)
            {
                this.getLanguageCodesDelegate = getLanguageCodesDelegate;
                this.findLanguageCodeDelegate = findLanguageCodeDelegate;
            }

        public string Convert(string language)
        {
            var languageCodes = getLanguageCodesDelegate.GetValue();
            return findLanguageCodeDelegate.Find(languageCodes, lc => lc.Value == language).Key;
        }
    }
}