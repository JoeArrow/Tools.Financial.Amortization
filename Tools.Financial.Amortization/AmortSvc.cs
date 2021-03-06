﻿#region © 2019 JoeWare.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System;

namespace Tools.Financial.Amortization
{
    public class AmortSvc
    {
        public CalculateAmortizationResponse CalculateAmortization(CalculateAmortizationRequest req)
        {
            var retVal = new CalculateAmortizationResponse();

            var rate = req.InterestRate;
            var loanAmount = req.LoanAmount - req.DownPayment;
            var interest = 0d;
            var principle = 0d;

            // ---------------------------------------------------------
            // Tax and Insurance are constant, and are simply the Annual 
            // amount spread over twelve months.

            var tax = (req.AnnualTax / 12);
            var insurance = (req.AnnualInsurancePmt / 12);

            var balance = req.LoanAmount;
            var payment = (double) CalculatePayment(new CalculatePaymentRequest(req)).PaymentAmount;

            var paymentNo = 0;

            while(balance != 0 && balance > 0)
            {
                if(req.AlternatePaymentAmt > 0)
                {
                    if(paymentNo >= req.AlternatePaymentNo)
                    {
                        payment = req.AlternatePaymentAmt;
                    }
                }

                // ------------------------------------------------------
                // calculate interest rate based on remaining loan amount

                interest = (loanAmount * rate) / 12;


                if(loanAmount < payment)
                {
                    payment = loanAmount + interest + tax + insurance;
                }

                // -------------------------------------------------------------------
                // calculate principle by subtracting the interest, tax, and insurance

                principle = payment - interest - tax - insurance;

                // -----------------------------------------------------------------------
                // The new loan amount is the old loan amount minus the principal payment.

                loanAmount -= principle;

                balance = (int)loanAmount;

                // -----------------------------------------------
                // prevent a negative number once loan is paid off

                if(balance <= 0)
                {
                    loanAmount = 0;
                }

                PaymentDetail detail = new PaymentDetail();

                detail.PaymentNo = ++paymentNo;

                detail.Tax = Math.Round(tax, 2);
                detail.Payment = Math.Round(payment, 2);
                detail.Interest = Math.Round(interest, 2);
                detail.Balance = Math.Round(loanAmount, 2);
                detail.Principle = Math.Round(principle, 2);
                detail.Insurance = Math.Round(insurance, 2);

                retVal.PaymentDetails.Add(detail);
            }

            return retVal;
        }

        // ------------------------------------------------

        public CalculatePaymentResponse CalculatePayment(CalculatePaymentRequest req)
        {
            CalculatePaymentResponse retVal = new CalculatePaymentResponse();

            // --------------------------------------------------------
            // plug the values from the input into the mortgage formula

            double payment = (req.LoanAmount - req.DownPayment) * 
                             (Math.Pow((1 + req.InterestRate / 12), req.NumberOfPayments) * req.InterestRate) / 
                             (12 * (Math.Pow((1 + req.InterestRate / 12), req.NumberOfPayments) - 1));

            // ----------------------------------------------------
            // add on a monthly property tax and property insurance

            payment = payment + ((req.AnnualTax + req.AnnualInsurancePmt) / 12);

            // --------------------------------------------------------
            // place the monthly payment calculated into the temp field	

            retVal.PaymentAmount = Math.Round((decimal)payment, 2);

            return retVal;
        }
    }
}
