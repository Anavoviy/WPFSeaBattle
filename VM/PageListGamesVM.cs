using SeaBattleRepository.DTO;
using SeaBattleWPF.API;
using SeaBattleWPF.API.Game;
using SeaBattleWPF.mvvm;
using SeaBattleWPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleWPF.VM
{
    public class PageListGamesVM : BaseVM
    {
        private GameDTO selectedGame;
        private List<GameDTO> games;

        public List<GameDTO> Games { 
            get => games;
            set
            {
                games = value;
                Signal();
            }
        }
        public GameDTO SelectedGame { 
            get => selectedGame;
            set
            {
                selectedGame = value;
                Signal();
            }
        }

        public CommandVM CreateGame { get; set; }
        public CommandVM JoinGame { get; set; }
        public CommandVM RefreshGames { get; set; }

        public PageListGamesVM()
        {
            ListGames();

            RefreshGames = new CommandVM(ListGames);

            CreateGame = new CommandVM(async () =>
            {
                var gameResult = await Client.Instance.PostMessageAsync<GameDTO>("GameInfo/CreateGame");
                if (gameResult.Item1 == HttpStatusCode.OK)
                    OpenCreatedGame(gameResult.Item2, true);
            });

            JoinGame = new CommandVM(async () =>
            {
                if (SelectedGame != null)
                {
                    var result = await Client.Instance.PostMessagePlainAsync("GameInfo/JoinGame?idGame=" + SelectedGame.Id);
                    if (result.Item1 == HttpStatusCode.OK)
                        OpenCreatedGame(SelectedGame, false);
                }
            });
        }

        private void OpenCreatedGame(GameDTO game, bool iCreator)
        {
            Game.CurrentGame = game;
            Game.CreatorIsCurrentUser = iCreator;
            PageControl.GetInstance().CurrentPage =
                new GamePage();
        }

        private void ListGames()
        {
            Task.Run(GetGames);
        }

        private async Task GetGames()
        {
            var result = await Client.Instance.PostMessageAsync<List<GameDTO>>("GameInfo/ListGame");
            if (result.Item1 == System.Net.HttpStatusCode.OK)
                Games = result.Item2;
        }
    }
}
