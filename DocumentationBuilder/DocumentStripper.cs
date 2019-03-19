using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationBuilder {
    class DocumentStripper {

        private char vertIcon;
        private char horiIcon;
        private char crosIcon;
        private int typeWidth;
        private int methodWidth;

        String[] userText;

        public DocumentStripper(String inputText, String vertIcon, String horiIcon, String crosIcon, String typeWidth, String methodWidth) {
            this.vertIcon = Char.Parse(vertIcon);
            this.horiIcon = Char.Parse(horiIcon);
            this.crosIcon = Char.Parse(crosIcon);
            this.typeWidth = int.Parse(typeWidth);
            this.methodWidth = int.Parse(methodWidth);
            SplitInputText(inputText);
        }

        public void SplitInputText(String inputText) {
            //Array rawInputText;
            userText = inputText.Split('\n');
        }

        private void ParseUserText() {

        }

        private String GetVerticalLine() {
            return "" + this.vertIcon;
        }

        private String GetHorizontalLine() {
            StringBuilder horizontalLine = new StringBuilder();
            horizontalLine.Append(this.crosIcon);
            for (int i = 0; i < typeWidth; i++) {
                horizontalLine.Append(this.horiIcon);
            }
            horizontalLine.Append(this.crosIcon);
            for (int i = 0; i < methodWidth; i++) {
                     horizontalLine.Append(this.horiIcon);
            }
            horizontalLine.Append(this.crosIcon + "\n");
            return horizontalLine.ToString();
        }

        private String LeftAlignmentTextWithPadding(String passedText, int TypeOrMethod) {
            StringBuilder leftAlign = new StringBuilder();
            int spacingCount = TypeOrMethod - passedText.Length;
            leftAlign.Append(passedText);
            for (int i = spacingCount; i < TypeOrMethod; i++) {
                leftAlign.Append(" ");
            }
            return leftAlign.ToString();
        }

        private String GetConstructorSummaryHeader() {
            StringBuilder constructorHeader = new StringBuilder();
            constructorHeader.Append();
        }

        private String GetMethodSummaryHeader() {
            StringBuilder headerMessage = new StringBuilder();
            headerMessage.Append(GetHorizontalLine());
            headerMessage.Append(GetVerticalLine() + LeftAlignmentTextWithPadding("Modifier and Type", this.typeWidth) + GetVerticalLine() + LeftAlignmentTextWithPadding("Method and Description", this.methodWidth) + GetVerticalLine());
            headerMessage.Append(GetHorizontalLine());
            return headerMessage.ToString();
        }

        public String DisplayText() {
            StringBuilder sb =  new StringBuilder();
            sb.Append(GetMethodSummaryHeader());
            for (int i = 0; i < userText.Length; i++) {
                sb.Append(userText[i]);
            }
            return sb.ToString();
        }

    }



}
