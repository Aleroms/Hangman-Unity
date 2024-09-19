using System.Text.RegularExpressions;

namespace Hangman.CloudInfrastructure
{



    public enum FoundationalModel { META_LLAMA3, ANTHROPIC_CLAUDE1 }

    public static class AWSBedrock
    {
        public static FoundationalModel FoundationalModel { get; set; }
        public static string ModelPrompt(string difficulty)
        {
            string promptFormat = "";
            switch (FoundationalModel)
            {
                case FoundationalModel.META_LLAMA3:
                    promptFormat = "wordGenerate?hangman_prompt=%3C%7Cbegin_of_text%7C%3E%3C%"
                + "7Cstart_header_id%7C%3Esystem%3C%7Cend_header_id%7C%3E%20You%20are%20a%20helpful%20AI%20assistant%20"
                + "for%20generating%20unqiue%20words%20to%20be%20used%20in%20Hangman.%20Your%20response%20should%20only%20be"
                + "%20a%20single%20lowercased%20word%3C%7Ceot_id%7C%3E%3C%7Cstart_header_id%7C%3Euser%3C%7Cend_header_id%7C%3Egenerate"
                + $"%20an%20{difficulty}%20word%20in%20English%3C%7Ceot_id%7C%3E%3C%7Cstart_header_id%7C%3Eassistant%3C%7Cend_header_id%7C%3E";
                    break;
                case FoundationalModel.ANTHROPIC_CLAUDE1:
                    promptFormat = $"wordGenerate?hangman_prompt=%5Cn%5CnHuman%3Agenerate%20an%20{difficulty}%20English%20word."
                        + "%20Only%20return%20a%20single%20word%20and%20wrap%20it%20in%20parenthesis!%5Cn%5CnAssistant%3A";
                    break;
            }
            return promptFormat;
        }
        public static string Sanitize(string text) =>
            FoundationalModel == FoundationalModel.META_LLAMA3
                ? Regex.Replace(text, @"[^a-z]", "")
                : FoundationalModel == FoundationalModel.ANTHROPIC_CLAUDE1
                    ? Regex.Replace(text, @"\((\s*[a-zA-Z]+\s*)\)", "$1")
                    : text;


    }
}
