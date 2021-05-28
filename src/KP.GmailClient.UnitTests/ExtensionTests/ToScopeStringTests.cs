﻿using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KP.GmailClient.Common;
using Xunit;

namespace KP.GmailClient.UnitTests.ExtensionTests
{
    public class ToScopeStringTests
    {
        [Fact]
        public void CanConvert()
        {
            // Arrange
            const GmailScopes scopes = GmailScopes.Readonly;
            string readOnlyScope = GmailHelper.GetGmailScopesField("ReadOnlyScope");

            // Act
            string scopeString = scopes.ToScopeString();

            // Assert
            scopeString.Should().BeEquivalentTo(readOnlyScope);
        }

        [Fact]
        public void WithMultipleScopes_ReturnsSpaceSeparatedString()
        {
            // Arrange
            const GmailScopes scopes = GmailScopes.Readonly | GmailScopes.Modify | GmailScopes.Labels | GmailScopes.Insert | GmailScopes.Send;
            // order shouldn't matter
            var scopeList = new List<string>
            {
                GmailHelper.GetGmailScopesField("LabelsScope"),
                GmailHelper.GetGmailScopesField("ReadOnlyScope"),
                GmailHelper.GetGmailScopesField("ModifyScope"),
                GmailHelper.GetGmailScopesField("SendScope"),
                GmailHelper.GetGmailScopesField("InsertScope"),
            };

            // Act
            string scopeString = scopes.ToScopeString();

            // Assert
            var parsedScopeList = scopeString.Split(' ').ToList();
            parsedScopeList.Should().BeEquivalentTo(scopeList);
        }
    }
}
