namespace StackExchange.Windows.Services.Settings
{
    /// <summary>
    /// Defines a class that represents constraints on a setting.
    /// </summary>
    public struct SettingsConstraints
    {
        /// <summary>
        /// Gets or sets the max allowed value.
        /// </summary>
        public int? MaxValue { get; set; }

        /// <summary>
        /// Gets or sets the min allowed value.
        /// </summary>
        public int? MinValue { get; set; }
    }
}