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
        private String ClassTitle;
        private ArrayList TypesAndMethods;

        String[] userText;

        public DocumentStripper(String inputText, String vertIcon, String horiIcon, String crosIcon, String typeWidth, String methodWidth) {
            this.vertIcon = Char.Parse(vertIcon);
            this.horiIcon = Char.Parse(horiIcon);
            this.crosIcon = Char.Parse(crosIcon);
            this.typeWidth = int.Parse(typeWidth);
            this.methodWidth = int.Parse(methodWidth);
            SplitInputText(inputText);
            ParseUserText();
        }

        public void SplitInputText(String inputText) {
            //Array rawInputText;
            userText = inputText.Split('\n');
        }

        private void ParseUserText() {
            for(int i = 0; i < userText.Length; i++) {
                userText[i] = userText[i].Trim();
            }
            for (int p = 0; p < userText.Length; p++) {
                if (userText[p].Contains("class")) {
                    this.ClassTitle = userText[p];
                }
                if(userText[p].Contains("public")|| userText[p].Contains("private")) {
                    TypesAndMethods.Add(userText[p]);
                }
            }

        }

        private String GetVerticalIcon() {
            return "" + this.vertIcon;
        }

        private String GetCrossIcon() {
            return "" + this.crosIcon;
        }

        private String GetHorizontalLine() {
            StringBuilder horizontalLine = new StringBuilder();
            horizontalLine.Append(DevelopTypeLine());
            horizontalLine.Append(GetCrossIcon());
            horizontalLine.Append(DevelopMethodLine());
            return horizontalLine.ToString();
        }

        private String DevelopTypeLine() {
            StringBuilder typeLine = new StringBuilder();
            typeLine.Append(GetCrossIcon());
            for (int i = 0; i < typeWidth; i++) {
                typeLine.Append(this.horiIcon);
            }
            return typeLine.ToString();
        }

        private String DevelopMethodLine() {
            StringBuilder methodLine = new StringBuilder();
            methodLine.Append(GetCrossIcon());
            for (int i = 0; i < methodWidth; i++) {
                methodLine.Append(this.horiIcon);
            }
            methodLine.Append(GetCrossIcon() + "\n");
            return methodLine.ToString();
        }

        private String LeftAlignmentTextWithPadding(String passedText, int TypeOrMethod) {
            StringBuilder leftAlign = new StringBuilder();
            int spacingCount = TypeOrMethod - passedText.Length;
            leftAlign.Append(passedText);
            for (int i = spacingCount; i < TypeOrMethod; i++) {
                leftAlign.Append(" ");
            }
            return leftAlign.Append("\n").ToString();
        }

        public String GetConstructorSummaryHeader() {
            StringBuilder constructorHeader = new StringBuilder();
            constructorHeader.Append(DevelopMethodLine());
            constructorHeader.Append(GetVerticalIcon() + LeftAlignmentTextWithPadding("Constructor and Description", this.methodWidth) + GetVerticalIcon());
            constructorHeader.Append(DevelopMethodLine());
            return constructorHeader.ToString();
        }

        private String GetHeaderTitle() {
            return "Class: " + this.ClassTitle;
        }

        private String GetMethodSummaryHeader() {
            StringBuilder headerMessage = new StringBuilder();
            headerMessage.Append(GetHorizontalLine());
            headerMessage.Append(GetVerticalIcon() + LeftAlignmentTextWithPadding("Modifier and Type", this.typeWidth) + GetVerticalIcon() + LeftAlignmentTextWithPadding("Method and Description", this.methodWidth) + GetVerticalIcon());
            headerMessage.Append(GetHorizontalLine());
            return headerMessage.ToString();
        }

        public String DisplayText() {
            StringBuilder sb =  new StringBuilder();
            sb.Append(GetHeaderTitle());
            sb.Append(GetConstructorSummaryHeader());
            sb.Append("\n\n\n");
            sb.Append(GetMethodSummaryHeader());
            for (int i = 0; i < userText.Length; i++) {
                sb.Append(userText[i].Trim());
            }
            return sb.ToString();
        }

    }



}
