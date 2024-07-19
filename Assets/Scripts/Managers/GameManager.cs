using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        
        private GamesType _currentGame;

        public GamesType CurrentGame {
            get
            {
                return _currentGame;
            }

            set
            {
                _currentGame = value;
            }
        }

    }
}