#region © 2019 JoeWare.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

using System;
using System.Text;
using System.Collections.ObjectModel;

namespace Tools.Financial.Amortization
{
    // ----------------------------------------------------
    /// <summary>
    ///     CalculateAmortizationResponse Description
    /// </summary>

    public class CalculateAmortizationResponse
    {
        public ObservableCollection<PaymentDetail> PaymentDetails { set; get; }

        public CalculateAmortizationResponse()
        {
            PaymentDetails = new ObservableCollection<PaymentDetail>();
        }

        // ------------------------------------------------

        public string ToLog()
        {
            var crt = $"{Environment.NewLine}\t";
            var retVal = new StringBuilder();

            foreach(var detail in PaymentDetails)
            {
                retVal.Append($"{detail.ToLog()}{crt}");
            }

            return retVal.ToString();
        }
    }
}
