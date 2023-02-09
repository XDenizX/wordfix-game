namespace Models
{
    public class GameSettings
    {
        public string DictionaryLanguage { get; set; } = "ru-ru";
        public bool IsMusicEnabled { get; set; } = true;
        public float MusicVolume { get; set; } = 0.5f;
    }
}