using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ModelsDapper
{
    public interface IKodyPocztoweRepository
    {
        public Task<IEnumerable<KodyPocztowe>> GetKodyPocztowe();
        public Task CreateKodPocztowy(KodyPocztowe kodyPocztowe);
    }
}
