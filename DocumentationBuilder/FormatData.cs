using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;

namespace DocumentationBuilder {
    class FormatData { // This class does all the heavy lifting, it parses the data and places it in the appropriate arrays.
        private ClassContainer classContain;
        private ConstructorData constructData;
//        private ArrayList rawConstructorsorMethods;

        private ArrayList Variables;

        private ArrayList Types;
        private ArrayList MethodName;
        private ArrayList MethodComment;

        public FormatData() { // Default constructor; this initializes all the arraylists.
            this.classContain = new ClassContainer();
       //     this.rawConstructorsorMethods = new ArrayList();

            this.constructData = new ConstructorData();

            this.Variables = new ArrayList();

            this.Types = new ArrayList();
            this.MethodName = new ArrayList();
            this.MethodComment = new ArrayList();
        }

        public void SplitConstructorComment(String passedRawData) { // I'm not entirely sure what this does exactly, to be honest.  passedRawData is saved to rawConstructorsorMethods
            String[] visibilityAndConstructor = passedRawData.Split(new[] { ' ' }, 3);
            if (Regex.IsMatch(passedRawData, @"//.*$")) {
                String[] constructorAndComment = passedRawData.Split(new string[] { "//" }, StringSplitOptions.None);
                SetConstructor(visibilityAndConstructor[1], constructorAndComment[1]);
            } else {
                SetConstructor(visibilityAndConstructor[1]);
            }
        }

        public void SplitFunctionComment(String passedRawData) {
            String[] visibilityAndFunction = passedRawData.Split(new[] { ' ' }, 3);
            if (Regex.IsMatch(passedRawData, @"//.*$")) {
                String[] functionAndComment = visibilityAndFunction[2].Split(new string[] { "//" }, StringSplitOptions.None);
                SetDataLine(visibilityAndFunction[1], functionAndComment[0], functionAndComment[1]);
            } else {
                String[] removeParentheses = visibilityAndFunction[2].Split('{');
                SetDataLine(visibilityAndFunction[1], removeParentheses[0]);
            }
        }

        public void SetConstructor(String passedConstruct) { // Method that saves passed data to constructor dataset.  If only one argument is passed, constructor comment is blank.
            SetConstructorTitle(passedConstruct.Trim());
            SetConstructorComment("");
        }

        public void SetConstructor(String passedConstruct, String passedComment) { // Similar to the method above, but with a passed comment variable.
            SetConstructorTitle(passedConstruct.Trim());
            SetConstructorComment(passedComment.Trim());
        }

        public void SetVariables(String passedVariable) { // I'm not sure whether I'm going to implement this or not.  This will save passed variables to an arraylist.
            this.Variables.Add(passedVariable.Trim());
        }

        public String[] GetVariables() {
            String[] variableList = { };
            for (int i = 0; i < this.Variables.Count; i++) {
                variableList[i] = this.Variables[i].ToString();
            }
            return variableList;
        }

        public String GetVariable(int passedCount) {
            return this.Variables[passedCount].ToString();
        }

        public void SetDataLine(String passedType, String passedMethod, String passedComment) {
            this.Types.Add(passedType.Trim());
            this.MethodName.Add(passedMethod.Trim());
            this.MethodComment.Add(passedComment.Trim());
        }

        public void SetDataLine(String passedType, String passedMethod) {
            this.Types.Add(passedType.Trim());
            this.MethodName.Add(passedMethod.Trim());
            this.MethodComment.Add("");
        }

        public void SetDataLine(String passedMethod) {
            this.Types.Add("");
            this.MethodName.Add(passedMethod.Trim());
            this.MethodComment.Add("");
        }

        public int GetMethodCount() { // Another assistance function, that returns the size of the arraylist.
            return this.MethodName.Count;
        }

        public String GetMethod(int positionValue) { // Function that gets a method saved at a specific location.
            return this.MethodName[positionValue].ToString();
        }

        public String GetType(int positionValue) { // Function that gets a type saved at a specific location.
            return this.Types[positionValue].ToString();
        }

        public String GetMethodComment(int positionValue) {
            return this.MethodComment[positionValue].ToString();
        }

        // ********* Constructor Data **********
        public String ReturnConstructor(int positionValue) {
            return GetConstructorTitle(positionValue) + " -- " + GetConstructorComment(positionValue);
        }

        public int ConstructorCount() { // This is an assistance function, this is for a loop or something.
            return this.constructData.GetCount();
        }

        public String GetConstructorTitle(int positionValue) { // Function that gets a constructor saved a specific location.
            return this.constructData.GetTitle(positionValue);
        }

        public String GetConstructorComment(int positionValue) {
            return this.constructData.GetComment(positionValue);
        }

        public void SetConstructorTitle(String passedTitle) {
            this.constructData.AddTitle(passedTitle);
        }

        public void SetConstructorComment(String passedComment) {
            this.constructData.AddComment(passedComment);
        }

        public Boolean IsCommentSet(int positionValue) {
            if (String.IsNullOrEmpty(GetConstructorComment(positionValue)) || String.IsNullOrWhiteSpace(GetConstructorComment(positionValue))) {
                return false;
            }
            return true;
        }

        public void SetConstructorTitleAndComment(String passedTitle, String passedComment) {
            this.constructData.SetTitleAndComment(passedTitle, passedComment);
        }

        // ********* Class Container ***********
        public void SetClassContainerName(String passedTitle) {
            this.classContain.SetName(passedTitle);
        }

        public void SetClassContainerDescription(String passedDescription) {
            this.classContain.SetDescription(passedDescription);
        }

        public void SetClassContainerNameAndDescription(String passedTitle, String passedDescription) {
            this.classContain.SetNameAndDescription(passedTitle, passedDescription);
        }

        public String GetClassContainerName() {
            return this.classContain.GetName();
        }

        public String ReturnClassContainerName() {
            return this.classContain.ReturnClassName();
        }

        public String GetClassContainerDescription() {
            return this.classContain.GetDescription();
        }

        public Boolean IsClassContainerDescriptionSet() {
            return this.classContain.IsDescriptionSet();
        }

    }

}