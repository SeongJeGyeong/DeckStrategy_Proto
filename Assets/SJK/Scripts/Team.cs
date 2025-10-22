namespace UserData
{
    [System.Serializable]
    public class Team
    {
        private const int maxSize = 5;

        public CharacterBase[] characters = new CharacterBase[maxSize];
    }
}