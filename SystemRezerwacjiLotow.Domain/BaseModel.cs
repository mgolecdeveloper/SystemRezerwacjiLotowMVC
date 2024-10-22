namespace SystemRezerwacjiLotow.Domain
{
    public class BaseModel
    {
        public string? Message { get; set; } // informacja zwracana gdy np rekord w bazie nie został odnaleziony, lub wystąpił jakiś inny wyjątek
        public bool Success { get; set; } // jeżeli operacja na bazie się powiedzie, zwracany jest sukces
    }
}
