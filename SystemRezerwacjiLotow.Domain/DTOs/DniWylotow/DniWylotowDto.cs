using SystemRezerwacjiLotow.Domain.Models;

namespace SystemRezerwacjiLotow.Domain.DTOs.DniWylotow
{
    public class DniWylotowDto : BaseSearchModel <DzienWylotu>
    {
        public List<DzienWylotu> DniWylotow { get; set; }
    }
}
