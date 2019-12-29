#region © 2019 JoeWare.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

namespace Tools.Financial.Amortization
{
    // ----------------------------------------------------
    /// <summary>
    ///     CalculateAmortizationRequest Description
    /// </summary>

    public class CalculateAmortizationRequest
    {
        public double AlternatePaymentAmt { set; get; }
        public int AlternatePaymentNo { set; get; }
        public double AnnualTax { set; get; }
        public double LoanAmount { set; get; }
        public double DownPayment { set; get; }

        private double m_dblInterestRate;

        public double InterestRate
        {
            set
            {
                // -----------------------------------------------------
                // if value is a not a decimal, convert it to a decimal.
                // 5.75% would become .0575 for example.

                if(value > 1)
                {
                    value /= 100;
                }

                m_dblInterestRate = value;
            }

            get { return m_dblInterestRate; }
        }

        public double NumberOfPayments { set; get; }
        public double AnnualInsurancePmt { set; get; }
    }
}
