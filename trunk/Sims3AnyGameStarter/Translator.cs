/*
 * Copyright (c) 2010, Jonathan Haas
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without 
 * modification, are permitted provided that the following conditions are met:
 *    * Redistributions of source code must retain the above copyright notice, 
 *      this list of conditions and the following disclaimer.
 *    * Redistributions in binary form must reproduce the above copyright 
 *      notice, this list of conditions and the following disclaimer in the 
 *      documentation and/or other materials provided with the distribution.
 *    
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE 
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF 
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
 * POSSIBILITY OF SUCH DAMAGE. 
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Jonha.TS3.AnyGameStarter {
    public class Translator {
        static Dictionary<string, string> translatedTexts;
        static Dictionary<string, string> defaultTexts;

        /// <summary>
        /// Translates a Form. Controls need to have a Text property starting with "!" to be translated.
        /// </summary>
        /// <param name="f">The Form to translate</param>
        public static void TranslateForm(Form f) {
            if (!string.IsNullOrEmpty(f.Text) && f.Text[0] == '!')
                f.Text = GetText(f.Text.Substring(1));
            foreach (Control c in f.Controls)
                TranslateControl(c);
        }

        private static void TranslateControl(Control c) {
            if (!string.IsNullOrEmpty(c.Text) && c.Text[0] == '!')
                c.Text = GetText(c.Text.Substring(1));
            foreach (Control cc in c.Controls)
                TranslateControl(cc);
            if (c.ContextMenuStrip != null)
                TranslateControl(c.ContextMenuStrip);
            if (c is ToolStrip) {
                foreach (ToolStripItem tbb in ((ToolStrip)c).Items)
                    if (!string.IsNullOrEmpty(tbb.Text) && tbb.Text[0] == '!')
                        tbb.Text = GetText(tbb.Text.Substring(1));
            }
        }

        /// <summary>
        /// Returns a text for a specified string as written in the dictionary
        /// </summary>
        /// <param name="source">Text to translate</param>
        /// <returns>Translated text or #[source]# if the text could not be translated</returns>
        public static string GetText(string source) {
            source = source.ToLowerInvariant();
            if (translatedTexts.ContainsKey(source))
                return translatedTexts[source];
            if (defaultTexts.ContainsKey(source))
                return defaultTexts[source];
            return "#[" + source + "]#";
        }

        public static string GetText(string source, string param1) {
            return GetText(source).Replace("%1", param1);
        }

        public static string GetText(string source, string param1, string param2) {
            return GetText(source, param1).Replace("%2", param2);
        }

        public static string GetText(string source, string param1, string param2, string param3) {
            return GetText(source, param1, param2).Replace("%3", param3);
        }

        static Translator() {
            defaultTexts = readLanguage("en-US");
            string locale = System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag;
            if (locale == "en-US")
                translatedTexts = new Dictionary<string, string>();
            else
                translatedTexts = readLanguage(locale);
        }

        private static Dictionary<string, string> readLanguage(string locale) {
            string path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "localization");
            Directory.CreateDirectory(path);
            string filePath = Path.Combine(path, locale + ".txt");
            if (File.Exists(filePath))
                return ReadDictionary(filePath);

            var dirinfo = new DirectoryInfo(path);
            var files = dirinfo.GetFiles(locale.Substring(0, 3) + "*.txt");
            if (files.Length >= 1) {
                return ReadDictionary(files[0].FullName);
            }

            return new Dictionary<string, string>();
        }

        private static Dictionary<string, string> ReadDictionary(string filePath) {
            //we already know the file exists. Read it.
            Dictionary<string, string> dic = new Dictionary<string, string>();
            var reader = new StreamReader(filePath);
            while (!reader.EndOfStream) {
                var line = reader.ReadLine();
                line = line.Trim();
                if (!String.IsNullOrEmpty(line)) {
                    var arr = line.Split(new char[1] { '=' }, 2);
                    if (arr.Length == 2) {
                        dic.Add(arr[0].ToLowerInvariant(), arr[1]);
                    }
                }
            }
            return dic;
        }
    }
}

