using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
        private NavAdvanceResult [] proformaAdvanceList;
        private NavOthersResult[] proformaOthersList;


        public ProformaInvoice()
        {
            InitializeComponent();
            BindingContext = new ProformaViewModel();



            try
            {
                MessagingCenter.Subscribe<ProformaViewModel, ProformaModel>(this, "ProformaData", (sender, arg) =>
                {
                    navLinesResults = arg.D.Results[0].NavLines.Results;
                    proformaAdvanceList = arg.D.Results[0].NavAdvance.NavAdvResults;
                    proformaOthersList = arg.D.Results[0].NavOthers.Results;

                    CreateMainProformaTable();
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
                    var voucherNoLabel = new Label { Text = navLinesResults[j].BillNo, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                    var roomTypeLabel = new Label { Text = navLinesResults[j].RoomType, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                    var mealPlanLabel = new Label { Text = navLinesResults[j].MealPlan, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                    var paxLabel = new Label { Text = navLinesResults[j].Occupancy.ToString(), VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                    var roomNightsLabel = new Label { Text = navLinesResults[j].RoomNight.ToString(), VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                    var rateLabel = new Label { Text = navLinesResults[j].Rate, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                    var curLabel = new Label { Text = navLinesResults[j].RateCurr, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                    var amountLabel = new Label { Text = navLinesResults[j].Amount, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.EndAndExpand, HorizontalTextAlignment = TextAlignment.End };

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

                grandTotalLabel.Text = grandTotal.ToString("##.000");

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
                    var valueLabel = new Label { Text = proformaAdvanceList[j].AdvValue, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.EndAndExpand, HorizontalTextAlignment = TextAlignment.End };
                    advanceTable.Children.Add(itemLabel, 1, dataRawIndex);
                    advanceTable.Children.Add(valueLabel, 3, dataRawIndex);

                    totalAdvance += Convert.ToDouble(proformaAdvanceList[j].AdvValue);

                    dataRawIndex += 2;
                }

                double balanceDue = due - totalAdvance;

                var balanceDueLabel = new Label { Text = "Balance Due", VerticalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.StartAndExpand, HorizontalTextAlignment = TextAlignment.Start };
                var balanceDueValLabel = new Label { Text = balanceDue.ToString("##.000"), VerticalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.EndAndExpand, HorizontalTextAlignment = TextAlignment.End };
                advanceTable.Children.Add(balanceDueLabel, 1, (loopCount * 2) + 1);
                advanceTable.Children.Add(balanceDueValLabel, 3, (loopCount * 2) + 1);

                totalDueLabel.Text = due.ToString("##.000");

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

                totalExVal.Text = otherDetails.TotalExclVat == "0.000" ? otherDetails.TotalExclSvat : otherDetails.TotalExclVat;
                vatLabel.Text = otherDetails.SvatPerc == "" ? $"VAT {otherDetails.VatPerc}%" : $"SVAT {otherDetails.SvatPerc}%";
                vatVal.Text = otherDetails.Svat == "0.000" ? otherDetails.Vat : otherDetails.Svat;
                totalVal.Text = otherDetails.GrandTotal;

                roomNumberVal.Text = otherDetails.RoomingList;
                createdByVal.Text = otherDetails.GeneratedBy;
                exchangeRateVal.Text = otherDetails.ExCurr + " " + otherDetails.ExRate;
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

            return date.ToString("yyyy-MMM-dd");
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
        public ObservableCollection <NavLinesResult> _navLinesResult { get; set; }

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



        public ProformaViewModel()
        {
            GetProforma();
        }

        private async void GetProforma()
        {
            try
            {


                string MessageV1 = string.Empty, MessageV2 = string.Empty, MessageV3 = string.Empty, MessageV4 = string.Empty;

                var res = await new PostServiceManager().SetPerformaInvoice("1",null);

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

                var navLinesResults = result.D.Results[0].NavLines.Results;
                var proformaAdvanceList = result.D.Results[0].NavAdvance.NavAdvResults;
                var proformaOthersList = result.D.Results[0].NavOthers.Results;

                MessagingCenter.Send<ProformaViewModel, ProformaModel>(this, "ProformaData", result);

                IsRunningIndicator = false;
                IsVisibleIndicator = false;
                IsVisibleData = true;

            }
            catch(Exception)
            {
                Debug.WriteLine("Binding Headers error");
                IsRunningIndicator = true;
                IsVisibleIndicator = true;
                IsVisibleData = false;
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

            return date.ToString("yyyy-MMM-dd");
        }

        private string ConvertToTime(string time)
        {
            return time.Replace("PT", "").Replace("H", ":").Replace("M", ":").Replace("S","  Hrs");
        }
    }
}
