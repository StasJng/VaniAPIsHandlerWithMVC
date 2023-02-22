namespace MP.Lib.Core.BootstrapSupport
{
    public static class ControlGroupExtensions
    {
        public static class Alerts
        {
            public const string SUCCESS = "success";
            public const string ATTENTION = "attention";
            public const string ERROR = "danger";
            public const string INFORMATION = "info";

            public static string[] ALL
            {
                get
                {
                    return new[]
                           {
                               SUCCESS,
                               ATTENTION,
                               INFORMATION,
                               ERROR
                           };
                }
            }
        }
    }
}
