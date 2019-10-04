using Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    [JsonObject("message")]
    public class Message : IEntity
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("sender")]
        public User Sender { get; set; }

        [JsonProperty("recievers")]
        public ICollection<User> Recievers { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("message")]
        public Message SharedMessage { get; set; }

        [JsonProperty("images")]
        public ICollection<Image> ImageLinks { get; set; }

        [JsonProperty("audios")]
        public ICollection<Audio> AudioLink { get; set; }

        [JsonProperty("videos")]
        public ICollection<Video> VideoLink { get; set; }

        public DateTime DateOfCreation { get; set; }
    }
}
