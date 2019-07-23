using System.Collections.Generic;

namespace SwissU.Models
{
    // collection + links + results
    public class Poco
    {
        //public string Collection { get; set; }
        //public string Links { get; set; }

        // The results section of the JSON is a List - so we save the data in a list of PocoResut
        public List<PocoResultData> Results { get; set; }
    }

    // Result => Data + Links + Search_result_metadata 
    public class PocoResultData
    {
        public PocoDataDataContianer Data { get; set; }
        //public string Links { get; set; }
        //public string Search_result_metadata { get; set; }
    }

    // Result => Data => properties + version
    public class PocoDataDataContianer
    {
        public PocoDataPropertiesContainer properties { get; set; }
        public PocoDataVersionContainer versions { get; set; }
    }

    // Result => Data => Properties => ...
    public class PocoDataPropertiesContainer
    {
        public string Create_date { get; set; }
        public int Create_user_id { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mime_type { get; set; }
        public string Modify_date { get; set; }
        public int Parent_id { get; set; }
        public string Owner { get; set; }
        public int Owner_user_id { get; set; }
        public string Type_name { get; set; }
    }

    // Result => Data => Versions => ...
    public class PocoDataVersionContainer
    {
        public string File_name { get; set; }
        public int File_size { get; set; }
        public string File_type { get; set; }
    }
}
