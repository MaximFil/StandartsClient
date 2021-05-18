using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandartsClient
{
    public interface ISearch
    {
        void Search(string pattern);

        void ClearResultSearch();

        void Back();
    }
}
