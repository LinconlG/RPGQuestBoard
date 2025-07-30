using RPGQuestBoard.Models;
using System.Text.Json;

namespace RPGQuestBoard.Services {
    public class QuestService {
        private readonly string _dataFilePath;

        public QuestService() {
            _dataFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "quests.json");
            Directory.CreateDirectory(Path.GetDirectoryName(_dataFilePath)!);
            if (!File.Exists(_dataFilePath))
                File.WriteAllText(_dataFilePath, "[]");
        }

        public List<Quest> GetQuests() {
            var json = File.ReadAllText(_dataFilePath);
            return JsonSerializer.Deserialize<List<Quest>>(json) ?? new List<Quest>();
        }

        public void AddQuest(Quest quest) {
            var quests = GetQuests();
            quests.Insert(0, quest);
            File.WriteAllText(_dataFilePath, JsonSerializer.Serialize(quests, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
