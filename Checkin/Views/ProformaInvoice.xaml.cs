using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Checkin.Models.ModelClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace Checkin.Views
{
    public partial class ProformaInvoice : ContentPage
    {
        private NavLinesResult[] navLinesResults;
        private NavAdvanceResult[] proformaAdvanceList;
        private NavOthersResult[] proformaOthersList;
        private NavHeaderResult[] navHeaderResult;

        public ProformaInvoice(string folio, string responsible)
        {
            InitializeComponent();
            BindingContext = new ProformaViewModel(folio,responsible,Navigation);



            try
            {
                MessagingCenter.Subscribe<ProformaViewModel, ProformaModel>(this, "ProformaData", (sender, arg) =>
                {
                    navLinesResults = arg.D.Results[0].NavLines.Results;
                    proformaAdvanceList = arg.D.Results[0].NavAdvance.NavAdvResults;
                    proformaOthersList = arg.D.Results[0].NavOthers.Results;
                    navHeaderResult = arg.D.Results[0].NavHeader.Results;

                    CreateMainProformaTable();

                    if (Constants._hotel_code == "3300" || Constants._hotel_code == "3305" || Constants._hotel_code == "3310")
                    {
                        taxAreaLayout.IsVisible = true;
                    }
                    else
                    {
                        taxAreaLayout.IsVisible = false;
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Data Reciever error");
            }

        }

        private void CreateMainProformaTable()
        {
            try
            {
                int loopCount = navLinesResults.Length;

                amountText.Text = $"Amount ({navLinesResults[0].AmountCurr})";

                for (int i = 0; i < loopCount; i++)
                {
                    mainProformaTable.RowDefinitions.Add(new RowDefinition
                    {
                        Height = 20
                    });

                    mainProformaTable.RowDefinitions.Add(new RowDefinition
                    {
                        Height = 2
                    });
                }


                var colCount = mainProformaTable.ColumnDefinitions.Count;


                for (int ii = 2; ii <= loopCount + 1; ii++)
                {
                    var boxView = new BoxView { HeightRequest = 2, BackgroundColor = Color.Black, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.Center };
                    mainProformaTable.Children.Add(boxView, 0, (ii * 2));
                    Grid.SetColumnSpan(boxView, colCount);
                }

                var rowCount = mainProformaTable.RowDefinitions.Count;

                Grid.SetRowSpan(vbv1, rowCount);
                Grid.SetRowSpan(vbv2, rowCount);
                Grid.SetRowSpan(vbv3, rowCount);
                Grid.SetRowSpan(vbv4, rowCount);
                Grid.SetRowSpan(vbv5, rowCount);
                Grid.SetRowSpan(vbv6, rowCount);
                Grid.SetRowSpan(vbv7, rowCount);
                Grid.SetRowSpan(vbv8, rowCount);
                Grid.SetRowSpan(vbv9, rowCount);
                Grid.SetRowSpan(vbv10, rowCount);
                Grid.SetRowSpan(vbv11, rowCount);
                Grid.SetRowSpan(vbv12, rowCount);

                int dataRowInitIndex = 3;

                double grandTotal = 0;

                for (int j = 0; j < loopCount; j++)
                {
                    var startDateLabel = new Label { Text = ConvertToDateTime(navLinesResults[j].StartDate), VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                    var endDateLabel = new Label { Text = ConvertToDateTime(navLinesResults[j].EndDate), VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                    var descriptionLabel = new Label { Text = navLinesResults[j].Description, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                    var voucherNoLabel = new Label { Text = navHeaderResult[0].Voucher, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                    var roomTypeLabel = new Label { Text = navLinesResults[j].RoomType, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                    var mealPlanLabel = new Label { Text = navLinesResults[j].MealPlan, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };


                    var roomNights = "";
                    var pax = "";
                    var roomRate = "";

                    if (navLinesResults[j].Description.Contains("Accommodation"))
                    {
                        roomNights = navLinesResults[j].RoomNight.ToString();
                        pax = navLinesResults[j].Occupancy.ToString();
                        roomRate = navLinesResults[j].Rate;
                    }
                    else
                    {
                        roomNights = "";
                        pax = "";
                        roomRate = "";
                    }

                   var paxLabel = new Label { Text = pax, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }; 
                   var roomNightsLabel = new Label { Text = roomNights, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                    var rateLabel = new Label { Text = roomRate, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                    var curLabel = new Label { Text = navLinesResults[j].RateCurr, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                    var amountLabel = new Label { Text = Convert.ToDouble(navLinesResults[j].Amount).ToString("#0.00"), VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.EndAndExpand, HorizontalTextAlignment = TextAlignment.End };

                    grandTotal += Convert.ToDouble(navLinesResults[j].Amount);

                    int dataRowIndex = dataRowInitIndex + j;

                    mainProformaTable.Children.Add(startDateLabel, 1, dataRowIndex);
                    mainProformaTable.Children.Add(endDateLabel, 3, dataRowIndex);
                    mainProformaTable.Children.Add(descriptionLabel, 5, dataRowIndex);
                    mainProformaTable.Children.Add(voucherNoLabel, 7, dataRowIndex);
                    mainProformaTable.Children.Add(roomTypeLabel, 9, dataRowIndex);
                    mainProformaTable.Children.Add(mealPlanLabel, 11, dataRowIndex);
                    mainProformaTable.Children.Add(paxLabel, 13, dataRowIndex);
                    mainProformaTable.Children.Add(roomNightsLabel, 15, dataRowIndex);
                    mainProformaTable.Children.Add(rateLabel, 17, dataRowIndex);
                    mainProformaTable.Children.Add(curLabel, 19, dataRowIndex);
                    mainProformaTable.Children.Add(amountLabel, 21, dataRowIndex);

                    dataRowInitIndex++;
                }

                grandTotalLabel.Text = grandTotal.ToString("#0.00");

                CreateAdvancePaymentTable(grandTotal);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Main Lines error");
            }

        }

        private void CreateAdvancePaymentTable(double due)
        {
            try
            {
                advanceTable.IsVisible = proformaAdvanceList.Length > 0;

                int loopCount = proformaAdvanceList.Length + 1;

                for (int i = 0; i < loopCount; i++)
                {
                    advanceTable.RowDefinitions.Add(new RowDefinition
                    {
                        Height = 20
                    });

                    advanceTable.RowDefinitions.Add(new RowDefinition
                    {
                        Height = 2
                    });
                }


                var colCount = advanceTable.ColumnDefinitions.Count;


                for (int ii = 2; ii <= loopCount + 1; ii++)
                {
                    var boxView = new BoxView { HeightRequest = 2, BackgroundColor = Color.Black, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.Center };
                    advanceTable.Children.Add(boxView, 0, (ii * 2));
                    Grid.SetColumnSpan(boxView, colCount);
                }

                var rowCount = advanceTable.RowDefinitions.Count;

                Grid.SetRowSpan(adVerBoxV1, rowCount);
                Grid.SetRowSpan(adVerBoxV2, rowCount);
                Grid.SetRowSpan(adVerBoxV3, rowCount);

                int dataRawIndex = 3;
                double totalAdvance = 0;

                for (int j = 0; j <= loopCount - 2; j++)
                {
                    var itemLabel = new Label { Text = proformaAdvanceList[j].AdvText, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.StartAndExpand, HorizontalTextAlignment = TextAlignment.Start };
                    var valueLabel = new Label { Text = Convert.ToDouble(proformaAdvanceList[j].AdvValue).ToString("#0.00"), VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.EndAndExpand, HorizontalTextAlignment = TextAlignment.End };
                    advanceTable.Children.Add(itemLabel, 1, dataRawIndex);
                    advanceTable.Children.Add(valueLabel, 3, dataRawIndex);

                    totalAdvance += Convert.ToDouble(proformaAdvanceList[j].AdvValue);

                    dataRawIndex += 2;
                }

                double balanceDue = due - totalAdvance;

                var balanceDueLabel = new Label { Text = "Balance Due", VerticalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.StartAndExpand, HorizontalTextAlignment = TextAlignment.Start };
                var balanceDueValLabel = new Label { Text = balanceDue.ToString("#0.00"), VerticalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.EndAndExpand, HorizontalTextAlignment = TextAlignment.End };
                advanceTable.Children.Add(balanceDueLabel, 1, (loopCount * 2) + 1);
                advanceTable.Children.Add(balanceDueValLabel, 3, (loopCount * 2) + 1);

                totalDueLabel.Text = due.ToString("#0.00");

                CreateOtherDetailsTable();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Advance table error");
            }
        }

        private void CreateOtherDetailsTable()
        {
            try
            {
                var otherDetails = proformaOthersList[0];

                OtherAmountText.Text = $"Amount ({otherDetails.TotalCurrM})";

                //totalExVal.Text = otherDetails.TotalExclVat == "0.000" ? Convert.ToDouble(otherDetails.TotalExclSvat).ToString("#0.00") : Convert.ToDouble(otherDetails.TotalExclVat).ToString("#0.00");
                totalExVal.Text = (Convert.ToDouble(otherDetails.GrandTotal) - (Convert.ToDouble(otherDetails.ScAmtM) + Convert.ToDouble(otherDetails.GstAmtM) + Convert.ToDouble(otherDetails.GreenTaxM))).ToString("#0.00");

                //vatLabel.Text = otherDetails.SvatPerc == "" ? $"VAT {otherDetails.VatPerc}%" : $"SVAT {otherDetails.SvatPerc}%";
                //vatVal.Text = otherDetails.Svat == "0.000" ? otherDetails.Vat : otherDetails.Svat;

                serviceChargeLabel.Text = $"Service Charge {otherDetails.ScPercM}%";
                serviceChargeVal.Text = Convert.ToDouble(otherDetails.ScAmtM).ToString("#0.00");

                gstLabel.Text = $"T -  GST at {otherDetails.GstPercM}%";
                gstVal.Text = Convert.ToDouble(otherDetails.GstAmtM).ToString("#0.00");

                greenTaxVal.Text = Convert.ToDouble(otherDetails.GreenTaxM).ToString("#0.00");

                TotalText.Text = $"Total in {otherDetails.TotalCurrM}";
                totalVal.Text = Convert.ToDouble(otherDetails.GrandTotal).ToString("#0.00");

                roomNumberVal.Text = otherDetails.RoomingList;
                createdByVal.Text = otherDetails.GeneratedBy;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Other Details error");
            }
        }

        private string ConvertToDateTime(string ticks)
        {
            //var resultString = Regex.Match(ticks, @"\d+").Value;
            //DateTime myDate = new DateTime(Convert.ToInt64(resultString),DateTimeKind.Utc);

            //System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            //dateTime = dateTime.AddMilliseconds(Convert.ToInt64(resultString)).ToLocalTime();

            string sa = @"""" + ticks + @"""";
            DateTime date = JsonConvert.DeserializeObject<DateTime>(sa);

            return date.ToString("dd.MM.yyyy");
        }

        private string ConvertToTime(string time)
        {
            return time.Replace("PT", "").Replace("H", ":").Replace("M", ":").Replace("S", "  Hrs");
        }

    }

    public partial class ProformaViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public bool _isRunningIndicator = true;
        public bool _isVisibleIndicator = true;
        public bool _isVisibleData = false;

        public NavHeaderResult _proformaHeader { get; set; }
        public AccountDetailsModel _accountDetailsModel { get; set; }
        public ObservableCollection<NavLinesResult> _navLinesResult { get; set; }

        private INavigation _navigation;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public NavHeaderResult ProformaHeader
        {
            get
            {
                return _proformaHeader;
            }

            set
            {
                _proformaHeader = value;
                OnPropertyChanged("ProformaHeader");
            }
        }

        public AccountDetailsModel AccountDetails
        {
            get
            {
                return _accountDetailsModel;
            }

            set
            {
                _accountDetailsModel = value;
                OnPropertyChanged("AccountDetails");
            }
        }

        public bool IsRunningIndicator
        {
            get
            {
                return _isRunningIndicator;
            }

            set
            {
                _isRunningIndicator = value;
                OnPropertyChanged("IsRunningIndicator");
            }
        }

        public bool IsVisibleIndicator
        {
            get
            {
                return _isVisibleIndicator;
            }

            set
            {
                _isVisibleIndicator = value;
                OnPropertyChanged("IsVisibleIndicator");
            }
        }

        public bool IsVisibleData
        {
            get
            {
                return _isVisibleData;
            }

            set
            {
                _isVisibleData = value;
                OnPropertyChanged("IsVisibleData");
            }
        }



        public ProformaViewModel(string folio,string responsible,INavigation navigation)
        {
            _navigation = navigation;
            GetProforma(folio,responsible);
        }

        private async void GetProforma(string folio, string responsible)
        {
            try
            {


                string MessageV1 = string.Empty, MessageV2 = string.Empty, MessageV3 = string.Empty, MessageV4 = string.Empty;

                var res = await new PostServiceManager().SetPerformaInvoice(responsible, folio);

                if (res != null || res.Contains("Invoice successfuly created"))
                {
                    var jObj = JObject.Parse(res);
                    MessageV1 = jObj["d"]["MessageV1"].ToString();
                    MessageV2 = jObj["d"]["MessageV2"].ToString();
                    MessageV3 = jObj["d"]["MessageV3"].ToString();
                    MessageV4 = jObj["d"]["MessageV4"].ToString();
                }

                var data = await new CheckInManager().GetPerformaInvoiceNew(MessageV1);

                var result = JsonConvert.DeserializeObject<ProformaModel>(data);

                var preProHeader = result.D.Results[0].NavHeader.Results[0];
                preProHeader.ArrivalDate = ConvertToDateTime(preProHeader.ArrivalDate);
                preProHeader.DepartureDate = ConvertToDateTime(preProHeader.DepartureDate);
                preProHeader.BillDate = ConvertToDateTime(preProHeader.BillDate);
                preProHeader.ArrivalTime = ConvertToTime(preProHeader.ArrivalTime);
                preProHeader.DepartureTime = ConvertToTime(preProHeader.DepartureTime);
                preProHeader.ResvStatus = preProHeader.ResvStatus == "00" ? "Confirmed" : "";

                ProformaHeader = preProHeader;
                AccountDetails = Constants.BankAccountsDetails.FirstOrDefault(x => x.HotelCode == Constants._hotel_code);

                var navLinesResults = result.D.Results[0].NavLines.Results;
                var proformaAdvanceList = result.D.Results[0].NavAdvance.NavAdvResults;
                var proformaOthersList = result.D.Results[0].NavOthers.Results;

                MessagingCenter.Send<ProformaViewModel, ProformaModel>(this, "ProformaData", result);

                IsRunningIndicator = false;
                IsVisibleIndicator = false;
                IsVisibleData = true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Binding Headers error");
                IsRunningIndicator = true;
                IsVisibleIndicator = true;
                IsVisibleData = false;

                await Application.Current.MainPage.DisplayAlert("No Charges", "There are no charges for the selected Folio", "OK");

                await _navigation.PopAsync();

            }
        }

        private string ConvertToDateTime(string ticks)
        {
            //var resultString = Regex.Match(ticks, @"\d+").Value;
            //DateTime myDate = new DateTime(Convert.ToInt64(resultString),DateTimeKind.Utc);

            //System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            //dateTime = dateTime.AddMilliseconds(Convert.ToInt64(resultString)).ToLocalTime();

            string sa = @"""" + ticks + @"""";
            DateTime date = JsonConvert.DeserializeObject<DateTime>(sa);

            return date.ToString("dd.MM.yyyy");
        }

        private string ConvertToTime(string time)
        {
            return time.Replace("PT", "").Replace("H", ":").Replace("M", ":").Replace("S", "  Hrs");
        }
    }
}
