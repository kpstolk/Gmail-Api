﻿using System;
using FluentAssertions;
using KP.GmailClient.Builders;
using KP.GmailClient.Common.Enums;
using Xunit;

namespace KP.GmailClient.UnitTests.BuilderTests
{
    public class LabelQueryStringBuilderTests
    {
        private const string Path = "labels";

        [Fact]
        public void EmptyLabel_ThrowsError()
        {
            // Act
            Action action = () => new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Get, string.Empty)
                .Build();

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void AllLabelFields_ReturnsNoValue()
        {
            // Act
            string queryString = new LabelQueryStringBuilder()
                .SetFields(LabelFields.All)
                .Build();

            // Assert
            queryString.Should().Be(Path);
        }

        [Fact]
        public void RequestActionCreate_CanSet()
        {
            // Act
            string queryString = new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Create)
                .Build();

            // Assert
            queryString.Should().Be(Path);
        }

        [Fact]
        public void RequestActionCreate_CannotSetWithId()
        {
            // Act
            Action action = () => new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Create, "id")
                .Build();

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void RequestActionList_CanSet()
        {
            // Act
            string queryString = new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.List)
                .Build();

            // Assert
            queryString.Should().Be(Path);
        }

        [Fact]
        public void RequestActionList_CannotSetWithId()
        {
            // Act
            Action action = () => new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.List, "id")
                .Build();

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void RequestActionGet_CanSet()
        {
            // Act
            const string id = "id";
            string queryString = new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Get, id)
                .Build();

            // Assert
            queryString.Should().Be(Path + "/" + id);
        }

        [Fact]
        public void RequestActionGet_CannotSetWithoutId()
        {
            // Act
            Action action = () => new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Get)
                .Build();

            // Assert
            action.Should().Throw<ArgumentException>();
        }
    }
}
