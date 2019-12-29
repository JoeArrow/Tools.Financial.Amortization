#region © 2019 JoeWare.
//
// All rights reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical, or otherwise, is prohibited
// without the prior written consent of the copyright owner.
//
#endregion

namespace Tools.Financial.Amortization
{
    public interface IAmort
    {
        CalculatePaymentResponse CalculatePayment(CalculatePaymentRequest req);
        CalculateAmortizationResponse CalculateAmortization(CalculateAmortizationRequest req);
    }
}
