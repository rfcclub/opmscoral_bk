using CoralPOS.Interfaces.Logic;

/// <summary>
/// Summary description for CounterLogic
/// </summary>
/// 
namespace CoralPOS.Logic
{
    public class CounterLogic : ICounter
    {
        public int GetCounter()
        {
            return 100;
        }
    }
}