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
        private String[] dividedUserInput;

        public DocumentStripper(String rawUserText, FormatData fd) {
            SplitInputText(rawUserText);
            TrimUserInput();
            StoreConstructorsAndFunctions(fd);
        }

        public void SplitInputText(String rawUserText) { // Divides each line by newline, and places the data in a string array.
            dividedUserInput = rawUserText.Split('\n');
        }

        private void TrimUserInput() {
            for (int i = 0; i < dividedUserInput.Length; i++) { // Iterates through string array and trims blank spaces.
                dividedUserInput[i] = dividedUserInput[i].Trim();
            }
        }

        private void StoreConstructorsAndFunctions(FormatData fd) {
            for (int p = 0; p < dividedUserInput.Length; p++) {
                if (dividedUserInput[p].Contains("class")) {
                    fd.SetClassName(dividedUserInput[p]);
                }
                String constructorMatch = @"^(public|private).([^\s]+).\(";
                String functionMatch = @"^(public|private).\w+\s\w+\(";
                String functionOrConstructor = @"^(public|private)\s\w+.*$";
                foreach (Match m in Regex.Matches(dividedUserInput[p].ToString(), functionOrConstructor)) {
                    if (!m.Value.Contains('=') && Regex.IsMatch(m.Value, constructorMatch)) {
                        fd.SplitConstructorComment(m.Value);
                    }
                    else if (!m.Value.Contains('=') && Regex.IsMatch(m.Value, functionMatch)) {
                        fd.SplitFunctionComment(m.Value);
                    }
                }
            }

        }
    }

    

}
