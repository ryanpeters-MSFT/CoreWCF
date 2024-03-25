using System.Runtime.Serialization;

namespace Interfaces
{
    [DataContract]
    public class Client
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime BirthDate { get; set; }

        public int Age => ((DateTime.Now.Date - BirthDate).Days) / 365;
    }
}
