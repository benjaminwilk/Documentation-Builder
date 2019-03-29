using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentationBuilder {
    class DocumentStripper {

        //String[] userText;
        private String[] userInputRawData;

        public DocumentStripper(String inputText, FormatData fd) {
            SplitInputText(inputText);
            ParseUserText(fd);
        }

        public void SplitInputText(String inputText) {
            userInputRawData = inputText.Split('\n');
        }

        private void ParseUserText(FormatData fd) {
            for (int i = 0; i < userInputRawData.Length; i++) {
                userInputRawData[i] = userInputRawData[i].Trim();
            }
            for (int p = 0; p < userInputRawData.Length; p++) {
                if (userInputRawData[p].Contains("class")) {
                    fd.SetClassName(userInputRawData[p]);
                }
                String constructorMatch = @"^(public|private).([^\s]+).\(";
                String functionMatch = @"^(public|private).\w+\s\w+\(";
                String functionOrConstructor = @"^(public|private)\s\w+.*$";
                foreach (Match m in Regex.Matches(userInputRawData[p].ToString(), functionOrConstructor)) {
                    if (!m.Value.Contains('=') && Regex.IsMatch(m.Value, constructorMatch)) {
                        fd.SplitConstructorComment(m.Value);
                    }
                    else if (!m.Value.Contains('=') && Regex.IsMatch(m.Value, functionMatch)) {
                        fd.SplitFunctionComment(m.Value);
                    }
                    //fd.SplitConstructorComment(m.Value);
                }
                //  fd.SplitDataAndCategorize();
            }

        }
    }

    class FormatData { // This class does all the heavy lifting, it parses the data and places it in the appropriate arrays.
        String ClassName;
        ArrayList rawConstructorsorMethods;

        ArrayList Constructor;
        ArrayList ConstructorComment;

        ArrayList Variables;

        ArrayList Types;
        ArrayList MethodName;
        ArrayList MethodComment;
        private Boolean isSet = false;

        public FormatData() { // Default constructor; this initializes all the arraylists.
            this.rawConstructorsorMethods = new ArrayList();

            this.Constructor = new ArrayList();
            this.ConstructorComment = new ArrayList();

            this.Variables = new ArrayList();

            this.Types = new ArrayList();
            this.MethodName = new ArrayList();
            this.MethodComment = new ArrayList();
        }

        public void SetClassName(String passedTitle) { // This is a one-time-use method, it runs once, and is set forever.  I'd probably need to extend it more if I wanted multiple classes.
            if (isSet == false) {
                this.ClassName = passedTitle;
                isSet = true;
            }
        }

        public String GetClassName() { // This returns the class name, less attractive version.
            if (isSet == true) {
                String[] headerData = this.ClassName.ToString().Split(' ');
                return headerData[1];
            }
            return "Error: No Class Name Set";
        }
        
        public String ReturnClassName() { // This is the more attractive version of GetClassname.
            return "Class: " + GetClassName() + "\n\n";
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
            this.Constructor.Add(passedConstruct.Trim());
            this.ConstructorComment.Add("");
        }

        public void SetConstructor(String passedConstruct, String passedComment) { // Similar to the method above, but with a passed comment variable.
            this.Constructor.Add(passedConstruct.Trim());
            this.ConstructorComment.Add(passedComment.Trim());
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

        public String ReturnConstructor(int positionValue) {
            return this.Constructor[positionValue].ToString() + " -- " + this.ConstructorComment[positionValue].ToString();
        }

        public int ConstructorCount() { // This is an assistance function, this is for a loop or something.
            return this.Constructor.Count;
        }

        public String GetConstructor(int positionValue) { // Function that gets a constructor saved a specific location.
            return this.Constructor[positionValue].ToString();
        }

    }

}
