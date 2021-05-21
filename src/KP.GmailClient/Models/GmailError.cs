﻿using Newtonsoft.Json;

namespace KP.GmailClient.Models
{
    /// <summary>The details of a single error returned by Gmail.</summary>
    public class GmailError
    {
        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        /// <summary>The  message of the error.</summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("locationType")]
        public string LocationType { get; set; }

        /// <summary>A string with the values of the properties from this <see cref="GmailError"/>.</summary>
        public override string ToString()
        {
            return string.Concat("Domain: ", Domain, ", Reason: ", Reason, ", Message: ", Message, ", Location: ", Location, "LocationType: ", LocationType);
        }
    }
}