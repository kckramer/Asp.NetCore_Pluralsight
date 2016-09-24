using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Controllers
{
    [Route("company/[controller]/[action]")]
    public class AboutController
    {
        [Route("")]
        public string Phone()
        {
            return "+1-555-555-5555";
        }

        [Route("[action]")]
        public string Country()
        {
            return "USA";
        }
    }

}
