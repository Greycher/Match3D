namespace MatchHotel.Model.Runtime.Model
{
    public class GameModelLocator
    {
        private static GameModel _gameModel = new GameModel();

        public static GameModel Locate()
        {
            return _gameModel;
        }
    }
}