using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class MessageDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("sender")]
        public UserDTO Sender { get; set; }

        [JsonProperty("reciever")]
        public UserDTO Reciever { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("message")]
        public MessageDTO SharedMessage { get; set; }

        [JsonProperty("images")]
        public ICollection<ImageDTO> ImageLinks { get; set; }

        [JsonProperty("audios")]
        public ICollection<AudioDTO> AudioLink { get; set; }

        [JsonProperty("videos")]
        public ICollection<VideoDTO> VideoLink { get; set; }
    }
}
