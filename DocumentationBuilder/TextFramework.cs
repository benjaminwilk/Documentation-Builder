using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationBuilder {
    class TextFramework {
        private char vertIcon;
        private char horiIcon;
        private char crosIcon;
        private int typeWidth;
        private int methodWidth;

        private static char origVertIcon = '|';
        private static char origHoriIcon = '-';
        private static char origCrosIcon = '+';
        private static int origTypeWidth = 20;
        private static int origMethodWidth = 60;

        public TextFramework(String passedVertIcon, String passedHoriIcon, String passedCrosIcon, int passedTypeWidth, int passedMethodWidth) {
            SetVertIcon(passedVertIcon);
            SetHoriIcon(passedHoriIcon);
            SetCrosIcon(passedCrosIcon);
            SetTypeWidth(passedTypeWidth);
            SetMethodWidth(passedMethodWidth);
        }

        public TextFramework(char passedVertIcon, char passedHoriIcon, char passedCrosIcon, int passedTypeWidth, int passedMethodWidth) {
            SetVertIcon(passedVertIcon);
            SetHoriIcon(passedHoriIcon);
            SetCrosIcon(passedCrosIcon);
            SetTypeWidth(passedTypeWidth);
            SetMethodWidth(passedMethodWidth);
        }

        public static String LeftAlignmentTextWithPadding(String passedText, int TypeOrMethod) {
            StringBuilder leftAlign = new StringBuilder();
         //   int spacingCount = TypeOrMethod - passedText.Length;
            leftAlign.Append(passedText);
            for (int i = 1; i <= TypeOrMethod - passedText.Length; i++) {
                leftAlign.Append(" ");
            }
            return leftAlign.ToString();
        }

        public static Boolean IsPrintOverflow(String passedText, int TypeOrMethod) {
            if (passedText.Length > TypeOrMethod) {
                return true;
            }
            return false;
        }

        public static String PrintOverflowText(String passedText, int TypeOrMethod) {
            StringBuilder overflowText = new StringBuilder();
            double rotationCount = passedText.Length / TypeOrMethod;
            if (rotationCount > 1.0) {
                for (int rows = 0; rows <= rotationCount; rows++) {
                    overflowText.Append("|");
                    for (int charCount = 0; charCount < passedText.Length; charCount++) {
                        overflowText.Append(passedText[charCount]);
                    }
                    overflowText.Append("|");
                    overflowText.Append(Environment.NewLine);
                }
                return overflowText.ToString();
            }
            return passedText;
        }

    /*    public String PrintCorrectLineLength(String passedText) {
            if (passedText.Length < GetMethodWidth()) {
                return passedText;
            }
            StringBuilder correctLength = new StringBuilder();
            double splitter = passedText.Length / GetMethodWidth();
            for (int u = 0; u <= splitter; u++) {
                correctLength.Append(Environment.NewLine + CreateTypeSpace());
                for (int count = 0; count < GetMethodWidth(); count++) {
                    correctLength.Append(passedText.ToCharArray()[count]);
                }
            }
            return correctLength.ToString();
        }*/

        public static char GetOriginalVertIcon() {
            return origVertIcon;
        }
        public static char GetOriginalHoriIcon() {
            return origHoriIcon;
        }

        public static char GetOriginalCrosIcon() {
            return origCrosIcon;
        }

        public static int GetOriginalTypeWidth() {
            return origTypeWidth;
        }

        public static int GetOriginalMethodWidth() {
            return origMethodWidth;
        }

        public void SetVertIcon(char passedVertIcon) {
            this.vertIcon = passedVertIcon;
        }

        public void SetVertIcon(String passedVertIcon) {
            this.vertIcon = Char.Parse(passedVertIcon);
        }

        public char GetVertIcon() {
            return this.vertIcon;
        }

        public void SetHoriIcon(char passedHoriIcon) {
            this.horiIcon = passedHoriIcon;
        }

        public void SetHoriIcon(String passedHoriIcon) {
            this.horiIcon = Char.Parse(passedHoriIcon);
        }

        public char GetHoriIcon() {
            return this.horiIcon;
        }

        public void SetCrosIcon(char passedCrosIcon) {
            this.crosIcon = passedCrosIcon;
        }

        public void SetCrosIcon(String passedCrosIcon) {
            this.crosIcon = Char.Parse(passedCrosIcon);
        }

        public char GetCrosIcon() {
            return this.crosIcon;
        }

        public void SetTypeWidth(int passedTypeWidth) {
            this.typeWidth = passedTypeWidth;
        }

        public int GetTypeWidth() {
            return this.typeWidth;
        }

        public void SetMethodWidth(int passedMethodWidth) {
            this.methodWidth = passedMethodWidth;
        }

        public int GetMethodWidth() {
            return this.methodWidth;
        }

        private static String GetMethodMessage() {
            return "Modifier and Type";
        }

        private static String GetConstructorMessage() {
            return "Constructor and Description";
        }

        private static String GetMethodDescriptionMessage() {
            return "Method and Description";
        }

        public String GetConstructorSummaryHeader() {
            StringBuilder constructorHeader = new StringBuilder();
            constructorHeader.Append(CreateMethodLine() + Environment.NewLine);
            constructorHeader.Append(GetVertIcon());
            constructorHeader.Append(LeftAlignmentTextWithPadding(GetConstructorMessage(), this.methodWidth));
            constructorHeader.Append(GetVertIcon() + Environment.NewLine);
            constructorHeader.Append(CreateMethodLine());
            return constructorHeader.ToString();
        }

        public String GetMethodSummaryHeader() {
            StringBuilder headerMessage = new StringBuilder();
            headerMessage.Append(GetHorizontalDivider() + Environment.NewLine);
            headerMessage.Append(GetVertIcon());
            headerMessage.Append(LeftAlignmentTextWithPadding(GetMethodMessage(), this.typeWidth));
            headerMessage.Append(GetVertIcon());
            headerMessage.Append(LeftAlignmentTextWithPadding(GetMethodDescriptionMessage(), this.methodWidth));
            headerMessage.Append(GetVertIcon() + Environment.NewLine);
            headerMessage.Append(GetHorizontalDivider());
            return headerMessage.ToString();
        }

        public String GetHorizontalDivider() {
            return CreateTypeLine() + CreateMethodLine();
        }

        public String GetConstructorDivider() {
            return CreateMethodLine();
        }

        private String CreateTypeLine() {
            StringBuilder typeLine = new StringBuilder();
            typeLine.Append(GetCrosIcon());
            for (int i = 0; i < typeWidth; i++) {
                typeLine.Append(this.horiIcon);
            }
            return typeLine.ToString();
        }

        public String CreateTypeSpace() {
            StringBuilder typeSpace = new StringBuilder();
            typeSpace.Append(GetVertIcon());
            for (int i = 0; i < GetTypeWidth(); i++) {
                typeSpace.Append(" ");
            }
            typeSpace.Append(GetVertIcon());
            return typeSpace.ToString();
        }

        private String AppendSymbolBefore(char passedIcon, String typeOrLine) {
            return passedIcon + typeOrLine;
        }

        private String AppendSymbolAfter(char passedIcon, String typeOrLine) {
            return typeOrLine + passedIcon;
        }

        private String CreateMethodLine() {
            StringBuilder methodLine = new StringBuilder();
            methodLine.Append(GetCrosIcon());
            for (int i = 0; i < GetMethodWidth(); i++) {
                methodLine.Append(GetHoriIcon());
            }
            methodLine.Append(GetCrosIcon());
            return methodLine.ToString();
        }

        }

}
