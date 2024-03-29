﻿using Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    [JsonObject("message")]
    public class Message : IEntity
    {
        public Message()
        {
            DateOfCreation = DateTime.Now;
        }
        [JsonProperty("id")]
        [Key]
        public int Id { get; set; }

        [JsonProperty("sender")]
        public User Sender { get; set; }

        [JsonProperty("reciever")]
        public User Reciever { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("message")]
        [ForeignKey("SharedMessageId")]
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
