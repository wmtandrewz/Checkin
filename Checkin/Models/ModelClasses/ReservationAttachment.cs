using System;
using Newtonsoft.Json;

namespace Checkin.Models.ModelClasses
{
    public class ReservationAttachment
    {
       
        [JsonProperty("IXhotelId")]
        public string IXhotelId {
            get;
            set;
        }

        [JsonProperty("IXfileName")]
        public string AttachmentName {
            get;
            set;
        }

        [JsonProperty("IXreservaId")]
        public string IXreservaId {
            get;
            set;
        }

        [JsonProperty("IXtype")]
        public string IXtype
        {
            get;
            set;
        }

        [JsonProperty("XhotelId")]
        public string HotelID {
            get;
            set;
        }

        [JsonProperty("XreservaId")]
        public string ReservationID {
            get;
            set;
        }

        [JsonProperty("Xattach")]
        public string AttachmentString {
            get;
            set;
        }

        [JsonProperty("Xtype")]
        public string AttachmentType
        {
            get;
            set;
        }
    }
}
