using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Audio;
using System.Xml;
using System.IO;

namespace TankzC
{
    static class AudioManager
    {
        static Dictionary<string, AudioClip> audio;
        static AudioSource source;
        static AudioSource stream;
        static public AudioSource Source { get { return stream; } }

        static AudioManager()
        {
            audio = new Dictionary<string, AudioClip>();
            source = new AudioSource();
            stream = new AudioSource();
        }

        public static AudioClip AddAudio(string name, string filePath)
        {
            AudioClip t = new AudioClip(filePath);
            audio.Add(name, t);

            return t;
        }

        public static AudioClip GetAudio(string name)
        {
            if (audio.ContainsKey(name))
            {
                return audio[name];
            }
            return null;
        }

        public static void SetStreamingMusic(string name, float vol)
        {
            stream.Stream(AudioManager.GetAudio(name), Game.window.deltaTime);
            stream.Volume = vol;
        }

        public static void SetAudio(string name, float volume, int pitch = 1)
        {
            AudioSource pitchAudio = new AudioSource();
            if (pitch == 1)
            {
                source.Play(AudioManager.GetAudio(name));
                source.Volume = volume;
            }
            else if (pitch != 1)
            {
                pitchAudio.Play(AudioManager.GetAudio(name));
                pitchAudio.Volume = volume;
                pitchAudio.Pitch = RandomGenerator.GetRandom(1, pitch);
            }
        }

        public static void Load()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Assets/Audio.xml");

            XmlNode root = doc.DocumentElement;

            foreach (XmlNode audiosheetNode in root.ChildNodes)
            {
                LoadAudiosheet(audiosheetNode);
            }
        }

        static void LoadAudiosheet(XmlNode audiosheetNode)
        {
            XmlNode nameNode = audiosheetNode.FirstChild;

            string name = nameNode.InnerText;
            XmlNode filenameNode = nameNode.NextSibling;
            string path = filenameNode.InnerText;

            AddAudio(name, path);
        }

        public static void Clear()
        {
            audio.Clear();
        }

    }
}
