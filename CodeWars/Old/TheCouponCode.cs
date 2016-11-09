using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CodeWars
{
    public static class TheCouponCode
    {
        public static bool CheckCoupon(string enteredCode, string correctCode, string currentDate, string expirationDate)
        {
            var currentDateTime = DateTime.Parse(currentDate);
            var expirationDateTime = DateTime.Parse(expirationDate);

            if (enteredCode != correctCode || currentDateTime > expirationDateTime)
            {
                return false;
            }
            return true;
        }
    }

    [TestFixture]
    public class CouponCodeTest
    {
        [Test]
        public static void ValidCoupon()
        {
            Assert.AreEqual(true, TheCouponCode.CheckCoupon("123", "123", "September 5, 2014", "October 1, 2014"));
        }

        [Test]
        public static void InvalidCoupon()
        {
            Assert.AreEqual(false, TheCouponCode.CheckCoupon("123a", "123", "September 5, 2014", "October 1, 2014"));
        }
    }
}
