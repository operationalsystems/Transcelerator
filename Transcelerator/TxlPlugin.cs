﻿// ---------------------------------------------------------------------------------------------
#region // Copyright (c) 2013, SIL International. All Rights Reserved.   
// <copyright from='2013' to='2013' company='SIL International'>
//		Copyright (c) 2013, SIL International. All Rights Reserved.   
//
//		Distributable under the terms of either the Common Public License or the
//		GNU Lesser General Public License, as specified in the LICENSING.txt file.
// </copyright> 
#endregion
// 
// File: UNSQuestionsDialog.cs
// ---------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Paratext.PluginFramework;
using SILUBS.SharedScrUtils;

namespace SILUBS.Transcelerator
{
    [PartCreationPolicy(CreationPolicy.Shared)]
    [Export(typeof(ParatextPlugin))]
    [ExportMetadata("InsertAfterMenuName", "Tools|Text Converter")]
    [ExportMetadata("MenuText", "Transcelerator")]
    [ExportMetadata("RequiresActiveProject", true)]
    public class TxlPlugin : ParatextPlugin, ParatextMenuPlugin, PluginWithSharedProjectData
    {
        public TxlPlugin()
        {
        }

        public Dictionary<string, PluginDataFileMergeInfo> DataFileKeySpecifications
        {
            get { return ParatextDataFileProxy.GetDataFileKeySpecifications(); }
        }

        [Import(applicationName)]
        public string CallingApplicationName { get; set; }

        [Import(getKeyTerms)]
        public GetKeyTermsDelegate GetKeyTerms { get; set; }

        [Import(getProjectFont)]
        public Func<string, Font> GetVernFont { get; set; }

        [Import(getProjectLanguageId)]
        public Func<string, string, string> GetVernIcuLocale { get; set; }

        [Import(getProjectRtL)]
        public Func<string, bool> GetVernRtoL { get; set; }

        [Import("LCF Folder")]
        public Func<string> DefaultLcfFolder { get; set; }

        [Import(getCurrentReference)]
        public Func<IScrVers, int> GetCurrentRef { get; set; }

        [Import(getVersification)]
        public Func<string, IScrVers> GetVersification { get; set; }

        [Import(displayKeyTerm)]
        public Action<string, IEnumerable<IKeyTerm>> DisplayKeyTerm { get; set; }

        [Import(getPluginData)]
        public Func<ParatextPlugin, string, string, TextReader> GetPlugInData { get; set; }

        [Import(putPluginData)]
        public Func<ParatextPlugin, string, string, TextReader, bool> PutPlugInData { get; set; }

        public void HandleMenuClick(string projectName)
        {
            TxlSplashScreen splashScreen = new TxlSplashScreen();
            splashScreen.Show(Screen.FromPoint(Properties.Settings.Default.WindowLocation));
            splashScreen.Message = string.Format(
                Properties.Resources.kstidSplashMsgRetrievingDataFromCaller, CallingApplicationName);

            IScrVers englishVersification = GetVersification("English");
            int currRef = GetCurrentRef(englishVersification);
            BCVRef startRef = new BCVRef(currRef);
            startRef.Chapter = 1;
            startRef.Verse = 1;
            BCVRef endRef = new BCVRef(currRef);
            endRef.Chapter = englishVersification.LastChapter(endRef.Book);
            endRef.Verse = englishVersification.LastVerse(endRef.Book, endRef.Chapter);

            var unsDlg = new UNSQuestionsDialog(splashScreen, projectName,
                GetKeyTerms(projectName), GetVernFont(projectName),
                GetVernIcuLocale(projectName, "generate templates"), GetVernRtoL(projectName),
                new ParatextDataFileProxy(fileId => GetPlugInData(this, projectName, fileId),
                    (fileId, reader) => PutPlugInData(this, projectName, fileId, reader)),
                DefaultLcfFolder(), CallingApplicationName, englishVersification, startRef,
                endRef, b => { }, terms => DisplayKeyTerm(projectName, terms));

            unsDlg.Show();
        }
    }
}
