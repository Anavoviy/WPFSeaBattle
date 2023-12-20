using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleWPF.API.Game
{
    internal class User
    {
        string httptoken;

        internal static User TryLogin(string? loginText, string password)
        {
            var token = Task.Run(async () =>
            {
                var result = await Client.Instance.PostMessagePlainAsync($"Auth/GetToken?login={loginText}&password={password}");
                return result.Item2;
            }).Result;
            if (token == null)
                return null;
            Client.Instance.SetToken(token);
            return new User { httptoken = token };
        }

        internal static bool TryRegister(string? loginText, string password)
        {
            var result = Task.Run(async () =>
            {
                var result = await Client.Instance.PostMessagePlainAsync($"Auth/Registration", new AuthData { Login = loginText, Password = password });
                return result.Item1 == System.Net.HttpStatusCode.OK;
            }).Result;
            return result;
        }

        public class AuthData
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }
    }
}
