﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AppFrame.Common
{
    public class AsyncController 
    {
        public delegate void AsyncDelegate();

        // must call end invoke to clean up resources by the .net runtime.
        // if there is an exception, call the OnExcption which may be overridden by
        // children.
        protected void EndAsync(IAsyncResult ar)
        {
            // clean up only.
            AsyncDelegate del = (AsyncDelegate)ar.AsyncState;
            try
            {
                del.EndInvoke(ar);
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
        }

        protected void BeginInvoke(AsyncDelegate del)
        {
            // thread the delegate, as a fire and forget.
            del.BeginInvoke(EndAsync, del);
        }

        protected virtual void OnException(System.Exception ex)
        {
            // override by childern
        }

    }
}
