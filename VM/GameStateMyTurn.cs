using SeaBattleRepository.DTO;
using SeaBattleWPF.API;
using SeaBattleWPF.API.Game;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SeaBattleWPF.VM
{
    internal class GameStateMyTurn : GameState
    {
        public override void ClickField(Canvas fieldUser, MouseButtonEventArgs e)
        {
            if (fieldUser == fieldUser2)
            {
                var position = e.GetPosition(fieldUser);
                int x = (int)Math.Round(position.X) / 30;
                int y = (int)Math.Round(position.Y) / 30;
               
                Task.Run( async () =>
                {
                    //TODO: Найти квадратик, по которому стреляли
                  


                    var result = await Client.Instance.PostMessagePlainAsync($"Game/MakeTurn?idGame={Game.CurrentGame.Id}&x={x}&y={y}");

                    fieldUser.Dispatcher.Invoke(() =>
                    {
                        if (result.Item1 == System.Net.HttpStatusCode.OK)
                        {
                            int.TryParse(result.Item2, out int hitResult); // в item2 -Ю 0 1 2
                            TurnResult hit = (TurnResult)hitResult;

                            if (hit == TurnResult.WasShoot)
                            {
                                MessageBox.Show("Вы уже стреляли по этой клетке");
                                return;
                            }

                            Rectangle box = new Rectangle();
                            box.Width = 30;
                            box.Height = 30;
                            Canvas.SetLeft(box, x * 30); // Далее находим квадратик по которому стреляли и взависимости от цифры которая пришла перекрываем ег другим квадратиком
                            Canvas.SetTop(box, y * 30);
                            if (hit == TurnResult.Hit)
                            {
                                box.Fill = Brushes.Red;
                            }
                            else if (hit == TurnResult.Lose)
                            {
                                box.Fill = Brushes.Black;
                                Game.SetState(States.WaitTurn);
                            }
                            else
                            {
                                MessageBox.Show("Победа");
                            }
                            fieldUser.Children.Add(box);


                        }
                    });
                }
                );
                
            }


        }
    }
}