using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentationBuilder {
    class DocumentStripper {

        DataIcons di = new DataIcons();
        UserClasses uc;
        //String[] userText;

        public DocumentStripper(String inputText, DataIcons userDefinedDI) {
            this.uc = new UserClasses(inputText);
            SetLocalData(userDefinedDI);
         //   ParseUserText();
         //   SplitTypesAndMethods();
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
            int spacingCount = passedText.Length - TypeOrMethod;
            leftAlign.Append(passedText);
            for (int i = spacingCount; i < TypeOrMethod; i++) {
                leftAlign.Append(" ");
            }
            return leftAlign.ToString();
        }

        public int GetTypeMethodLength() {
            return ReturnType().Length;
        }

        public String GetMethodSummaryHeader() {
            return di.GetMethodSummaryHeader();
        }

        public String GetConstructorSummaryHeader() {
            return di.GetConstructorSummaryHeader();
        }

        public String GetHeaderTitle() {
            return this.uc.GetHeaderTitle();
        }

        public String ReturnType(int positionValue) {
            return this.uc.ReturnType(positionValue);
        }

        public String ReturnMethodName(int positionValue) {
            return this.uc.ReturnMethodName(positionValue);
        }

        public String[] ReturnType() {
            return this.uc.ReturnType();
        }

        public String[] ReturnMethodName() {
            return this.uc.ReturnMethodName();
        }

        public String ReturnVariables(int positionValue) {
            return this.uc.ReturnVariables(positionValue);
        }

        public String ReturnMethodDescription(int positionValue) {
            return this.uc.ReturnMethodDescription(positionValue);
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
            constructorHeader.Append(GetVertIcon() + /*DocumentStripper.LeftAlignmentTextWithPadding(*/GetConstructorMessage()/*, this.methodWidth)*/ + GetVertIcon() + "\n");
            constructorHeader.Append(CreateMethodLine());
            return constructorHeader.ToString();
        }

        public String GetMethodSummaryHeader() {
            StringBuilder headerMessage = new StringBuilder();
            headerMessage.Append(GetHorizontalDivider() + "\n");
            headerMessage.Append(GetVertIcon() + /*DocumentStripper.LeftAlignmentTextWithPadding(*/GetMethodMessage()/*, this.methodWidth) */+ GetVertIcon() +/* DocumentStripper.LeftAlignmentTextWithPadding(*/GetMethodDescriptionMessage()/*, this.methodWidth) */+ GetVertIcon() + "\n");
            headerMessage.Append(GetHorizontalDivider());
            return headerMessage.ToString();
        }

        public String GetHorizontalDivider() {
            return CreateTypeLine() + CreateMethodLine();
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
        private ArrayList outputConstructor;
        private ArrayList TypesAndMethods;
        private String[] outputTypes;
        private String[] outputMethodNames;
        private ArrayList outputVariables;
        private ArrayList outputMethodDescription;
        private Boolean isSet = false;

        public UserClasses(String rawUserInput) {
            this.outputConstructor = new ArrayList();
            this.TypesAndMethods = new ArrayList();
            this.outputVariables = new ArrayList();
            this.outputMethodDescription = new ArrayList();
            SplitInputText(rawUserInput);
            ParseUserText();
            SplitTypesAndMethods();
       //     SetConstructors();
        }

        public void SplitInputText(String inputText) {
            //Array rawInputText;
            userInputRawData = inputText.Split('\n');
        }

        private void ParseUserText() {
            for (int i = 0; i < userInputRawData.Length; i++) {
                userInputRawData[i] = userInputRawData[i].Trim();
            }
            for (int p = 0; p < userInputRawData.Length; p++) {
                if (userInputRawData[p].Contains("class") && (this.isSet == false)) {
                    this.outputClassTitle = userInputRawData[p];
                    this.isSet = true;
                }
                if (userInputRawData[p].Contains("//")) {
                    this.outputMethodDescription.Add(userInputRawData[p]);
                }
                if (userInputRawData[p].Contains("public") || userInputRawData[p].Contains("private") && !userInputRawData[p].Contains("(")) {
                    this.outputVariables.Add(userInputRawData[p]);
                }
                if (userInputRawData[p].Contains("public") || userInputRawData[p].Contains("private") && userInputRawData[p].Contains("(")) {
                    this.TypesAndMethods.Add(userInputRawData[p]);
                }
            }

        }

        private void SetConstructors() {
            for (int e = 0; e < this.outputVariables.Capacity; e++) {
                if (this.outputVariables[e].ToString().Contains(this.outputClassTitle)) {
                    this.outputVariables.RemoveAt(e);
                }
            }
        }

        private void SplitTypesAndMethods() {
            String[] splitValues;
            this.outputTypes = new string[100];
            this.outputMethodNames = new string[100];
            for (int i = 0; i < TypesAndMethods.Count; i++) {
                splitValues = TypesAndMethods[i].ToString().Split(' ');
                this.outputTypes[i] = splitValues[1];
                this.outputMethodNames[i] = splitValues[2];
            }
        }

        public String GetHeaderTitle() {
            String[] headerData = this.outputClassTitle.ToString().Split(' ');
            return "Class: " + headerData[1];
        }

        public String ReturnType(int positionValue) {
            return this.outputTypes[positionValue];
        }

        public String ReturnMethodName(int positionValue) {
            return this.outputMethodNames[positionValue];
        }

        public String[] ReturnType() {
            return this.outputTypes;
        }

        public String[] ReturnMethodName() {
            return this.outputMethodNames;
        }

        public String ReturnVariables(int positionValue) {
            return this.outputVariables[positionValue].ToString();
        }

        public String ReturnMethodDescription(int positionValue) {
            if (this.outputMethodDescription.Count > positionValue) {
                return "-1";
            } else {
                return this.outputMethodDescription[positionValue].ToString();
            }
        }

    }
}
