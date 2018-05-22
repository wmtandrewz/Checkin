using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Collections.ObjectModel;
using checkin;

namespace Checkin
{
	public class PerformaInformation
	{
		PerformaDetails performaDetails;
		CheckInManager checkinManger = new CheckInManager();
		//List Collection
		List<PerformaItemDetails> performaItemDetails = new List<PerformaItemDetails>();

		string result = "";
		public async Task<PerformaDetails> performaInfo(string reservationID)
		{

			try
			{
				result = await checkinManger.GetPerformaDetails(reservationID);
				if (result == null || result == "Error")
				{
					MessagingCenter.Send<PerformaInformation, string>(this, Constants._proformaGeneratError, "");
				}
				else
				{

					var output = JObject.Parse(result);
					if (Enumerable.Count(output["d"]["results"]) > 0)
					{
						string advanceTextPositive = string.Empty, advanceTextNegative = string.Empty, advanceTextPositiveValue = string.Empty, advanceTextNegativeValue = string.Empty;
						try
						{
							if (Enumerable.Count(output["d"]["results"][0]["advanceLinesSet"]["results"]) > 0)
							{
								advanceTextPositive = Convert.ToString(output["d"]["results"][0]["advanceLinesSet"]["results"][0]["AdvText"]) + " :";
								advanceTextPositiveValue = Convert.ToString(output["d"]["results"][0]["advanceLinesSet"]["results"][0]["AdvValue"]);
								advanceTextNegative = Convert.ToString(output["d"]["results"][0]["advanceLinesSet"]["results"][1]["AdvText"])+ " :";
								advanceTextNegativeValue = Convert.ToString(output["d"]["results"][0]["advanceLinesSet"]["results"][1]["AdvValue"]);
							}
						}
						catch (Exception )
						{

						}
						string value = string.Format("{0:F2}", Convert.ToString(output["d"]["results"][0]["BdGrandTotal"]));
						int count = Enumerable.Count(output["d"]["results"][0]["profomaLinesSet"]["results"]);
						PerformaDetails PerformaDetails = new PerformaDetails(Convert.ToString(output["d"]["results"][0]["HdKunnr"]),
																			  Convert.ToString(output["d"]["results"][0]["HdCusName"]) + "\n" +
																			  Convert.ToString(output["d"]["results"][0]["HdCusStreet"]) + "\n" +
																			  Convert.ToString(output["d"]["results"][0]["HdCusCity"]) + " \n" +
																			  Convert.ToString(output["d"]["results"][0]["HdCusCountry"]),
																			  Convert.ToString(output["d"]["results"][0]["HdCusVatno"]),
																			  Convert.ToString(output["d"]["results"][0]["HdCusGuest"]),
																			  Convert.ToString(output["d"]["results"][0]["HdCusBookingparty"]),
																			  Convert.ToString(output["d"]["results"][0]["HdInvoice"]),
																			  FormatChanges.changedateformat(Convert.ToString(output["d"]["results"][0]["HdInvoiceDate"])),
																			  FormatChanges.changedateformat(Convert.ToString(output["d"]["results"][0]["HdArrivalDate"])),
																			  FormatChanges.changedateformat(Convert.ToString(output["d"]["results"][0]["HdDepartureDate"])),
																			  Convert.ToString(output["d"]["results"][0]["HdReservation"]),
																			  Convert.ToString(output["d"]["results"][0]["HdVoucher"]),
																			  Convert.ToString(output["d"]["results"][0]["HdResStatus"]),
																			  Convert.ToString(output["d"]["results"][0]["HdRooms"]),
																			  Convert.ToString(output["d"]["results"][0]["HdAdult"]),
																			  Convert.ToString(output["d"]["results"][0]["HdChild"]),
																			  Convert.ToString(output["d"]["results"][0]["HdChildf"]),
																			  Convert.ToString(output["d"]["results"][0]["HdGuide"]),
						                                                      serviceDataValidation.decimalTruncating(Convert.ToString(output["d"]["results"][0]["BdGrandTotal"])),
						                                                      serviceDataValidation.decimalTruncating(Convert.ToString(output["d"]["results"][0]["BdTotalExVat"])),
																			  serviceDataValidation.decimalTruncating(Convert.ToString(output["d"]["results"][0]["BdVat"])),
																			  serviceDataValidation.decimalTruncating(Convert.ToString(output["d"]["results"][0]["BdTotalDue"])),
																			  serviceDataValidation.decimalTruncating(Convert.ToString(output["d"]["results"][0]["BdBalanceDue"])),
																			  Convert.ToString(output["d"]["results"][0]["FtRoomList"]),
																			  Convert.ToString(output["d"]["results"][0]["FtGeneratedBy"]),
																			  serviceDataValidation.decimalTruncating(Convert.ToString(output["d"]["results"][0]["FtExRate"])),
																			  Convert.ToString(output["d"]["results"][0]["FtDescription1"]),
																			  Convert.ToString(output["d"]["results"][0]["FtAccHolder"]),
																			  Convert.ToString(output["d"]["results"][0]["FtBank"]),
																			  Convert.ToString(output["d"]["results"][0]["FtAccNum"]),
																			  Convert.ToString(output["d"]["results"][0]["FtSwiftCode"]),
																			  Convert.ToString(output["d"]["results"][0]["FtDescription2"]),
																			  Convert.ToString(output["d"]["results"][0]["FtDescription3"]),
																			  Convert.ToString(output["d"]["results"][0]["FtPrintedBy"]),
																			  Convert.ToString(output["d"]["results"][0]["FtExecDate"]),
																			  advanceTextPositive,
																			  advanceTextPositiveValue,
																			  advanceTextNegative,
																			  advanceTextNegativeValue
																			 );
						performaDetails = PerformaDetails;
					}
				}
			}

			catch (Exception e)
			{
				MessagingCenter.Send<PerformaInformation, string>(this, Constants._proformaGeneratError, result);
			}
			return performaDetails;
		}

		public List<PerformaItemDetails> performaItemInformation()
		{
			var output = JObject.Parse(result);
			if (Enumerable.Count(output["d"]["results"][0]["profomaLinesSet"]["results"]) > 0)
			{
				int performaItemsHeight = 0;
				int initialItem = 1;
				for (int i = 0; i < Enumerable.Count(output["d"]["results"][0]["profomaLinesSet"]["results"]); i++)
				{

					if (initialItem == 1)
					{
						performaItemsHeight = 60;
					}
					else
					{
						performaItemsHeight = performaItemsHeight + 30;
					}

					performaItemDetails.Add(new PerformaItemDetails(
					FormatChanges.changedateformat(Convert.ToString(output["d"]["results"][0]["profomaLinesSet"]["results"][i]["StartDate"])),
					FormatChanges.changedateformat(Convert.ToString(output["d"]["results"][0]["profomaLinesSet"]["results"][i]["EndDate"])),
					Convert.ToString(output["d"]["results"][0]["profomaLinesSet"]["results"][i]["Description"]),
					Convert.ToString(output["d"]["results"][0]["profomaLinesSet"]["results"][i]["RoomType"]),
					Convert.ToString(output["d"]["results"][0]["profomaLinesSet"]["results"][i]["MealPlan"]),
					Convert.ToString(output["d"]["results"][0]["profomaLinesSet"]["results"][i]["Occu"]),
					Convert.ToString(output["d"]["results"][0]["profomaLinesSet"]["results"][i]["Nos"]),
					Convert.ToString(output["d"]["results"][0]["profomaLinesSet"]["results"][i]["RoomNights"]),
					serviceDataValidation.decimalTruncating(Convert.ToString(output["d"]["results"][0]["profomaLinesSet"]["results"][i]["Rate"])),
					Convert.ToString(output["d"]["results"][0]["profomaLinesSet"]["results"][i]["RateCur"]),
					serviceDataValidation.decimalTruncating( Convert.ToString(output["d"]["results"][0]["profomaLinesSet"]["results"][i]["Amount"]))));
					initialItem = 0;
				}
				MessagingCenter.Send<PerformaInformation, int>(this, Constants._performaListHeight, performaItemsHeight);
			}
			return performaItemDetails;
		}

	}
}

