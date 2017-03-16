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
        private DBService _dbs;
        public DBService dbs
        {
            get
            {
                if (_dbs == null)
                {
                    _dbs = new DBService();
                }
                return this._dbs;
            }
            set { }
        }
        public string TagCloudJson { get; set; }

        public CloudModel()
        {
            var tags = TagCloudHelper.GetTagCloudJson();
            this.TagCloudJson = JsonConvert.SerializeObject(tags);
        }
    }
}
