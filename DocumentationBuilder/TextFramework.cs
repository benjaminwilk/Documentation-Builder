using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationBuilder {
    class TextFramework {

        UserDefinedIcons udi;

        private static String origVertIcon = "|";
        private static String origHoriIcon = "-";
        private static String origCrosIcon = "+";
        private static int origTypeWidth = 20;
        private static int origMethodWidth = 60;

        public TextFramework() {
            this.udi = new UserDefinedIcons();
            this.udi.SetVertIcon(GetOriginalVertIcon());
            this.udi.SetHoriIcon(GetOriginalHoriIcon());
            this.udi.SetCrosIcon(GetOriginalCrosIcon());
            this.udi.SetTypeWidth(GetOriginalTypeWidth());
            this.udi.SetMethodWidth(GetOriginalMethodWidth());
        }

        public TextFramework(String passedVertIcon) {
            this.udi = new UserDefinedIcons();
            this.udi.SetVertIcon(passedVertIcon);
            this.udi.SetHoriIcon(GetOriginalHoriIcon());
            this.udi.SetCrosIcon(GetOriginalCrosIcon());
            this.udi.SetTypeWidth(GetOriginalTypeWidth());
            this.udi.SetMethodWidth(GetOriginalMethodWidth());
        }

        public TextFramework(String passedVertIcon, String passedHoriIcon) {
            this.udi = new UserDefinedIcons();
            this.udi.SetVertIcon(passedVertIcon);
            this.udi.SetHoriIcon(passedHoriIcon);
            this.udi.SetCrosIcon(GetOriginalCrosIcon());
            this.udi.SetTypeWidth(GetOriginalTypeWidth());
            this.udi.SetMethodWidth(GetOriginalMethodWidth());
        }

        public TextFramework(String passedVertIcon, String passedHoriIcon, String passedCrosIcon) {
            this.udi = new UserDefinedIcons();
            this.udi.SetVertIcon(passedVertIcon);
            this.udi.SetHoriIcon(passedHoriIcon);
            this.udi.SetCrosIcon(passedCrosIcon);
            this.udi.SetTypeWidth(GetOriginalTypeWidth());
            this.udi.SetMethodWidth(GetOriginalMethodWidth());
        }

        public TextFramework(String passedVertIcon, String passedHoriIcon, String passedCrosIcon, int passedTypeWidth, int passedMethodWidth) {
            this.udi = new UserDefinedIcons();
            this.udi.SetVertIcon(passedVertIcon);
            this.udi.SetHoriIcon(passedHoriIcon);
            this.udi.SetCrosIcon(passedCrosIcon);
            this.udi.SetTypeWidth(passedTypeWidth);
            this.udi.SetMethodWidth(passedMethodWidth);
        }

        public TextFramework(int passedTypeWidth) {
            this.udi = new UserDefinedIcons();
            this.udi.SetVertIcon(GetOriginalVertIcon());
            this.udi.SetHoriIcon(GetOriginalHoriIcon());
            this.udi.SetCrosIcon(GetOriginalCrosIcon());
            this.udi.SetTypeWidth(passedTypeWidth);
            this.udi.SetMethodWidth(GetOriginalMethodWidth());
        }

        public TextFramework(int passedTypeWidth, int passedMethodWidth) {
            this.udi = new UserDefinedIcons();
            this.udi.SetVertIcon(GetOriginalVertIcon());
            this.udi.SetHoriIcon(GetOriginalHoriIcon());
            this.udi.SetCrosIcon(GetOriginalCrosIcon());
            this.udi.SetTypeWidth(passedTypeWidth);
            this.udi.SetMethodWidth(passedMethodWidth);
        }

        public static String LeftAlignmentTextWithPadding(String passedText, int TypeOrMethod) {
            StringBuilder leftAlign = new StringBuilder();
            leftAlign.Append(passedText);
            for (int i = 1; i <= TypeOrMethod - passedText.Length; i++) {
                leftAlign.Append(" ");
            }
            return leftAlign.ToString();
        }

        public static String LeftAlignmentTextWithPadding(String passedText, int TypeOrMethod, String endIcons) {
            StringBuilder leftAlign = new StringBuilder();
            leftAlign.Append(endIcons);
            leftAlign.Append(passedText);
            for (int i = 1; i <= TypeOrMethod - passedText.Length; i++) {
                leftAlign.Append(" ");
            }
            leftAlign.Append(endIcons);
            return leftAlign.ToString();
        }

        public static Boolean IsPrintOverflow(String passedText, int TypeOrMethod) {
            if (passedText.Length > TypeOrMethod) {
                return true;
            }
            return false;
        }

        public String FormatOverflowText(String passedComment, int typeOrMethodWidth) {
            StringBuilder outputText = new StringBuilder();
            ArrayList texter = new ArrayList();
            StringBuilder outputter = new StringBuilder();
            if (passedComment.Length % typeOrMethodWidth != 0) {
                do {
                    passedComment += " ";
                } while (passedComment.Length % typeOrMethodWidth != 0);
            }
            int character = 0;
            int multiplier = 1;
            for (int row = 0; row < passedComment.Length / typeOrMethodWidth; row++) {
                do {
                    outputText.Append(passedComment[character]);
                    character++;
                } while (character != (typeOrMethodWidth * multiplier));
                texter.Add(this.udi.GetVertIcon() + DevelopLine(" ", this.udi.GetTypeWidth()) + this.udi.GetVertIcon()  + outputText + this.udi.GetVertIcon());
                outputText.Clear();
                multiplier++;
            }

            for (int lines = 0; lines < texter.Count; lines++) {
                outputter.Append(texter[lines] + Environment.NewLine);
            }
            return outputter.ToString();
        }

        public String FormatConstructorText(String passedComment, int typeOrMethodWidth) {
            StringBuilder outputText = new StringBuilder();
            ArrayList texter = new ArrayList();
            StringBuilder outputter = new StringBuilder();
            if (passedComment.Length % typeOrMethodWidth != 0) {
                do {
                    passedComment += " ";
                } while (passedComment.Length % typeOrMethodWidth != 0);
            }
            int character = 0;
            int multiplier = 1;
            for (int row = 0; row < passedComment.Length / typeOrMethodWidth; row++) {
                do {
                    outputText.Append(passedComment[character]);
                    character++;
                } while (character != (typeOrMethodWidth * multiplier));
                texter.Add(this.udi.GetVertIcon() + outputText + this.udi.GetVertIcon());
                outputText.Clear();
                multiplier++;
            }

            for (int lines = 0; lines < texter.Count; lines++) {
                outputter.Append(texter[lines] + Environment.NewLine);
            }
            return outputter.ToString();
        }


        public static String CheckCommentLength(String passedComment, int TypeOrMethod, TextFramework tf) {
            if (IsPrintOverflow(passedComment, TypeOrMethod)) {
                return tf.FormatOverflowText(passedComment, TypeOrMethod);
            }
            return "";
        }

        public static String GetOriginalVertIcon() {
            return origVertIcon;
        }
        public static String GetOriginalHoriIcon() {
            return origHoriIcon;
        }

        public static String GetOriginalCrosIcon() {
            return origCrosIcon;
        }

        public static int GetOriginalTypeWidth() {
            return origTypeWidth;
        }

        public static int GetOriginalMethodWidth() {
            return origMethodWidth;
        }

        public String AssembleConstructorRow(String displayString) {
            StringBuilder constructionRow = new StringBuilder();
            constructionRow.Append(LeftAlignmentTextWithPadding(displayString, this.udi.GetMethodWidth(), this.udi.GetVertIcon()) + Environment.NewLine);
            constructionRow.Append(CreateLine(this.udi.GetMethodWidth(), this.udi.GetHoriIcon(), this.udi.GetCrosIcon()));
            return constructionRow.ToString();
        }

        public String AssembleConstructorRow(String displayString, String displayComment) {
            StringBuilder constructionRow = new StringBuilder();
            constructionRow.Append(LeftAlignmentTextWithPadding(FormatConstructorText(displayString + " -- " + displayComment, this.udi.GetMethodWidth()), this.udi.GetMethodWidth()));
            constructionRow.Append(CreateLine(this.udi.GetMethodWidth(), this.udi.GetHoriIcon(), this.udi.GetCrosIcon()));
            return constructionRow.ToString();
        }

        public String AssembleFunctionRow(String passedType, String passedMethod) {
            StringBuilder functionRow = new StringBuilder();
            functionRow.Append(Environment.NewLine + LeftAlignmentTextWithPadding(passedType, this.udi.GetTypeWidth(), this.udi.GetVertIcon()));
            functionRow.Append(LeftAlignmentTextWithPadding(passedMethod.Replace('{', ' ').Trim(), this.udi.GetMethodWidth()));
            functionRow.Append(this.udi.GetVertIcon() + Environment.NewLine);
            functionRow.Append(GetHorizontalDivider());
            return functionRow.ToString();
        }

        public String AssembleFunctionRow(String passedType, String passedMethod, String passedComment, TextFramework tf) {
            StringBuilder functionRow = new StringBuilder();
            functionRow.Append(Environment.NewLine + this.udi.GetVertIcon());
            functionRow.Append(LeftAlignmentTextWithPadding(passedType, this.udi.GetTypeWidth()));
            functionRow.Append(LeftAlignmentTextWithPadding(passedMethod.Replace('{', ' ').Trim(), this.udi.GetMethodWidth(), this.udi.GetVertIcon()) + Environment.NewLine);
            functionRow.Append(CheckCommentLength(passedComment, this.udi.GetMethodWidth(), tf));
            functionRow.Append(GetHorizontalDivider());
            return functionRow.ToString();
        }

        public String GetConstructorSummaryHeader() {
            StringBuilder constructorHeader = new StringBuilder();
            constructorHeader.Append(CreateLine(this.udi.GetMethodWidth(), this.udi.GetHoriIcon(), this.udi.GetCrosIcon()) + Environment.NewLine);
            constructorHeader.Append(LeftAlignmentTextWithPadding(DisplayHeaders.GetConstructorMessage(), this.udi.GetMethodWidth(), this.udi.GetVertIcon()) + Environment.NewLine);
            constructorHeader.Append(CreateLine(this.udi.GetMethodWidth(), this.udi.GetHoriIcon(), this.udi.GetCrosIcon()));
            return constructorHeader.ToString();
        }

        public String GetMethodSummaryHeader() {
            StringBuilder headerMessage = new StringBuilder();
            headerMessage.Append(GetHorizontalDivider() + Environment.NewLine);
            headerMessage.Append(LeftAlignmentTextWithPadding(DisplayHeaders.GetMethodMessage(), this.udi.GetTypeWidth(), this.udi.GetVertIcon()));
            headerMessage.Append(LeftAlignmentTextWithPadding(DisplayHeaders.GetMethodDescriptionMessage(), this.udi.GetMethodWidth()));
            headerMessage.Append(this.udi.GetVertIcon() + Environment.NewLine);
            headerMessage.Append(GetHorizontalDivider());
            return headerMessage.ToString();
        }

    /*    public String CreateTypeSpace() {
            StringBuilder typeSpace = new StringBuilder();
            typeSpace.Append(this.udi.GetVertIcon());
            for (int i = 0; i < this.udi.GetTypeWidth(); i++) {
                typeSpace.Append(" ");
            }
            typeSpace.Append(this.udi.GetVertIcon());
            return typeSpace.ToString();
        }*/

        public String GetHorizontalDivider() {
            return this.udi.GetCrosIcon() + DevelopLine(this.udi.GetHoriIcon(), this.udi.GetTypeWidth()) + this.udi.GetCrosIcon() + DevelopLine(this.udi.GetHoriIcon(), this.udi.GetMethodWidth()) + this.udi.GetCrosIcon();
        }

        private String DevelopLine(String passedIcon, int TypeOrMethodLength) {
            StringBuilder typeLine = new StringBuilder();
            for (int i = 0; i < TypeOrMethodLength; i++) {
                typeLine.Append(passedIcon);
            }
            return typeLine.ToString();
        }

        private String CreateLine(int lineLength) {
            StringBuilder methodLine = new StringBuilder();
            methodLine.Append(this.udi.GetCrosIcon());
            for (int i = 0; i < lineLength; i++) {
                methodLine.Append(this.udi.GetHoriIcon());
            }
            methodLine.Append(this.udi.GetCrosIcon());
            return methodLine.ToString();
        }

        private String CreateLine(int lineLength, String printIcon, String endIcons) {
            StringBuilder methodLine = new StringBuilder();
            methodLine.Append(endIcons);
            for (int i = 0; i < lineLength; i++) {
                methodLine.Append(printIcon);
            }
            methodLine.Append(endIcons);
            return methodLine.ToString();
        }

        private String CreateLine(int lineLength, String printIcon, String endIcons, Boolean beforeOrAfter) { // I know this isn't optimal, but I'm using a boolean.  True is Before, and False is After.
            StringBuilder methodLine = new StringBuilder();
            if (beforeOrAfter) {
                methodLine.Append(endIcons);
            }
            for (int i = 0; i < lineLength; i++) {
                methodLine.Append(printIcon);
            }
            if (!beforeOrAfter) {
                methodLine.Append(endIcons);
            }
            return methodLine.ToString();
        }

    }

}
