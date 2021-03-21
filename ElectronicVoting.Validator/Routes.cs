namespace ElectronicVoting
{
    public static class Routes
    {
        public static string Root = "https://localhost:5001";
        public static class Identity
        {
            public static string Login = "/api/ValidatorIdentity/Login";
        }
        public static class SignalR
        {
            public static string Connection = Root + "/ValidationServerManagerHub";
        }
    }
}