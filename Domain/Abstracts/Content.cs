using Domain.Interfaces;
using Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Abstracts
{
    public abstract class Content : IEntity
    {
        [JsonProperty("id")]
        [Key]
        public int Id { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("message")]
        public Message message { get; set; }

        [JsonProperty("user")]
        public User user { get; set; }

    }
}
