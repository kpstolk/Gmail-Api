﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using KP.GmailApi.Managers;
using KP.GmailApi.Models;

namespace KP.GmailApi.UnitTests.Extensions
{
    internal static class TokenManagerExtensions
    {
        public static OAuth2Token Token(this OAuth2TokenManager tokenManager)
        {
            return ReflectionHelper.GetFieldValue<OAuth2Token>(tokenManager, "_token");
        }

        public static void DeleteFolder(this OAuth2TokenManager tokenManager)
        {
            IDictionary<string, OAuth2Token> tokens = StaticTokens(tokenManager);
            string tokenFile = tokens.First().Key;
            var directory = new FileInfo(tokenFile).Directory;
            if (directory != null && directory.Exists)
                directory.Delete(true);
        }

        public static IDictionary<string, OAuth2Token> StaticTokens(this OAuth2TokenManager tokenManager)
        {
            return ReflectionHelper.GetStaticFieldValue<IDictionary<string, OAuth2Token>>(typeof(OAuth2TokenManager), "Tokens");
        }

        public static string TokenFile(this OAuth2TokenManager tokenManager)
        {
            return ReflectionHelper.GetFieldValue<string>(tokenManager, "_tokenFile");
        }
    }
}
