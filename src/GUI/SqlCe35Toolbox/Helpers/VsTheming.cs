﻿using System;
using Microsoft.VisualStudio.Shell.Interop;
using System.Windows.Media;

namespace ErikEJ.SqlCeToolbox.Helpers
{
    public static class VsThemes
    {
        public static SolidColorBrush GetCommandBackground()
        {
            int color = (int)__VSSYSCOLOREX.VSCOLOR_COMMANDBAR_GRADIENT_BEGIN;
            return SolidColorBrushFromWin32Color(GetWin32Color(color));
        }

        public static SolidColorBrush GetWindowBackground()
        {
            return new SolidColorBrush(Color.FromArgb(0xFF, 0xF0, 0xF0, 0xF0));
            //int color = (int)__VSSYSCOLOREX3.VSCOLOR_WINDOW;
            //return SolidColorBrushFromWin32Color(GetWin32Color(color));
        }

        public static SolidColorBrush GetWindowText()
        {
            int colorval = (int)__VSSYSCOLOREX3.VSCOLOR_WINDOWTEXT;
            var color = SolidColorBrushFromWin32Color(GetWin32Color(colorval)).Color;
            //For dark theme Inactive item
            if (color.ToString() == "#FFF1F1F1")
            {
                return new SolidColorBrush(Colors.Silver);
            }
            else
            {
                return SolidColorBrushFromWin32Color(GetWin32Color(colorval));
            }
        }

        //public static SolidColorBrush GetDialogBackground()
        //{
        //    int color = (int)__VSSYSCOLOREX.VSCOLOR_PROJECTDESIGNER_TAB_BACKGROUND_GRADIENTBEGIN;
        //    return SolidColorBrushFromWin32Color(GetWin32Color(color));
        //}

        public static SolidColorBrush GetToolbarSeparatorBackground()
        {
            int color = (int)__VSSYSCOLOREX3.VSCOLOR_COMMANDBAR_TOOLBAR_SEPARATOR;
            return SolidColorBrushFromWin32Color(GetWin32Color(color));
        }

        public static SolidColorBrush GetToolWindowBackground()
        {
            int color = (int)__VSSYSCOLOREX3.VSCOLOR_WINDOW;
            return SolidColorBrushFromWin32Color(GetWin32Color(color));
        }

        private static uint GetWin32Color(int color)
        {
            uint win32Color;
            var shell = Microsoft.VisualStudio.Shell.Package.GetGlobalService(typeof(SVsUIShell)) as IVsUIShell2;
            // ReSharper disable once PossibleNullReferenceException
            shell.GetVSSysColorEx(color, out win32Color);
            return win32Color;
        }

        private static SolidColorBrush SolidColorBrushFromWin32Color(uint win32Color)
        {
            byte[] bytes = BitConverter.GetBytes(win32Color);
            return new SolidColorBrush(Color.FromArgb(0xFF, bytes[0], bytes[1], bytes[2]));            
        }
    }
}
