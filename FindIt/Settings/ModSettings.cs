﻿using System.Xml.Serialization;
using UnityEngine;


namespace FindIt
{
    /// <summary>
    /// Class to hold global mod settings.
    /// </summary>
    [XmlRoot(ElementName = "FindIt2", Namespace = "", IsNullable = false)]
    internal static class Settings
    {
        internal static bool hideDebugMessages = true;

        internal static bool unlockAll =false;

        internal static bool centerToolbar = true;

        internal static bool fixBadProps = false;

        internal static KeyBinding searchKey = new KeyBinding { keyCode = (int)KeyCode.F, control = true, shift = false, alt = false };


        /// <summary>
        /// Checks to see if the search hotkey has been pressed.
        /// </summary>
        /// <returns>True if pressed, false otherwise</returns>
        public static bool IsSearchPressed()
        {
            // Keycode is lower 7 nibbles of CO InputKey.
            //KeyCode keyCode = (KeyCode)searchKey & 0xFFFFFFF);

            // Don't do anything if a keycode hasn't been set, or if the key isn't pressed.
            if (searchKey.keyCode == (int)KeyCode.None || !Input.GetKey((KeyCode)searchKey.keyCode))
            {
                return false;
            }

            // Check for control.
            if (searchKey.control && !(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
            {
                return false;
            }

            // Check for shift.
            if (searchKey.shift && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
            {
                return false;
            }

            // Check for alt.
            if (searchKey.shift && !(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt) || Input.GetKey(KeyCode.AltGr)))
            {
                return false;
            }

            // If we got here, all checks have passed - search has been pressed.
            return true;
        }
    }


    /// <summary>
    /// Defines the XML settings file.
    /// </summary>
    [XmlRoot(ElementName = "FindIt", Namespace = "", IsNullable = false)]
    public class XMLSettingsFile
    {
        [XmlElement("HideDebugMessages")]
        public bool HideDebugMessages { get => Settings.hideDebugMessages; set => Settings.hideDebugMessages = value; }

        [XmlElement("UnlockAll")]
        public bool UnlockAll { get => Settings.unlockAll; set => Settings.unlockAll = value; }

        [XmlElement("CenterToolbar")]
        public bool CenterToolbar { get => Settings.centerToolbar; set => Settings.centerToolbar = value; }

        [XmlElement("FixBadProps")]
        public bool FixBadProps { get => Settings.fixBadProps; set => Settings.fixBadProps = value; }

        [XmlElement("SearchKey")]
        public KeyBinding SearchKey { get => Settings.searchKey; set => Settings.searchKey = value; }

        [XmlElement("Language")]
        public string Language
        {
            get
            {
                return Translations.Language;
            }
            set
            {
                Translations.Language = value;
            }
        }
    }


    /// <summary>
    /// Basic keybinding class - code and modifiers.
    /// </summary>
    public struct KeyBinding
    {
        [XmlAttribute("KeyCode")]
        public int keyCode;

        [XmlAttribute("Control")]
        public bool control;

        [XmlAttribute("Shift")]
        public bool shift;

        [XmlAttribute("Alt")]
        public bool alt;
    }
}