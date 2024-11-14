using Microsoft.CognitiveServices.Speech;
using System;
using System.Threading.Tasks;

namespace TCPPR.Helpers
{
    public static class VoiceInputHelper
    {
        /// <summary>
        /// Розпізнавання мови за допомогою Azure Cognitive Services.
        /// </summary>
        public static async Task RecognizeSpeechAsync()
        {
            var config = SpeechConfig.FromSubscription("YourSubscriptionKey", "YourServiceRegion");

            using var recognizer = new SpeechRecognizer(config);

            recognizer.Canceled += (s, e) =>
            {
                Console.WriteLine($"Розпізнавання скасовано. Причина: {e.Reason}");

                if (e.Reason == CancellationReason.Error)
                {
                    Console.WriteLine($"Деталі помилки: {e.ErrorDetails}");
                }
            };

            await recognizer.StartContinuousRecognitionAsync();
        }
    }
}