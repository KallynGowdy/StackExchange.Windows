using StackExchange.Windows.Attributes;

namespace StackExchange.Windows.Services.Settings
{
    public enum SyntaxStyle
    {
        [Resource("AtelierHeathLight")]
        AtelierHeathLight = 1,

        [Resource("AtelierHeathDark")]
        AtelierHeathDark = 2,

        [Resource("SyntaxStyleTomorrow")]
        Tomorrow = 3,

        [Resource("SyntaxStyleTomorrowNight")]
        TomorrowNight = 4,

        [Resource("SyntaxStyleVibrantInk")]
        VibrantInk = 5,

        [Resource("SyntaxStyleGithub")]
        Github = 6
    }
}