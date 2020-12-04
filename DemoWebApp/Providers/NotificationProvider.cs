using SlackBotMessages;
using SlackBotMessages.Models;

namespace DemoWebApp.Providers
{
    internal class NotificationProvider
    {
        internal static void SendSlackNotification(string heading, string userName, string interest, string content)
        {
            var WebHookUrl = "https://hooks.slack.com/services/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";

            var client = new SbmClient(WebHookUrl);

            var message = new Message(heading)
                .SetUserWithEmoji("Website", Emoji.Loudspeaker);
            message.AddAttachment(new Attachment()
                .AddField("User Name", userName, true)
                .AddField("Interest", interest, true)
                .AddField("Content", content, true)
                .SetColor("#f96332")
            );

            client.Send(message);
        }
    }
}
