﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FontFamily=System.Drawing.FontFamily;

namespace AppFrameClient.Common
{
    public class Code39Settings
    {
        private int height = 80;

        public int BarCodeHeight
        {
            get { return height; }
            set { height = value; }
        }

        private bool drawText = true;

        public bool DrawText
        {
            get { return drawText; }
            set { drawText = value; }
        }

        private int leftMargin = 10;

        public int LeftMargin
        {
            get { return leftMargin; }
            set { leftMargin = value; }
        }

        private int rightMargin = 10;

        public int RightMargin
        {
            get { return rightMargin; }
            set { rightMargin = value; }
        }

        private int topMargin = 10;

        public int TopMargin
        {
            get { return topMargin; }
            set { topMargin = value; }
        }

        private int bottomMargin = 10;

        public int BottomMargin
        {
            get { return bottomMargin; }
            set { bottomMargin = value; }
        }

        private int interCharacterGap = 2;

        public int InterCharacterGap
        {
            get { return interCharacterGap; }
            set { interCharacterGap = value; }
        }

        private int wideWidth = 6;

        public int WideWidth
        {
            get { return wideWidth; }
            set { wideWidth = value; }
        }

        private int narrowWidth = 2;

        public int NarrowWidth
        {
            get { return narrowWidth; }
            set { narrowWidth = value; }
        }

        private System.Drawing.Font font = new System.Drawing.Font(FontFamily.GenericSansSerif, 12);

        public System.Drawing.Font Font
        {
            get { return font; }
            set { font = value; }
        }

        private int codeToTextGapHeight = 10;

        public int BarCodeToTextGapHeight
        {
            get { return codeToTextGapHeight; }
            set { codeToTextGapHeight = value; }
        }
    }
}
