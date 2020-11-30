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
using System.Windows.Forms;
using System.IO;
using IWshRuntimeLibrary;

namespace Jonha
{
    public class ShortcutUtil
    {
        /// <summary>
        /// Creates a shortcut
        /// </summary>
        /// <param name="linkDirectory">Folder to put the shortcut in</param>
        /// <param name="targetPath">Application that should be launched</param>
        /// <param name="shortcutName">Name of the new shortcut</param>
        /// <param name="arguments">Arguments to use for the application</param>
        /// <param name="description">Description of the shortcut</param>
        public static string Create(string linkDirectory, string targetPath, string shortcutName, string arguments, string description)
        {
            string fileName = Path.Combine(linkDirectory, shortcutName + ".lnk");
            try
            {
                var shell = new WshShell();
                IWshShortcut link = (IWshShortcut)shell.CreateShortcut(fileName);
                link.TargetPath = targetPath;
                link.Arguments = arguments;
                link.Description = description;
                link.Save();
                return fileName;
            }
            catch (Exception e)
            {
                // TODO: Better error handling
                MessageBox.Show(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Creates a shortcut
        /// </summary>
        /// <param name="specialFolder">Special folder to put the shortcut in</param>
        /// <param name="targetPath">Application that should be launched</param>
        /// <param name="shortcutName">Name of the new shortcut</param>
        /// <param name="arguments">Arguments to use for the application</param>
        /// <param name="description">Description of the shortcut</param>
        public static string Create(Environment.SpecialFolder specialFolder, string targetPath, string shortcutName, string arguments, string description)
        {
            return Create(Environment.GetFolderPath(specialFolder), targetPath, shortcutName, arguments, description);
        }
    }
}
