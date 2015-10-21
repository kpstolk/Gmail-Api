﻿using System.Collections.Generic;
using KP.GmailApi.Builders;
using KP.GmailApi.Common;
using KP.GmailApi.Common.Enums;
using KP.GmailApi.Models;

namespace KP.GmailApi.Services
{
    /// <summary>
    /// Service to get, create, update and delete email labels.
    /// </summary>
    public class LabelService
    {
        private readonly GmailClient _client;

        internal LabelService(GmailClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Gets the specified label.
        /// </summary>
        /// <param name="id">The ID of the label to retrieve.</param>
        /// <returns></returns>
        public Label Get(string id)
        {
            string queryString = new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Get, id)
                .Build();

            return _client.Get<Label>(queryString);
        }

        /// <summary>
        /// Lists all labels in the user's mailbox.
        /// </summary>
        /// <returns></returns>
        public List<Label> List()
        {
            string queryString = new LabelQueryStringBuilder()
                .Build();

            return _client.Get<List<Label>>(queryString, new ParseOptions { Path = "labels" });
        }

        /// <summary>
        /// Creates a new label.
        /// </summary>
        public Label Create(CreateLabelInput labelInput)
        {
            string queryString = new LabelQueryStringBuilder()
                .Build();

            return _client.Post<Label>(queryString, labelInput);
        }

        /// <summary>
        /// WARNING: Immediately and permanently deletes the specified label and removes it from any messages and threads that it is applied to.
        /// </summary>
        /// <param name="id">The ID of the label to delete.</param>
        public void Delete(string id)
        {
            string queryString = new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Delete, id)
                .Build();

            _client.Delete(queryString);
        }

        /// <summary>
        /// Updates the specified label.
        /// </summary>
        /// <param name="labelInput"></param>
        public Label Update(UpdateLabelInput labelInput)
        {
            string queryString = new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Update, labelInput.Id)
                .Build();

            return _client.Put<Label>(queryString, labelInput);
        }

        /// <summary>
        /// Updates the specified label. This method supports patch semantics.
        /// </summary>
        /// <param name="label"></param>
        public Label Patch(Label label)
        {
            string queryString = new LabelQueryStringBuilder()
                .SetRequestAction(LabelRequestAction.Update, label.Id)
                .Build();

            return _client.Patch<Label>(queryString, label);
        }
    }
}
