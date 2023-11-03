namespace backendTask.DataBase.Dto
{
    public class GetUserCartResponseDTO
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int totalPrice { get; set; }

        public int amount { get; set; }
        public string image { get; set; }
    }
}
