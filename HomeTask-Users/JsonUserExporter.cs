using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTask_Users
{
    internal class JsonUserExporter : IUserExporter
    {
        public string ExportData<T>(T data)
        {
            string json = JsonConvert.SerializeObject(data);
            return json;
        }
    }
}
