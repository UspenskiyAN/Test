using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Model
{
    public class Client
    {
        [Key]
        public string Key { get; set; }
        public string Name { get; set; }
    }
}
