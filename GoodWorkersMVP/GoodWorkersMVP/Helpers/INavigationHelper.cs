using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GoodWorkersMVP.Helpers
{
    public interface INavigationService
    {
        Task NavigateToAsync(Page page);
    }
}
