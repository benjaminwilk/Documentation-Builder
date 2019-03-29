using System;
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

        public static Boolean IsPrintOverflow(String passedText, int TypeOrMethod) {
            if (passedText.Length > TypeOrMethod) {
                return true;
            }
            return false;
        }

        public String splitCommentText(String passedComment) {
            StringBuilder outputText = new StringBuilder();
            String builder = "";
            int characterCount = 0;
            for (int lineCount = 0; lineCount < (passedComment.Length/this.udi.GetMethodWidth()); lineCount++) {
                for (int i = 0; i < udi.GetMethodWidth(); i++) {

                    //buildLine.AppendLine();
                    //buildLine[lineCount] += passedComment[i];
                    if (passedComment.Length < characterCount) {
                        builder += "";
                    } else {
                        builder += passedComment[characterCount];
                    }
                    characterCount++;
                }
                outputText.Append(CreateTypeSpace());
                outputText.Append(LeftAlignmentTextWithPadding(builder, udi.GetMethodWidth()));
                outputText.Append(udi.GetVertIcon() + Environment.NewLine);
                builder = String.Empty;
            }
            return outputText.ToString();
        }

        public static String CheckCommentLength(String passedComment, int TypeOrMethod, TextFramework tf) {
            if (IsPrintOverflow(passedComment, TypeOrMethod)) {
                return tf.splitCommentText(passedComment);
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

        private static String GetMethodMessage() {
            return "Modifier and Type";
        }

        private static String GetConstructorMessage() {
            return "Constructor and Description";
        }

        private static String GetMethodDescriptionMessage() {
            return "Method and Description";
        }

        public String AssembleConstructorRow(String displayString) {
            StringBuilder constructionRow = new StringBuilder();
            constructionRow.Append(this.udi.GetVertIcon());
            constructionRow.Append(TextFramework.LeftAlignmentTextWithPadding(displayString, this.udi.GetMethodWidth()));
            constructionRow.Append(this.udi.GetVertIcon() + Environment.NewLine);
            constructionRow.Append(GetConstructorDivider());
            return constructionRow.ToString();
        }

        public String AssembleConstructorRow(String displayString, String displayComment) {
            StringBuilder constructionRow = new StringBuilder();
            constructionRow.Append(this.udi.GetVertIcon());
            constructionRow.Append(TextFramework.LeftAlignmentTextWithPadding(displayString + " -- " + displayComment, this.udi.GetMethodWidth()));
            constructionRow.Append(this.udi.GetVertIcon() + Environment.NewLine);
            constructionRow.Append(GetConstructorDivider());
            return constructionRow.ToString();
        }

        public String AssembleFunctionRow(String passedType, String passedMethod) {
            StringBuilder functionRow = new StringBuilder();
            functionRow.Append(Environment.NewLine + this.udi.GetVertIcon());
            functionRow.Append(TextFramework.LeftAlignmentTextWithPadding(passedType, this.udi.GetTypeWidth()));
            functionRow.Append(this.udi.GetVertIcon());
            functionRow.Append(TextFramework.LeftAlignmentTextWithPadding(passedMethod.Replace('{', ' ').Trim(), this.udi.GetMethodWidth()));
            functionRow.Append(this.udi.GetVertIcon() + Environment.NewLine);
            functionRow.Append(GetHorizontalDivider());
            return functionRow.ToString();
        }

 /*      public String AssembleFunctionRow(String passedType, String passedMethod, String passedComment) {
            StringBuilder functionRow = new StringBuilder();
            functionRow.Append(Environment.NewLine + this.udi.GetVertIcon());
            functionRow.Append(TextFramework.LeftAlignmentTextWithPadding(passedType, this.udi.GetTypeWidth()));
            functionRow.Append(this.udi.GetVertIcon());
            functionRow.Append(TextFramework.LeftAlignmentTextWithPadding(passedMethod, this.udi.GetMethodWidth()));
            functionRow.Append(this.udi.GetVertIcon() + Environment.NewLine);
            //functionRow.Append(CreateTypeSpace());
            functionRow.Append(TextFramework.CheckCommentLength(passedComment, this.udi.GetMethodWidth()));
          //  functionRow.Append(GetVertIcon() + Environment.NewLine);
            functionRow.Append(GetHorizontalDivider());
            return functionRow.ToString();
        }*/

        public String AssembleFunctionRow(String passedType, String passedMethod, String passedComment, TextFramework tf) {
            StringBuilder functionRow = new StringBuilder();
            functionRow.Append(Environment.NewLine + this.udi.GetVertIcon());
            functionRow.Append(TextFramework.LeftAlignmentTextWithPadding(passedType, this.udi.GetTypeWidth()));
            functionRow.Append(this.udi.GetVertIcon());
            functionRow.Append(TextFramework.LeftAlignmentTextWithPadding(passedMethod.Replace('{', ' ').Trim(), this.udi.GetMethodWidth()));
            functionRow.Append(this.udi.GetVertIcon() + Environment.NewLine);
            //functionRow.Append(CreateTypeSpace());
            functionRow.Append(TextFramework.CheckCommentLength(passedComment, this.udi.GetMethodWidth(), tf));
            //  functionRow.Append(GetVertIcon() + Environment.NewLine);
            functionRow.Append(GetHorizontalDivider());
            return functionRow.ToString();
        }

        public String GetConstructorSummaryHeader() {
            StringBuilder constructorHeader = new StringBuilder();
            constructorHeader.Append(CreateMethodLine() + Environment.NewLine);
            constructorHeader.Append(this.udi.GetVertIcon());
            constructorHeader.Append(LeftAlignmentTextWithPadding(GetConstructorMessage(), this.udi.GetMethodWidth()));
            constructorHeader.Append(this.udi.GetVertIcon() + Environment.NewLine);
            constructorHeader.Append(CreateMethodLine());
            return constructorHeader.ToString();
        }

        public String GetMethodSummaryHeader() {
            StringBuilder headerMessage = new StringBuilder();
            headerMessage.Append(GetHorizontalDivider() + Environment.NewLine);
            headerMessage.Append(this.udi.GetVertIcon());
            headerMessage.Append(LeftAlignmentTextWithPadding(GetMethodMessage(), this.udi.GetTypeWidth()));
            headerMessage.Append(this.udi.GetVertIcon());
            headerMessage.Append(LeftAlignmentTextWithPadding(GetMethodDescriptionMessage(), this.udi.GetMethodWidth()));
            headerMessage.Append(this.udi.GetVertIcon() + Environment.NewLine);
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
            typeLine.Append(this.udi.GetCrosIcon());
            for (int i = 0; i < this.udi.GetTypeWidth(); i++) {
                typeLine.Append(this.udi.GetHoriIcon());
            }
            return typeLine.ToString();
        }

        public String CreateTypeSpace() {
            StringBuilder typeSpace = new StringBuilder();
            typeSpace.Append(this.udi.GetVertIcon());
            for (int i = 0; i < this.udi.GetTypeWidth(); i++) {
                typeSpace.Append(" ");
            }
            typeSpace.Append(this.udi.GetVertIcon());
            return typeSpace.ToString();
        }

     /*   private String AppendSymbolBefore(char passedIcon, String typeOrLine) {
            return passedIcon + typeOrLine;
        }

        private String AppendSymbolAfter(char passedIcon, String typeOrLine) {
            return typeOrLine + passedIcon;
        }*/

        private String CreateMethodLine() {
            StringBuilder methodLine = new StringBuilder();
            methodLine.Append(this.udi.GetCrosIcon());
            for (int i = 0; i < this.udi.GetMethodWidth(); i++) {
                methodLine.Append(this.udi.GetHoriIcon());
            }
            methodLine.Append(this.udi.GetCrosIcon());
            return methodLine.ToString();
        }

        }

    class UserDefinedIcons {
        private String vertIcon;
        private String horiIcon;
        private String crosIcon;
        private int typeWidth;
        private int methodWidth;
        
        public UserDefinedIcons() {

        }

        public UserDefinedIcons(String userDefinedVert, String userDefinedHori, String userDefinedCros, int userDefinedType, int userDefinedMethod) {
            SetVertIcon(userDefinedVert);
            SetHoriIcon(userDefinedHori);
            SetCrosIcon(userDefinedCros);
            SetTypeWidth(userDefinedType);
            SetMethodWidth(userDefinedMethod);
        }

        public void SetVertIcon(String passedVertIcon) {
            this.vertIcon = passedVertIcon;
        }

        public void SetHoriIcon(String passedHoriIcon) {
            this.horiIcon = passedHoriIcon;
        }

        public void SetCrosIcon(String passedCrosIcon) {
            this.crosIcon = passedCrosIcon;
        }

        public void SetTypeWidth(int passedTypeWidth) {
            this.typeWidth = passedTypeWidth;
        }

        public void SetMethodWidth(int passedMethodWidth) {
            this.methodWidth = passedMethodWidth;
        }

        public String GetVertIcon() {
            return this.vertIcon;
        }

        public String GetHoriIcon() {
            return this.horiIcon;
        }

        public String GetCrosIcon() {
            return this.crosIcon;
        }

        public int GetTypeWidth() {
            return this.typeWidth;
        }

        public int GetMethodWidth() {
            return this.methodWidth;
        }

    }

}
