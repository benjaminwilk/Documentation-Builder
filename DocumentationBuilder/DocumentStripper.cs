using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationBuilder {
    class DocumentStripper {

        DataIcons di = new DataIcons();

   //     private String ClassTitle;
   //     private ArrayList TypesAndMethods;
   //     private String[] typeName;
   //     private String[] methodName;

        String[] userText;

        public DocumentStripper(String inputText, DataIcons userDefinedDI) {
            UserClasses uc = new UserClasses(inputText);
            SetLocalData(userDefinedDI);
            ParseUserText();
            SplitTypesAndMethods();
        }

        private void SetLocalData(DataIcons userDefinedDI) {
            this.di.SetVertIcon(userDefinedDI.GetVertIcon());
            this.di.SetHoriIcon(userDefinedDI.GetHoriIcon());
            this.di.SetCrosIcon(userDefinedDI.GetCrosIcon());
            this.di.SetTypeWidth(userDefinedDI.GetTypeWidth());
            this.di.SetMethodWidth(userDefinedDI.GetMethodWidth());
        }

        public static String LeftAlignmentTextWithPadding(String passedText, int TypeOrMethod) {
            StringBuilder leftAlign = new StringBuilder();
            int spacingCount = TypeOrMethod - passedText.Length;
            leftAlign.Append(passedText);
            for (int i = spacingCount; i < TypeOrMethod; i++) {
                leftAlign.Append(" ");
            }
            return leftAlign.ToString();
        }

    /*    private String GetHorizontalLine() {
            StringBuilder horizontalLine = new StringBuilder();
            horizontalLine.Append(DevelopTypeLine());
            horizontalLine.Append(GetCrossIcon());
            horizontalLine.Append(DevelopMethodLine());
            return horizontalLine.ToString();
        }*/


        private String GetHeaderTitle() {
            //String[] headerData = this.ClassTitle.ToString().Split(' ');
            return "Class: ";// + headerData[1];
        }

    /*    public String DisplayText() {
            StringBuilder sb =  new StringBuilder();
            sb.Append(GetHeaderTitle());
            sb.Append(GetConstructorSummaryHeader());
            sb.Append("\n\n\n");
            sb.Append(GetMethodSummaryHeader());
            for (int i = 0; i < TypesAndMethods.Count; i++) {
                sb.Append("|" + LeftAlignmentTextWithPadding(this.typeName[i], this.typeWidth) + "|") ;
                sb.Append(LeftAlignmentTextWithPadding(this.methodName[i], this.methodWidth) + "|\n");
            }
            /*for (int i = 0; i < userText.Length; i++) {
                sb.Append(userText[i].Trim());
            }
            return sb.ToString();
        }*/

        public String GetMethodSummaryHeader() {
            return di.GetMethodSummaryHeader();
        }

        public String GetConstructorSummaryHeader() {
            return di.GetConstructorSummaryHeader();
        }

    }

    class DataIcons {
        private char vertIcon;
        private char horiIcon;
        private char crosIcon;
        private int typeWidth;
        private int methodWidth;

        public DataIcons() {

        }

        public DataIcons(String passedVertIcon, String passedHoriIcon, String passedCrosIcon, int passedTypeWidth, int passedMethodWidth) {
            SetVertIcon(passedVertIcon);
            SetHoriIcon(passedHoriIcon);
            SetCrosIcon(passedCrosIcon);
            SetTypeWidth(passedTypeWidth);
            SetMethodWidth(passedMethodWidth);
        }

        public DataIcons(char passedVertIcon, char passedHoriIcon, char passedCrosIcon, int passedTypeWidth, int passedMethodWidth) {
            SetVertIcon(passedVertIcon);
            SetHoriIcon(passedHoriIcon);
            SetCrosIcon(passedCrosIcon);
            SetTypeWidth(passedTypeWidth);
            SetMethodWidth(passedMethodWidth);
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

        private String GetMethodMessage() {
            return "Modifier and Type";
        }

        private String GetConstructorMessage() {
            return "Constructor and Description";
        }

        private String GetMethodDescriptionMessage() {
            return "Method and Description";
        }

        public String GetConstructorSummaryHeader() {
            StringBuilder constructorHeader = new StringBuilder();
            constructorHeader.Append(CreateMethodLine() + "\n");
            constructorHeader.Append(GetVertIcon() + DocumentStripper.LeftAlignmentTextWithPadding(GetConstructorMessage(), this.methodWidth) + GetVertIcon() + "\n");
            constructorHeader.Append(CreateMethodLine());
            return constructorHeader.ToString();
        }

        public String GetMethodSummaryHeader() {
            StringBuilder headerMessage = new StringBuilder();
            headerMessage.Append(CreateTypeLine() + CreateMethodLine() + "\n");
            headerMessage.Append(GetVertIcon() + DocumentStripper.LeftAlignmentTextWithPadding(GetMethodMessage(), this.typeWidth) + GetVertIcon() + DocumentStripper.LeftAlignmentTextWithPadding(GetMethodDescriptionMessage(), this.methodWidth) + GetVertIcon() + "\n");
            headerMessage.Append(CreateTypeLine() + CreateMethodLine());
            return headerMessage.ToString();
        }

        private String CreateTypeLine() {
            StringBuilder typeLine = new StringBuilder();
            typeLine.Append(GetCrosIcon());
            for (int i = 0; i < typeWidth; i++) {
                typeLine.Append(this.horiIcon);
            }
            return typeLine.ToString();
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

    class UserClasses {
        private String[] userInputRawData;
        private String outputClassTitle;
        private String[] outputTypes;
        private String[] outputMethodNames;
        private String[] outputMethodDescription;
        private Boolean isSet = false;

        public UserClasses(String rawUserInput) {
            SplitInputText(rawUserInput);
        }

        public void SplitInputText(String inputText) {
            //Array rawInputText;
            userInputRawData = inputText.Split('\n');
        }

        private void ParseUserText() {
            this.TypesAndMethods = new ArrayList();
            for (int i = 0; i < userText.Length; i++) {
                userText[i] = userText[i].Trim();
            }
            for (int p = 0; p < userText.Length; p++) {
                if (userText[p].Contains("class") && (this.isSet == false)) {
                    this.ClassTitle = userText[p];
                    this.isSet = true;

                }
                if (userText[p].Contains("public") || userText[p].Contains("private")) {
                    this.TypesAndMethods.Add(userText[p]);
                }
            }

        }

        private void SplitTypesAndMethods() {
            String[] splitValues;
            this.typeName = new string[100];
            this.methodName = new string[100];
            for (int i = 0; i < TypesAndMethods.Count; i++) {
                splitValues = TypesAndMethods[i].ToString().Split(' ');
                this.typeName[i] = splitValues[1];
                this.methodName[i] = splitValues[2];
            }
        }

    }
}
