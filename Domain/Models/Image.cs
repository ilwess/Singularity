using Domain.Abstracts;
using Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    [JsonObject("image")]
    public class Image : Content
    {
    }
}
