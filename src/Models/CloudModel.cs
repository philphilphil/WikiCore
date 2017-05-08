using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WikiCore.DB;
using WikiCore.Helpers;
using Newtonsoft.Json;

namespace WikiCore.Models
{

    public class CloudModel
    {
        public string TagCloudJson { get; set; }

        public CloudModel()
        {
            var tags = TagCloudHelper.GetTagCloudJson();
            this.TagCloudJson = JsonConvert.SerializeObject(tags);
        }
    }
}
