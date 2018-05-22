using System;
namespace Checkin
{
	public class PerformaDetails
	{
		public string customerCode { get; private set; }

		public string customer { get; private set; }

		public string vatRegNo { get; private set; }

		public string guestName { get; private set; }

		public string bookingParty { get; private set; }

		public string profInvoiceNo { get; private set; }

		public string date { get; private set; }

		public string arrival { get; private set; }

		public string departure { get; private set; }

		public string reservationNumber { get; private set; }

		public string voucher { get; private set; }

		public string reservationStatus { get; private set; }

		public string numberOfRooms { get; private set; }

		public string adults { get; private set; }

		public string child { get; private set; }

		public string childF { get; private set; }

		public string guide { get; private set; }

		public string total { get; private set; }

		public string totalExcluVat { get; private set; }

		public string vat { get; private set; }

		public string totalDue { get; private set; }

		public string balanceDue { get; private set; }

		public string rooms { get; private set; }

		public string generatedBy { get; private set; }

		public string exchangeRate { get; private set; }

		public string paymentDescription { get; private set; }

		public string accountHolder { get; private set; }

		public string bank { get; private set; }

		public string accountNumber { get; private set; }

		public string swiftCode { get; private set; }

		public string description2 { get; private set; }

		public string description3 { get; private set; }

		public string printedBy { get; private set; }

		public string printedDate { get; private set; }

		public string advancedReceivedPositive { get; private set; }

		public string advancedReceivedPositiveValue { get; private set; }

		public string advancedReceivedNegative { get; private set; }

		public string advancedReceivedValue { get; private set; }

		public PerformaDetails(string CustomerCode, string Customer, string VatRegNo, string GuestName,
							  string BookingParty, string ProfInvoiceNo, string Date,
							  string Arrival, string Departure, string ReservationNumber,
							  string Voucher, string ReservationStatus, string NumberOfRooms,
							  string Adults, string Child, string ChildF,
							  string Guide, string Total, string TotalExcluVat, string Vat, string TotalDue,
							  string BalanceDue, string Rooms, string GeneratedBy,
							  string ExchangeRate, string PaymentDescription, string AccountHolder,
							  string Bank, string AccountNumber, string SwiftCode,
							  string Description2, string Description3, string PrintedBy,
							  string PrintedDate, string AdvancedReceivedPositive, string AdvancedReceivedPositiveValue, string AdvancedReceivedNegative, string AdvancedReceivedValue)
		{
			customerCode = CustomerCode;
			customer = Customer;
			vatRegNo = VatRegNo;
			guestName = GuestName;
			bookingParty = BookingParty;
			profInvoiceNo = ProfInvoiceNo;
			date = Date;
			arrival = Arrival;
			departure = Departure;
			reservationNumber = ReservationNumber;
			voucher = Voucher;
			reservationStatus = ReservationStatus;
			numberOfRooms = NumberOfRooms;
			adults = Adults;
			child = Child;
			childF = ChildF;
			guide = Guide;
			total = Total;
			totalExcluVat = TotalExcluVat;
			vat = Vat;
			totalDue = TotalDue;
			balanceDue = BalanceDue;
			rooms = Rooms;
			generatedBy = GeneratedBy;
			exchangeRate = ExchangeRate;
			paymentDescription = PaymentDescription;
			accountHolder = AccountHolder;
			bank = Bank;
			accountNumber = AccountNumber;
			swiftCode = SwiftCode;
			description2 = Description2;
			description3 = Description3;
			printedBy = PrintedBy;
			printedDate = PrintedDate;
			advancedReceivedPositive = AdvancedReceivedPositive;
			advancedReceivedPositiveValue = AdvancedReceivedPositiveValue;
			advancedReceivedNegative = AdvancedReceivedNegative;
			advancedReceivedValue = AdvancedReceivedValue;
		}

	}
}

