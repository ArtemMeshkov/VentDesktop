using System.Collections.Generic;

namespace BMC.Model
{
    class LiquidComparer : IEqualityComparer<LiquidModel>
    {

        public bool Equals(LiquidModel x, LiquidModel y)
        {
            return x.PumpPower == y.PumpPower ||
                x.ValvePower == y.ValvePower;

        }

        public int GetHashCode(LiquidModel obj)
        {
            return obj.PumpPower.GetHashCode() ^
                obj.ValvePower.GetHashCode();
        }
    }
}
