using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Checkin.Models.ModelClasses
{
    // To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
    //
    //    using QuickType;
    //
    //    var welcome = Welcome.FromJson(jsonString);


        public class ProformaModel
        {
            [JsonProperty("d")]
            public D D { get; set; }
        }

        public class DResult
        {
            [JsonProperty("__metadata")]
            public Metadata Metadata { get; set; }

            [JsonProperty("ImEmailAddr")]
            public string ImEmailAddr { get; set; }

            [JsonProperty("ImProf")]
            public string ImProf { get; set; }

            [JsonProperty("ImSendPdf")]
            public string ImSendPdf { get; set; }

            [JsonProperty("Type")]
            public string Type { get; set; }

            [JsonProperty("Id")]
            public string Id { get; set; }

            [JsonProperty("Number")]
            public string Number { get; set; }

            [JsonProperty("Message")]
            public string Message { get; set; }

            [JsonProperty("LogNo")]
            public string LogNo { get; set; }

            [JsonProperty("LogMsgNo")]
            public string LogMsgNo { get; set; }

            [JsonProperty("MessageV1")]
            public string MessageV1 { get; set; }

            [JsonProperty("MessageV2")]
            public string MessageV2 { get; set; }

            [JsonProperty("MessageV3")]
            public string MessageV3 { get; set; }

            [JsonProperty("MessageV4")]
            public string MessageV4 { get; set; }

            [JsonProperty("Parameter")]
            public string Parameter { get; set; }

            [JsonProperty("Row")]
            public long Row { get; set; }

            [JsonProperty("Field")]
            public string Field { get; set; }

            [JsonProperty("System")]
            public string System { get; set; }

            [JsonProperty("NavOthers")]
            public NavOthers NavOthers { get; set; }

            [JsonProperty("NavAdvance")]
            public NavAdvance NavAdvance { get; set; }

            [JsonProperty("NavLines")]
            public NavLines NavLines { get; set; }

            [JsonProperty("NavHeader")]
            public NavHeader NavHeader { get; set; }
        }

        public class D
        {
            [JsonProperty("results")]
            public DResult[] Results { get; set; }
        }

        public class Metadata
        {
            [JsonProperty("id")]
            public Uri Id { get; set; }

            [JsonProperty("uri")]
            public Uri Uri { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
        }
        
        public class NavAdvance
        {
            [JsonProperty("results")]
            public NavAdvanceResult[] NavAdvResults { get; set; }
        }

        public class NavAdvanceResult
        {
            [JsonProperty("__metadata")]
            public Metadata Metadata { get; set; }

            [JsonProperty("BillNo")]
            public string BillNo { get; set; }

            [JsonProperty("HotelId")]
            public long HotelId { get; set; }

            [JsonProperty("ReservationId")]
            public string ReservationId { get; set; }

            [JsonProperty("AdvText")]
            public string AdvText { get; set; }

            [JsonProperty("AdvValue")]
            public string AdvValue { get; set; }
        }
        
        public class NavHeader
        {
            [JsonProperty("results")]
            public NavHeaderResult[] Results { get; set; }
        }

        public class NavHeaderResult
        {
            [JsonProperty("__metadata")]
            public Metadata Metadata { get; set; }

            [JsonProperty("BillNo")]
            public string BillNo { get; set; }

            [JsonProperty("HotelId")]
            public string HotelId { get; set; }

            [JsonProperty("ReservationId")]
            public string ReservationId { get; set; }

            [JsonProperty("CustomerId")]
            public string CustomerId { get; set; }

            [JsonProperty("CustomerName")]
            public string CustomerName { get; set; }

            [JsonProperty("CareOf")]
            public string CareOf { get; set; }

            [JsonProperty("TinCodeM")]
            public string TinCodeM { get; set; }

            [JsonProperty("VatNo")]
            public string VatNo { get; set; }

            [JsonProperty("SvatNo")]
            public string SvatNo { get; set; }

            [JsonProperty("BookingParty")]
            public string BookingParty { get; set; }

            [JsonProperty("ArrivalDate")]
            public string ArrivalDate { get; set; }

            [JsonProperty("ArrivalTime")]
            public string ArrivalTime { get; set; }

            [JsonProperty("DepartureDate")]
            public string DepartureDate { get; set; }

            [JsonProperty("DepartureTime")]
            public string DepartureTime { get; set; }

            [JsonProperty("BillDate")]
            public string BillDate { get; set; }

            [JsonProperty("Voucher")]
            public string Voucher { get; set; }

            [JsonProperty("ResvStatus")]
            public string ResvStatus { get; set; }

            [JsonProperty("RoomCount")]
            public long RoomCount { get; set; }

            [JsonProperty("Adult")]
            public long Adult { get; set; }

            [JsonProperty("Guide")]
            public long Guide { get; set; }

            [JsonProperty("Child")]
            public long Child { get; set; }

            [JsonProperty("ChildFree")]
            public long ChildFree { get; set; }

            [JsonProperty("TaxExempt")]
            public string TaxExempt { get; set; }

            [JsonProperty("GroupName")]
            public string GroupName { get; set; }

            [JsonProperty("GuestName")]
            public string GuestName { get; set; }
        }

        public  class NavLines
        {
            [JsonProperty("results")]
            public NavLinesResult[] Results { get; set; }
        }

        public class NavLinesResult
        {
            [JsonProperty("__metadata")]
            public Metadata Metadata { get; set; }

            [JsonProperty("BillNo")]
            public string BillNo { get; set; }

            [JsonProperty("HotelId")]
            public string HotelId { get; set; }

            [JsonProperty("ReservationId")]
            public string ReservationId { get; set; }

            [JsonProperty("StartDate")]
            public string StartDate { get; set; }

            [JsonProperty("EndDate")]
            public string EndDate { get; set; }

            [JsonProperty("ArrivalDate")]
            public string ArrivalDate { get; set; }

            [JsonProperty("DepartureDate")]
            public string DepartureDate { get; set; }

            [JsonProperty("ConceptId")]
            public string ConceptId { get; set; }

            [JsonProperty("Description")]
            public string Description { get; set; }

            [JsonProperty("ResvStatus")]
            public string ResvStatus { get; set; }

            [JsonProperty("RoomType")]
            public string RoomType { get; set; }

            [JsonProperty("MealPlan")]
            public string MealPlan { get; set; }

            [JsonProperty("Occupancy")]
            public long Occupancy { get; set; }

            [JsonProperty("RoomQuantity")]
            public long RoomQuantity { get; set; }

            [JsonProperty("RoomNight")]
            public long RoomNight { get; set; }

            [JsonProperty("Rate")]
            public string Rate { get; set; }

            [JsonProperty("RateCurr")]
            public string RateCurr { get; set; }

            [JsonProperty("Amount")]
            public string Amount { get; set; }

            [JsonProperty("AmountCurr")]
            public string AmountCurr { get; set; }

            [JsonProperty("ResvType")]
            public string ResvType { get; set; }
        }

        public class NavOthers
        {
            [JsonProperty("results")]
            public NavOthersResult[] Results { get; set; }
        }

        public class NavOthersResult
        {
            [JsonProperty("__metadata")]
            public Metadata Metadata { get; set; }

            [JsonProperty("BillNo")]
            public string BillNo { get; set; }

            [JsonProperty("HotelId")]
            public string HotelId { get; set; }

            [JsonProperty("ReservationId")]
            public string ReservationId { get; set; }

            [JsonProperty("TotalB4Disc")]
            public string TotalB4Disc { get; set; }

            [JsonProperty("Discount")]
            public string Discount { get; set; }

            [JsonProperty("GrandTotal")]
            public string GrandTotal { get; set; }

            [JsonProperty("TotalExclVat")]
            public string TotalExclVat { get; set; }

            [JsonProperty("VatPerc")]
            public string VatPerc { get; set; }

            [JsonProperty("Vat")]
            public string Vat { get; set; }

            [JsonProperty("TotalExclSvat")]
            public string TotalExclSvat { get; set; }

            [JsonProperty("SvatPerc")]
            public string SvatPerc { get; set; }

            [JsonProperty("Svat")]
            public string Svat { get; set; }

            [JsonProperty("TotalDue")]
            public string TotalDue { get; set; }

            [JsonProperty("BalanceDue")]
            public string BalanceDue { get; set; }

            [JsonProperty("CommCurr")]
            public string CommCurr { get; set; }

            [JsonProperty("Commission")]
            public string Commission { get; set; }

            [JsonProperty("RoomingList")]
            public string RoomingList { get; set; }

            [JsonProperty("GeneratedBy")]
            public string GeneratedBy { get; set; }

            [JsonProperty("ExCurr")]
            public string ExCurr { get; set; }

            [JsonProperty("ExRate")]
            public string ExRate { get; set; }

            [JsonProperty("SvatNo")]
            public string SvatNo { get; set; }

            [JsonProperty("AmtM")]
            public string AmtM { get; set; }

            [JsonProperty("ScPercM")]
            public string ScPercM { get; set; }

            [JsonProperty("ScAmtM")]
            public string ScAmtM { get; set; }

            [JsonProperty("GstPercM")]
            public string GstPercM { get; set; }

            [JsonProperty("GstAmtM")]
            public string GstAmtM { get; set; }

            [JsonProperty("GstCalcOnM")]
            public string GstCalcOnM { get; set; }

            [JsonProperty("GreenTaxM")]
            public string GreenTaxM { get; set; }

            [JsonProperty("TotalUsdM")]
            public string TotalUsdM { get; set; }

            [JsonProperty("TotalCurrM")]
            public string TotalCurrM { get; set; }
        }

      
    }


