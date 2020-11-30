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

using System.Security.AccessControl;
using System.Security.Principal;
using Jonha.TS3.Utils;
using Microsoft.Win32;

namespace FixRegistryRights
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var game in GameUtils.InstalledGames)
            {
                if (game.RegistryKey != null) {
                    FixRights(game.RegistryKey);
                }
            }
        }

        private static void FixRights(string p)
        {
            var key = Registry.LocalMachine.OpenSubKey(p, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryRights.ChangePermissions);
            var access = key.GetAccessControl();
            access.AddAccessRule(AllowAccessRule);
            key.SetAccessControl(access);
            key.Close();
        }

        private static RegistryAccessRule _rule;

        private static RegistryAccessRule AllowAccessRule
        {
            get
            {
                if (_rule == null)
                {
                    _rule = new RegistryAccessRule(
                        new NTAccount(
                            WindowsIdentity.GetCurrent().Name
                            ),
                        System.Security.AccessControl.RegistryRights.ChangePermissions | RegistryRights.WriteKey,
                        System.Security.AccessControl.AccessControlType.Allow
                        );
                }
                return _rule;
            }
        }
    }
}
