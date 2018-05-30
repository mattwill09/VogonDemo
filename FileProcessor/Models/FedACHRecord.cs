using System;
using System.Collections.Generic;
using System.Text;
using FileHelpers;

namespace FileProcessor.Models
{
    [FixedLengthRecord()]
    public class FedACHRecord
    {
        [FieldFixedLength(9)]
        public string RoutingNumber { get; set; }

        [FieldFixedLength(1)]
        public string OfficeCode { get; set; }

        [FieldFixedLength(9)]
        public string ServicingFBRNumbber { get; set; }

        [FieldFixedLength(1)]
        public string RecordTypeCode { get; set; }

        [FieldFixedLength(6)]
        [FieldConverter(ConverterKind.Date, "MMddyy")]
        public DateTime ChangeDate { get; set; }

        [FieldFixedLength(9)]
        public string NewRoutingNumber { get; set; }

        [FieldFixedLength(36)]
        public string CustomerName { get; set; }

        [FieldFixedLength(36)]
        public string Address { get; set; }

        [FieldFixedLength(20)]
        public string City { get; set; }

        [FieldFixedLength(2)]
        public string StateCode { get; set; }

        [FieldFixedLength(5)]
        public string Zipcode { get; set; }

        [FieldFixedLength(4)]
        public string ZipcodeExtension { get; set; }

        [FieldFixedLength(3)]
        public string TelephoneAreaCode { get; set; }

        [FieldFixedLength(3)]
        public string TelephonePrefixNumber { get; set; }

        [FieldFixedLength(4)]
        public string TelephoneSuffixNumber { get; set; }

        [FieldFixedLength(1)]
        public string InstitutionStatusCode { get; set; }

        [FieldFixedLength(1)]
        public string DataViewCode { get; set; }

        [FieldFixedLength(5)]
        public string Filler { get; set; }

    }
}
