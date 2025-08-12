using System;

namespace DesignPatternsDemo.BehavioralPatterns
{
    /// <summary>
    /// Memento Pattern - captures and restores object state without violating encapsulation
    /// </summary>
    
    // Memento
    public class EditorMemento
    {
        private readonly string _content;
        private readonly int _cursorPosition;

        public EditorMemento(string content, int cursorPosition)
        {
            _content = content;
            _cursorPosition = cursorPosition;
        }

        public string GetContent() => _content;
        public int GetCursorPosition() => _cursorPosition;
    }

    // Originator
    public class TextEditor
    {
        private string _content = "";
        private int _cursorPosition = 0;

        public void Write(string text)
        {
            _content = _content.Insert(_cursorPosition, text);
            _cursorPosition += text.Length;
        }

        public void SetContent(string content)
        {
            _content = content;
            _cursorPosition = Math.Min(_cursorPosition, content.Length);
        }

        public void SetCursorPosition(int position)
        {
            _cursorPosition = Math.Max(0, Math.Min(position, _content.Length));
        }

        public string GetContent() => _content;
        public int GetCursorPosition() => _cursorPosition;

        public EditorMemento Save()
        {
            return new EditorMemento(_content, _cursorPosition);
        }

        public void Restore(EditorMemento memento)
        {
            _content = memento.GetContent();
            _cursorPosition = memento.GetCursorPosition();
        }

        public void ShowStatus()
        {
            Console.WriteLine($"Content: \"{_content}\"");
            Console.WriteLine($"Cursor position: {_cursorPosition}");
        }
    }

    // Caretaker
    public class EditorHistory
    {
        private System.Collections.Generic.Stack<EditorMemento> _history = 
            new System.Collections.Generic.Stack<EditorMemento>();

        public void Backup(TextEditor editor)
        {
            _history.Push(editor.Save());
            Console.WriteLine("Saved editor state to history");
        }

        public void Undo(TextEditor editor)
        {
            if (_history.Count == 0)
            {
                Console.WriteLine("No more states to restore");
                return;
            }

            var memento = _history.Pop();
            editor.Restore(memento);
            Console.WriteLine("Restored previous editor state");
        }

        public int GetHistorySize() => _history.Count;
    }

    /// <summary>
    /// Real-world example: Game state save system
    /// </summary>
    
    public class GameMemento
    {
        private readonly int _level;
        private readonly int _score;
        private readonly int _lives;
        private readonly string _playerName;

        public GameMemento(int level, int score, int lives, string playerName)
        {
            _level = level;
            _score = score;
            _lives = lives;
            _playerName = playerName;
        }

        public int GetLevel() => _level;
        public int GetScore() => _score;
        public int GetLives() => _lives;
        public string GetPlayerName() => _playerName;
    }

    public class GameState
    {
        private int _level = 1;
        private int _score = 0;
        private int _lives = 3;
        private string _playerName;

        public GameState(string playerName)
        {
            _playerName = playerName;
        }

        public void PlayLevel()
        {
            _level++;
            _score += 100;
            Console.WriteLine($"Completed level {_level - 1}!");
            ShowStatus();
        }

        public void LoseLife()
        {
            _lives--;
            Console.WriteLine("Lost a life!");
            ShowStatus();
        }

        public void ShowStatus()
        {
            Console.WriteLine($"Player: {_playerName}, Level: {_level}, Score: {_score}, Lives: {_lives}");
        }

        public GameMemento CreateSave()
        {
            return new GameMemento(_level, _score, _lives, _playerName);
        }

        public void LoadSave(GameMemento save)
        {
            _level = save.GetLevel();
            _score = save.GetScore();
            _lives = save.GetLives();
            _playerName = save.GetPlayerName();
        }
    }

    public class SaveManager
    {
        private System.Collections.Generic.Dictionary<string, GameMemento> _saves = 
            new System.Collections.Generic.Dictionary<string, GameMemento>();

        public void SaveGame(string saveSlot, GameState game)
        {
            _saves[saveSlot] = game.CreateSave();
            Console.WriteLine($"Game saved to slot: {saveSlot}");
        }

        public void LoadGame(string saveSlot, GameState game)
        {
            if (_saves.ContainsKey(saveSlot))
            {
                game.LoadSave(_saves[saveSlot]);
                Console.WriteLine($"Game loaded from slot: {saveSlot}");
            }
            else
            {
                Console.WriteLine($"No save found in slot: {saveSlot}");
            }
        }

        public void ListSaves()
        {
            Console.WriteLine("Available saves:");
            foreach (var slot in _saves.Keys)
            {
                var save = _saves[slot];
                Console.WriteLine($"  {slot}: {save.GetPlayerName()} - Level {save.GetLevel()}, Score {save.GetScore()}");
            }
        }
    }

    /// <summary>
    /// Usage example for Memento Pattern
    /// </summary>
    public class MementoExample
    {
        public static void RunExample()
        {
            Console.WriteLine("\n=== Memento Pattern Example ===");

            // Text editor example
            Console.WriteLine("1. Text Editor Example:");
            
            var editor = new TextEditor();
            var history = new EditorHistory();

            // Make some changes
            editor.Write("Hello ");
            editor.ShowStatus();
            history.Backup(editor);

            editor.Write("World!");
            editor.ShowStatus();
            history.Backup(editor);

            editor.SetCursorPosition(0);
            editor.Write("Hi, ");
            editor.ShowStatus();
            history.Backup(editor);

            // Undo changes
            Console.WriteLine("\nUndoing changes:");
            history.Undo(editor);
            editor.ShowStatus();

            history.Undo(editor);
            editor.ShowStatus();

            history.Undo(editor);
            editor.ShowStatus();

            history.Undo(editor); // No more states

            Console.WriteLine();

            // Game save system example
            Console.WriteLine("2. Game Save System Example:");
            
            var game = new GameState("Player1");
            var saveManager = new SaveManager();

            // Play some levels
            game.ShowStatus();
            saveManager.SaveGame("checkpoint1", game);

            game.PlayLevel();
            game.PlayLevel();
            saveManager.SaveGame("checkpoint2", game);

            game.PlayLevel();
            game.LoseLife();
            game.LoseLife();

            Console.WriteLine("\nCurrent state:");
            game.ShowStatus();

            Console.WriteLine("\nLoading previous save:");
            saveManager.LoadGame("checkpoint2", game);
            game.ShowStatus();

            Console.WriteLine("\nAll saves:");
            saveManager.ListSaves();

            Console.WriteLine("\nMemento pattern enables undo functionality and save systems!");
        }
    }
}