using MarketStrom.UIComponents.DTO;
using MarketStrom.UIComponents.Enums;
using MarketStrom.UIComponents.Models;
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
            AllPerson = DatabaseService.GetAllPerson();
        }

        public async Task PaymentDialogOpen()
        {
            if (SelectedPerson != null)
            {
                var result = await ModelDialogService.PaymentDialog();

                if (result.Data is double)
                {
                    PaymentHistory paymentOrder = new PaymentHistory()
                    {
                        PersonId = SelectedPerson.Id,
                        PaidAmount = (double)result.Data,
                        ReceivedDate = DateTime.Now,
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

                        //CHANGE STATUS OF ORDERS TO PAID FROM PENDING
                        foreach (var sellOrder in pendingSellOrders)
                        {
                            DatabaseService.SetOrderStatusPaid(sellOrder, PaymentStatus.Paid);
                        }
                        //SET ACTUAL MATCHED SELLORDER TO PAID
                        DatabaseService.SetOrderStatusPaid(matchedAmount.Key, PaymentStatus.Paid);

                        //ADD PAYMENT ORDER WITH SELLORDERS WHICH ARE PAID WITH THIS FULL PAYMENT
                        string orderIds = string.Join(",", pendingSellOrders);
                        orderIds = orderIds + "," +  matchedAmount.Key;
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
                    PendingOrders = DatabaseService.GetAllPendingSellOrderByPerson(_selectedPerson.Id);
                    CommunitiveBalance = new Dictionary<int, double>(); //ADD COMMUNITIVE BALANCE WITH ORDER ID

                    double communitiveBalance = 0;
                    foreach (var order in PendingOrders)
                    {
                        communitiveBalance += order.TotalAmount;
                        CommunitiveBalance[order.Id] = communitiveBalance;
                    }
                }
            }
        }

        public Dictionary<int, double> CommunitiveBalance { get; set; }
        public List<OrderDTO> PendingOrders { get; set; }
    }
}
