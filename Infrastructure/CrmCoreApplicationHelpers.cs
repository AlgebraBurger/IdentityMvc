using CrmCoreLite.Areas.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CrmCoreLite.Infrastructure
{
    public enum SerialType
    {
        ACCOUNT,AGENT,ORDER,INVOICE,BILL
    } 

    public class CrmCoreApplicationHelpers
    {
        
        public string getPrefix(SerialType serialType)
        {
            var prefix = "";
            switch (serialType)
            {
                case SerialType.ACCOUNT: prefix = "ACC-000-0000"; break;
                case SerialType.AGENT: prefix = "AGENT-000-0000"; break;
                case SerialType.BILL: prefix = "BILL-000-0000";  break;
                case SerialType.INVOICE: prefix = "INC-000-0000";  break;
                case SerialType.ORDER: prefix = "OR-000-0000";  break;

            }
            return prefix;
        }

        public string generateSequenceNumber(string prefix, int id)
        {
            StringBuilder sequence = new StringBuilder(prefix);
            StringBuilder sid = new StringBuilder(id.ToString());
            int sitLen = sid.Length;
            for (var i=prefix.Length;i>0;i--)
            {
                var x = i - 1;
                if (prefix[x] == '-')
                {
                    sequence[x] = prefix[x];
                }
                else
                {
                    if (sitLen>0)
                    {
                        sequence[x] = sid[sitLen-1];
                        sitLen--;
                    }
                    else
                    {
                        sequence[x] = prefix[x];
                    }
                    
                }
                
            }
            return sequence.ToString();
        }

        public decimal CalculateMonthlyBill(PaymentType paymentType, decimal totalContractAmount)
        {
            decimal result = 0;

            switch (paymentType)
            {
                case PaymentType.EASY:
                    result = (totalContractAmount / GetNumberOFMonths(paymentType)) + ((GetInterestPerMonths(paymentType) / 100) * totalContractAmount);
                    break;
                case PaymentType.MEDIUM:
                    result = (totalContractAmount / GetNumberOFMonths(paymentType)) + ((GetInterestPerMonths(paymentType) / 100) * totalContractAmount);
                    break;
                case PaymentType.HARD:
                    result = (totalContractAmount / GetNumberOFMonths(paymentType)) + ((GetInterestPerMonths(paymentType) / 100) * totalContractAmount);
                    break;
                case PaymentType.BRUTAL:
                    result = (totalContractAmount / GetNumberOFMonths(paymentType)) + ((GetInterestPerMonths(paymentType) / 100) * totalContractAmount);
                    break;
            }

            return Math.Round(result, 2);
        }

        public decimal GetNumberOFMonths(PaymentType paymentType)
        {
            decimal numberOfMonth = 0;
            switch (paymentType)
            {
                case PaymentType.EASY: numberOfMonth = 20; break;
                case PaymentType.MEDIUM: numberOfMonth = 15; break;
                case PaymentType.HARD: numberOfMonth = 10; break;
                case PaymentType.BRUTAL: numberOfMonth = 5; break;
            }
            return numberOfMonth;
        }

        public decimal GetInterestPerMonths(PaymentType paymentType)
        {
            decimal interestPerMonth = 0;
            switch (paymentType)
            {
                case PaymentType.EASY: interestPerMonth = 8; break;
                case PaymentType.MEDIUM: interestPerMonth = 4; break;
                case PaymentType.HARD: interestPerMonth = 2; break;
                case PaymentType.BRUTAL: interestPerMonth = 1; break;
            }
            return interestPerMonth;
        }
    }


}
