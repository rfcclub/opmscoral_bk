using System.Threading;
using System.Windows.Forms;
using AopAlliance.Intercept;
using CoralPOS.Interfaces.View;

namespace CoralPOS.Advice
{
    internal class UpdateStatusDisplayAdvice : IMethodInterceptor
    {
        private Thread thread;

        #region IMethodInterceptor Members

        public object Invoke(IMethodInvocation invocation)
        {
            // before process , show display value
            BeginMainFormStatusStrip();
            //process
            object returnValue = invocation.Proceed();

            // after process, update finish display
            EndMainFormStatusStrip();
            // return value
            return returnValue;
        }

        #endregion

        private void EndMainFormStatusStrip()
        {
        }

        private void BeginMainFormStatusStrip()
        {
        }

        #region Nested type: UpdateDisplay

        private delegate void UpdateDisplay(Form form);

        #endregion
    }
}