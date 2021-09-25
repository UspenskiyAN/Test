using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Model
{
    //[Keyless]
    public class RequestResponse
    {
        [Key]
        public string Request { get; set; }
        public string Response { get; set; } = "unknown request";
    }
}
