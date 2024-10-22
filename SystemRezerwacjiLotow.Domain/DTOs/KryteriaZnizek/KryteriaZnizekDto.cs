
using SystemRezerwacjiLotow.Domain.Models;

namespace SystemRezerwacjiLotow.Domain.DTOs.KryteriaZnizek
{
    public class KryteriaZnizekDto : BaseSearchModel<KryteriaZnizki>
    {
        public List<KryteriaZnizki> KryteriaZnizki { get; set; }
    }
}
