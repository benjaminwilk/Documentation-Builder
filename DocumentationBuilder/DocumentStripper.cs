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

    

}
