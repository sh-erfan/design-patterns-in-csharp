using System;

namespace DesignPatternsDemo.StructuralPatterns
{
    // Target interface that client expects
    public interface ITarget
    {
        string GetRequest();
    }

    // Adaptee class with incompatible interface
    public class Adaptee
    {
        public string GetSpecialRequest()
        {
            return "Special request from Adaptee";
        }
    }

    // Adapter class that makes Adaptee compatible with Target interface
    public class Adapter : ITarget
    {
        private readonly Adaptee _adaptee;

        public Adapter(Adaptee adaptee)
        {
            _adaptee = adaptee;
        }

        public string GetRequest()
        {
            // Adapt the interface
            return $"Adapter: {_adaptee.GetSpecialRequest()}";
        }
    }

    /// <summary>
    /// Real-world example: Media Player Adapter
    /// </summary>
    
    // Advanced media player interface
    public interface IAdvancedMediaPlayer
    {
        void PlayVlc(string fileName);
        void PlayMp4(string fileName);
    }

    // Concrete implementations of advanced media player
    public class VlcPlayer : IAdvancedMediaPlayer
    {
        public void PlayVlc(string fileName)
        {
            Console.WriteLine($"Playing vlc file: {fileName}");
        }

        public void PlayMp4(string fileName)
        {
            // VLC player doesn't support MP4 directly
        }
    }

    public class Mp4Player : IAdvancedMediaPlayer
    {
        public void PlayVlc(string fileName)
        {
            // MP4 player doesn't support VLC directly
        }

        public void PlayMp4(string fileName)
        {
            Console.WriteLine($"Playing mp4 file: {fileName}");
        }
    }

    // Basic media player interface
    public interface IMediaPlayer
    {
        void Play(string audioType, string fileName);
    }

    // Media adapter
    public class MediaAdapter : IMediaPlayer
    {
        private IAdvancedMediaPlayer _advancedPlayer;

        public void Play(string audioType, string fileName)
        {
            switch (audioType.ToLower())
            {
                case "vlc":
                    _advancedPlayer = new VlcPlayer();
                    _advancedPlayer.PlayVlc(fileName);
                    break;
                case "mp4":
                    _advancedPlayer = new Mp4Player();
                    _advancedPlayer.PlayMp4(fileName);
                    break;
                default:
                    Console.WriteLine($"Invalid media. {audioType} format not supported");
                    break;
            }
        }
    }

    // Audio player that uses adapter
    public class AudioPlayer : IMediaPlayer
    {
        private MediaAdapter _mediaAdapter;

        public void Play(string audioType, string fileName)
        {
            // Built-in support for mp3
            if (audioType.ToLower() == "mp3")
            {
                Console.WriteLine($"Playing mp3 file: {fileName}");
            }
            // Use adapter for other formats
            else
            {
                _mediaAdapter = new MediaAdapter();
                _mediaAdapter.Play(audioType, fileName);
            }
        }
    }

    /// <summary>
    /// Usage example for Adapter Pattern
    /// </summary>
    public class AdapterExample
    {
        public static void RunExample()
        {
            Console.WriteLine("\n=== Adapter Pattern Example ===");

            // Basic adapter example
            Console.WriteLine("1. Basic Adapter Example:");
            var adaptee = new Adaptee();
            var adapter = new Adapter(adaptee);
            
            Console.WriteLine($"Client receives: {adapter.GetRequest()}");

            // Real-world media player example
            Console.WriteLine("\n2. Media Player Adapter Example:");
            var audioPlayer = new AudioPlayer();

            audioPlayer.Play("mp3", "beyond_the_horizon.mp3");
            audioPlayer.Play("mp4", "alone.mp4");
            audioPlayer.Play("vlc", "far_far_away.vlc");
            audioPlayer.Play("avi", "mind_me.avi");
        }
    }
}