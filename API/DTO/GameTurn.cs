using SeaBattleWPF.API.DTO;

namespace SeaBattleRepository.DTO
{
    public class GameTurn
    {
        public bool IsMyTurn { get; set; }
        public WinState WinnerState { get; set; }
        public byte[] FieldUser { get; set; }
    }
}
