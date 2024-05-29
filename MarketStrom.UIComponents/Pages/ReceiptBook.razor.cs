using MarketStrom.UIComponents.Constants;
using MarketStrom.DataModels.DTO;
using MarketStrom.DataModels.Enums;
using MarketStrom.DataModels.Models;
using MarketStrom.UIComponents.Services;
using Microsoft.AspNetCore.Components;

namespace MarketStrom.UIComponents.Pages
{
    public partial class ReceiptBook
    {
        [Inject]
        public DatabaseService DatabaseService { get; set; }

        [Inject]
        public ModelDialogService ModelDialogService { get; set; }

        protected override void OnParametersSet()
        {
            AllPerson = DatabaseService.GetAllPerson().Where(o => o.Role == (int)Role.Customer).ToList();
            FinancialData.Clear();

            foreach (var person in AllPerson)
            {
                GetPendingOrders(person.Id);
                PersonFinance finance = new PersonFinance()
                {
                    Id = person.Id,
                    City = person.City,
                    CreatedBy = person.CreatedBy,
                    CreatedOn = person.CreatedOn,
                    FirstName = person.FirstName,
                    IsDeleted = person.IsDeleted,
                    IsDeletedBy = person.IsDeletedBy,
                    LastName = person.LastName,
                    MobileNo = person.MobileNo,
                    Role = person.Role,
                    UpdatedBy = person.UpdatedBy,
                    UpdatedOn = person.UpdatedOn,
                    PaymentDueDays = PendingOrders.Count > 0 ? PendingOrders.Min(o => o.CreatedOn) : DateTime.Now
                };
                var commulitiveBal = GetCommunitiveBalance();
                var untracePaymentOrders = DatabaseService.GetAllWithoutFullPaymentOrders(finance.Id);
                double untracePayment = 0;
                foreach (var payment in untracePaymentOrders)
                {
                    untracePayment = untracePayment + payment.PaidAmount;
                }
                finance.FinalAmount = commulitiveBal - (decimal)untracePayment;
                FinancialData.Add(finance);
            }
            if (GuideContstants.ReceiptBookSelectedPerson != 0)
                SelectedPerson = AllPerson.Where(o => o.Id == GuideContstants.ReceiptBookSelectedPerson).FirstOrDefault();
        }

        public async Task PaymentDialogOpen()
        {
            if (SelectedPerson != null)
            {
                var result = await ModelDialogService.PaymentDialog();

                if (result.Data is PaymentDetails)
                {
                    PaymentDetails data = (PaymentDetails)result.Data;
                    PaymentHistory paymentOrder = new PaymentHistory()
                    {
                        PersonId = SelectedPerson.Id,
                        PaidAmount = data.PaidAmount,
                        ReceivedDate = DateTime.Now,
                        PaymentMode = data.SelectedPaymentMode,
                        IsFullPaymentCompleted = false,
                    };

                    //GET PREVIOUS ORDER WHICH HAS NOT SET AS A FULLPAYMENT
                    List<PaymentHistory> pendingPaymentOrders = DatabaseService.GetAllWithoutFullPaymentOrders(SelectedPerson.Id);

                    //ADD PREVIOUS PAYMENT ORDERS PAYMENTS WITH CURRENT ONE
                    double actualCommunitiveAmount = paymentOrder.PaidAmount;
                    foreach (var pendingOrder in pendingPaymentOrders)
                    {
                        actualCommunitiveAmount += pendingOrder.PaidAmount;
                    }

                    //CHECK IS THERE ANY COMMUNITIVE BALANCE MATCHES THE PAYMENT
                    var matchedAmount = CommunitiveBalance.Where(o => o.Value == actualCommunitiveAmount).FirstOrDefault();

                    if (matchedAmount.Equals(default(KeyValuePair<int, double>)))
                    {
                        //IF NOT MATCH ADD ORDER AS NORMALORDER WITHOUT CONSIDER FULLPAYMENT 
                        DatabaseService.InsertPaymentOrder(paymentOrder);
                    }
                    else
                    {
                        //GET LIST OF ORDERID UPTO MATCH THE COMMUNITIVEBALANCE WITH PAID PAYMENT
                        var pendingSellOrders = CommunitiveBalance.TakeWhile(kv => kv.Value != matchedAmount.Value).Select(kv => kv.Key).ToList();

                        //SET ACTUAL MATCHED SELLORDER TO PAID
                        pendingSellOrders.Add(matchedAmount.Key);
                        //CHANGE STATUS OF ORDERS TO PAID FROM PENDING
                        foreach (var sellOrder in pendingSellOrders)
                        {
                            DatabaseService.SetOrderStatusPaid(sellOrder, PaymentStatus.Paid);
                        }

                        //ADD PAYMENT ORDER WITH SELLORDERS WHICH ARE PAID WITH THIS FULL PAYMENT
                        string orderIds = string.Join(",", pendingSellOrders);
                        paymentOrder.IsFullPaymentCompleted = true;
                        paymentOrder.OrderIds = orderIds;
                        DatabaseService.InsertPaymentOrder(paymentOrder);

                        //ADD SAME ORDERS IN PREVIOUS PAYMENT ORDERS FOR TRACE
                        foreach (PaymentHistory pendingPaymentOrder in pendingPaymentOrders)
                        {
                            pendingPaymentOrder.IsFullPaymentCompleted = true;
                            pendingPaymentOrder.OrderIds = orderIds;
                            DatabaseService.UpdatePaymentOrder(pendingPaymentOrder);
                        }
                    }
                    GetPendingOrders(SelectedPerson.Id);
                    PendingPaymentOrders = DatabaseService.GetAllWithoutFullPaymentOrders(_selectedPerson.Id);
                    CalculatePendingToPaymentAmount();
                    StateHasChanged();
                }
            }
        }

        public List<Person> AllPerson { get; set; }

        private Person? _selectedPerson;
        public Person? SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
                if (_selectedPerson != null)
                {
                    GetPendingOrders(_selectedPerson.Id);
                    GuideContstants.ReceiptBookSelectedPerson = _selectedPerson.Id;
                    CalculatePendingToPaymentAmount();
                }
            }
        }

        private decimal GetCommunitiveBalance()
        {
            double CommunitiveBalance = 0;
            foreach (var order in PendingOrders)
            {
                CommunitiveBalance = CommunitiveBalance + order.TotalAmount;
            }
            return (decimal)CommunitiveBalance;
        }

        private void GetPendingOrders(int personId)
        {
            PendingOrders = DatabaseService.GetAllPendingSellOrderByPerson(personId);
            CommunitiveBalance = new Dictionary<int, double>(); //ADD COMMUNITIVE BALANCE WITH ORDER ID

            double communitiveBalance = 0;
            foreach (var order in PendingOrders)
            {
                communitiveBalance += order.TotalAmount;
                CommunitiveBalance[order.Id] = communitiveBalance;
            }
        }

        public void PersonSelected(int personId)
        {
            SelectedPerson = AllPerson.Where(o => o.Id == personId).FirstOrDefault();
        }
        private void CalculatePendingToPaymentAmount()
        {
            var commulitiveBal = GetCommunitiveBalance();
            PendingPaymentOrders = DatabaseService.GetAllWithoutFullPaymentOrders(_selectedPerson.Id);
            double untracePayment = 0;
            foreach (var order in PendingPaymentOrders)
            {
                untracePayment = untracePayment + order.PaidAmount;
            }
            PendingToPayAmount = commulitiveBal - (decimal)untracePayment;
        }

        public Dictionary<int, double> CommunitiveBalance { get; set; }
        public List<OrderDTO> PendingOrders { get; set; }
        public List<PersonFinance> FinancialData { get; set; } = new();
        public decimal PendingToPayAmount { get; set; }
        public List<PaymentHistory> PendingPaymentOrders { get; set; }
    }
}
