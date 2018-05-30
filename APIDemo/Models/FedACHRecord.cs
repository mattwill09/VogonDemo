using System;
using System.Collections.Generic;
using System.Text;

namespace APIDemo.Models
{
    public class FedACHRecord
    {
        public string RoutingNumber { get; set; }

        public string OfficeCode { get; set; }

        public string ServicingFBRNumbber { get; set; }

        public string RecordTypeCode { get; set; }

        public DateTime ChangeDate { get; set; }

        public string NewRoutingNumber { get; set; }

        public string CustomerName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string StateCode { get; set; }

        public string Zipcode { get; set; }

        public string ZipcodeExtension { get; set; }

        public string TelephoneAreaCode { get; set; }

        public string TelephonePrefixNumber { get; set; }

        public string TelephoneSuffixNumber { get; set; }

        public string InstitutionStatusCode { get; set; }

        public string DataViewCode { get; set; }

        public string Filler { get; set; }

    }
}
