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
    ///     PaymentDetail Description
    /// </summary>

    public class PaymentDetail
    {
        public double Tax { set; get; }
        public int PaymentNo { set; get; }
        public double Payment { set; get; }
        public double Balance { set; get; }
        public double Interest { set; get; }
        public double Insurance { set; get; }
        public double Principle { set; get; }

        // ------------------------------------------------

        public PaymentDetail()
        {
            Tax = 0d;
            Payment = 0d;
            Balance = 0d;
            Interest = 0d;
            PaymentNo = 0;
            Insurance = 0d;
            Principle = 0d;
        }
    }
}
