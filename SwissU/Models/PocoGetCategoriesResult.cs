using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace SwissU.Models
{
    public class PocoResult
    {
        public List<PocoData> Results { get; set; }
    }

    public class PocoData
    {
        public PocoCategories Data { get; set; }
    }

    public class PocoCategories
    {
        public PocoCategoryValues categories { get; set; }
    }

    public class PocoCategoryValues
    {

        public JObject values { get; set; }

    }
}